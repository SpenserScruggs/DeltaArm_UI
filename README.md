# DeltaArm_UI
A simple GUI to control a delta arm through serial input

This project is currently in the prototype phase, it has some functionality but could still benefit from additional modifications. Here is the source for the kinematic equations:

https://hypertriangle.com/~alex/delta-robot-tutorial/

Prototype Render:
![Untitled Project 15](https://user-images.githubusercontent.com/78044374/160977757-f82b6d6d-ac65-4387-82fb-7df79fcd4c2d.png)

Prototype Functioning:
https://user-images.githubusercontent.com/78044374/164074046-c8909eee-885d-474c-8c80-e7db3166021f.mov

Record Feature:
https://user-images.githubusercontent.com/78044374/164074614-67ea297b-0f7b-4717-87f8-4b7f0d07ca0b.mov


MATLAB Code for Inverse Kinimatics Solution:
invKin.m

Code describes delta robot arms through 3 loop closure equations. Each loop closure equation is transformed from cartesian to a servo angle. Transformed loop closure equations are then trigonemetricly subsituted. Trig subbed equations are then solved with the quadratic formula and 2 solutions are given for every equation one solution with arms kinked in and one with arms kinked out. The kinked out solution is selected through a sorting loop that chooses the smaller servo position thus selecting the preffered solution. From there the selected solutions from each loop closure solution is combined into an array and returned.


AutoCAD files for frame of arm:
