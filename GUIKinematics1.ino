#include <Servo.h>

Servo servo1, servo2, servo3;
int startpos = 45;

const float e = 30;     // end effector
const float f = 105.0;     // base
const float re = 110.0;
const float rf = 155.0;

// trigonometric constants
const float sqrt3 = 1.73205;
const float pi = 3.141592653;    // PI
const float sin120 = sqrt3 / 2.0;
const float cos120 = -0.5;
const float tan60 = sqrt3;
const float sin30 = 0.5;
const float tan30 = 1 / sqrt3;


// inverse kinematics
// helper functions, calculates angle theta1 (for YZ-pane)
int delta_calcAngleYZ(float x0, float y0, float z0, float& theta) {
    float y1 = -0.5 * 0.57735 * f; // f/2 * tg 30
    y0 -= 0.5 * 0.57735 * e;    // shift center to edge
    // z = a + b*y
    float a = (x0 * x0 + y0 * y0 + z0 * z0 + rf * rf - re * re - y1 * y1) / (2 * z0);
    float b = (y1 - y0) / z0;
    // discriminant
    float d = -(a + b * y1) * (a + b * y1) + rf * (b * b * rf + rf);
    if (d < 0) return -1; // non-existing point
    float yj = (y1 - a * b - sqrt(d)) / (b * b + 1); // choosing outer point
    float zj = a + b * yj;
    theta = 180.0 * atan(-zj / (y1 - yj)) / pi + ((yj > y1) ? 180.0 : 0.0);
    return 0;
}

// inverse kinematics: (x0, y0, z0) -> (theta1, theta2, theta3)
// returned status: 0=OK, -1=non-existing position
int delta_calcInverse(float x0, float y0, float z0, float& theta1, float& theta2, float& theta3) {
    theta1 = theta2 = theta3 = 0;
    int status = delta_calcAngleYZ(x0, y0, z0, theta1);
    if (status == 0) status = delta_calcAngleYZ(x0 * cos120 + y0 * sin120, y0 * cos120 - x0 * sin120, z0, theta2);  // rotate coords to +120 deg
    if (status == 0) status = delta_calcAngleYZ(x0 * cos120 - y0 * sin120, y0 * cos120 + x0 * sin120, z0, theta3);  // rotate coords to -120 deg
    return status;
}


void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
  Serial.setTimeout(3);
  servo1.attach(8);
  servo2.attach(9);
  servo3.attach(10);
  
  servo1.write(startpos);
  servo2.write(startpos + 10);
  servo3.write(startpos);
}

void loop() {
  while(Serial.available() == 0){
  }
  String input = Serial.readString();
  String xval, yval, zval;
  int x, y, z;
  int th1, th2, th3;
  float theta1, theta2, theta3;
  Serial.flush();

  for(int i = 0; i < 3; i++){
    xval += input[i];
    yval += input[i+3];
    zval += input[i+6]; 
  }  
  x = xval.toInt();
  y = yval.toInt();
  z = zval.toInt();
  
  x = map(x, 0, 800, -60, 60);
  y = map(y, 0, 600, -45, 45);
  
  delta_calcInverse(x, y, z, theta1, theta2, theta3);
  th1 = theta1 * -1;
  th2 = theta2 * -1;
  th3 = theta3 * -1;
  
  if((th1 && th2 && th3 > 1) && (th1 && th2 && th3 < 155)){
    servo1.write(th1);
    servo2.write(th2 + 10);
    servo3.write(th3);
  }
  input = "";
  xval, yval, zval = "";
}
