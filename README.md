# DeltaArm_UI
A simple GUI to control a delta arm through serial input

This project is currently in the prototype phase, it has some functionality but could still benefit from additional modifications. Here is my source for the kinematic equations:

https://hypertriangle.com/~alex/delta-robot-tutorial/

Prototype Render:
![Untitled Project 15](https://user-images.githubusercontent.com/78044374/160977757-f82b6d6d-ac65-4387-82fb-7df79fcd4c2d.png)

Known problems:

1: Com port not recognized - I haven’t made a button on the application for selecting the comm port so to change it, first determine the port of your Arduino,
then on line 58 of MainWindow.xaml.cs change the string to the desired comm port ("COM3" "COM4" ect.).


