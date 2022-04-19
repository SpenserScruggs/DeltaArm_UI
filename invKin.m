function [ thetas ] = invKin( x, y, z )
%%base equilateral triangle side 
sB = 138.56;
%%platform equilateral triangle side 
sP = 27.7;
%%upper legs length  
L = 100;
%%lower legs parallelogram length 
l = 158;
%%lower legs parallelogram width
h = 28;
%%planar distance from {0} to near base side
wB = (sqrt(3)*sB)/6;
%%planar distance from {0} to a base vertex 
uB = (sqrt(3)*sB)/3;
%%planar distance from {P} to near platform side 
wP = (sqrt(3)*sP)/6;
%%planar distance from {P} to a platform vertex 
uP = (sqrt(3)*sP)/3;

%%vector loop-closure equations constants
a = wB - uP;
b = (sP- sqrt(3)*wB)/2;
c = wP-.5*wB;

%%vector loop-closure equations scalers
E1=2*L*(y+a);
E2=-L*(sqrt(3)*(x+b)+y+c);
E3=L*(sqrt(3)*(x-b)-y-c);
F=2*z*L;
G1=(x^2)+(y^2)+(z^2)+(a^2)+(L^2)+2*(y*a)-(l^2);
G2=(x^2)+(y^2)+(z^2)+(a^2)+(L^2)+2*(x*b+y*c)-(l^2);
G3=(x^2)+(y^2)+(z^2)+(a^2)+(L^2)+2*(-1*x*b+y*c)-(l^2);
%%trig subbed solution to loop-closure equations
t1plus=(F+sqrt((E1^2)+(F^2)-(G1^2)))/(G1-E1);
t1minus=(F-sqrt((E1^2)+(F^2)-(G1^2)))/(G1-E1);
t2plus=(F+sqrt((E2^2)+(F^2)-(G2^2)))/(G2-E2);
t2minus=(F-sqrt((E2^2)+(F^2)-(G2^2)))/(G2-E2);
t3plus=(F+sqrt((E3^2)+(F^2)-(G3^2)))/(G3-E3);
t3minus=(F-sqrt((E3^2)+(F^2)-(G3^2)))/(G3-E3);
%%inverse of trig sub
thetasRaw = [2*atan(t1plus)*(180/pi) 2*atan(t1minus)*(180/pi) 2*atan(t2plus)*(180/pi) 2*atan(t2minus)*(180/pi) 2*atan(t3plus)*(180/pi) 2*atan(t3minus)*(180/pi)];
thetas=[0 0 0];

if(thetasRaw(1)<=thetasRaw(2))
    thetas(1)=thetasRaw(1);
else
    thetas(1)=thetasRaw(2);
end

if(thetasRaw(3)<=thetasRaw(4))
    thetas(2)=thetasRaw(3);
else
    thetas(2)=thetasRaw(4);
end

if(thetasRaw(5)<=thetasRaw(6))
    thetas(3)=thetasRaw(5);
else
    thetas(3)=thetasRaw(6);
end
end
 