# DeltaArm_UI
A simple GUI to control a delta arm through serial input

This project is currently in the prototype phase, it still needs a function to return the xyz values as thetas for the servos 
so that the robot can translate smoothly along a cartesian coordinate system. Additionly there are several know problems:

Known problems:

1: Comm port not recognised - I havent made a button on the application for selecting the comm port so to change it determine the port of your arduino
and on line 58 of MainWindow.xaml.cs change the string to the desired comm port.

2: Program quits after some time - Probably a memory issue but i havent found a solution yet.
