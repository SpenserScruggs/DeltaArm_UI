# DeltaArm_UI
  The goal of this project was to create a robotic arm that could track mouse movements and physically write them out on paper. Its purpose was to make signing large amounts of physical documents easier by recording a signature and then replaying it as many times as necessary. The project also exists as just a fun item to build and design and to help introduce people to robotics and electro-mechanical systems.

  This project includes the code for a graphical user interface made using WPF, Arduino code to interface with the computer, files for both the 3D printed parts and laser cut parts, MATLAB code to model the kinematic system, a bill of materials to list each part required with both their quantity and an Amazon link to purchase the part listed if applicable, and a manufacturing plan to give step by step instructions for assembly.

Final Product:
![IMG_0710](https://user-images.githubusercontent.com/78044374/167205772-4779bab4-ca41-42d7-8c32-6ada8e1bc74a.jpg)


**Prototype Render:**
![Untitled Project 15](https://user-images.githubusercontent.com/78044374/160977757-f82b6d6d-ac65-4387-82fb-7df79fcd4c2d.png)


**Prototype Functioning:**
https://user-images.githubusercontent.com/78044374/164074046-c8909eee-885d-474c-8c80-e7db3166021f.mov


**Record Feature:**
https://user-images.githubusercontent.com/78044374/164074614-67ea297b-0f7b-4717-87f8-4b7f0d07ca0b.mov


**MATLAB Code for Inverse Kinematics Solution:**
https://github.com/SpenserScruggs/DeltaArm_UI/blob/master/invKin.m

Code describes delta robot arms through 3 loop closure equations. Each loop closure equation is transformed from cartesian to a servo angle. Transformed loop closure equations are then trigonometrically substituted. Trig subbed equations are then solved with the quadratic formula and 2 solutions are given for every equation one solution with arms kinked in and one with arms kinked out. The kinked-out solution is selected through a sorting loop that chooses the smaller servo position thus selecting the preferred solution. From there the selected solutions from each loop closure solution is combined into an array and returned.


**AutoCAD files for frame of arm:**
https://github.com/SpenserScruggs/DeltaArm_UI/tree/master/AutoCAD%20Drawings

  Folder Contains all of the AutoCAD drawings that are needed for the frame of the arm. 


**BOM File:**
https://github.com/SpenserScruggs/DeltaArm_UI/blob/master/BOM.xlsx

  File contains all components needed for creation of the arm, as well as the prices, size, mass, and location of purchase of the components.


**Manufacturing Plan:**
https://github.com/SpenserScruggs/DeltaArm_UI/blob/master/Manufacturing%20Plan.docx

  Document provides step by step instructions for how to assemble the project.


**GUI Application:**
https://github.com/SpenserScruggs/DeltaArm_UI/tree/master/DeltaArm_UI

  This folder contains the C# and xaml code necessary to make the GUI and run the application (only works on Windows). To run it right click on the folder and select open with, then open with visual studio. Additionally if you only want the application/ don’t want to download visual studio you can download the executable directly here at: https://github.com/SpenserScruggs/DeltaArm_UI/blob/master/DeltaArm_UI.exe


**Circuit Diagram:**
https://github.com/SpenserScruggs/DeltaArm_UI/blob/master/Circuit_Diagram.png


**Source for the kinematic equations:**
https://hypertriangle.com/~alex/delta-robot-tutorial/


**Arduino Code:**
https://github.com/SpenserScruggs/DeltaArm_UI/blob/master/GUIKinematics2.ino

