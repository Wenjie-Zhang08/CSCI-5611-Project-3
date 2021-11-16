# CSCI-5611-Project-3

# Video
https://youtu.be/OZ5Tk0ndL5o

# List of Features
## Part 2
* Single-arm IK
* Multi-arm IK
* Joint Limits (2D)
* Obstacles (2D)
* 3D Simulation & Rendering
* User Interaction
## Part 3
* FBRIK (3D)
* Compare (Included in this File)
## Note
2D Version is based on Processing and 3D Version is based on Unity

# Art Contest
![image](https://user-images.githubusercontent.com/81786534/141908574-e134de46-97c4-4e89-8fce-55c12bacefd8.png)


This Link is a fun video of failed Moving IK :)

https://youtu.be/xki90C4lUwQ

# User Guide
## 2D Version
* Mouse - Target
* Arrow keys - Moving the **red obstacle** in the scene
## 3D Version
* Mouse Drag - Moving Goals
* Arrow Keys - Change camera rotation angles
* w or s - Moving camera forward or backward
* a or d - Moving camera left or right

# Compare between two methods
## CCD
**Advantages:** 
* This method is using the "local angles" of each joint to calculate the position of their children's position. It is very accurate because we calculate the children's position using angles and fixed length.
* Very straight forward with the setting of the joint limits because you are just setting the limit for "local angles".

**Disadvantages:** 
* The rotation of the child joint is depent on its parent's joint rotation. The distortion of parent's joint rotation could lead to the child getting far from reaching the goal.
* Hard to implement. You need to think about the relationship (e.g. the local rotation) between parents and children carefully and further think about where the joint should be after the rotation.

## Fabrik
**Advantages:** 
* Easy to implement. No more need to consider about rotation.
* While goal is inside the range of the arm, the Fabrik method can easily reach the goal.

**Disadvantages:** 
* While using the Fabrik, we are changing the position of each joint. Caused by this feature, it is hard to calculate the "local angles" for each joints. As a result, it is hard to limit the angles of each joint while using Fabrik. 
* The joint position could be not so precise in each frame, even though we set a large max iteration number. (e.g., there could be some arms with slightly large or small length after each update).


# Discription of Features and Timestamp
## Moving obstacle and collision with limited angle joints (2D)
**Timestamp:** 0:00 - 0:32

On default, the joint angles are limited which is similating the top-down looking of human arms. 
While the arm hits an obstacle, it will be pushed back by some angle.
The red obstacle can be moved around the scene using the arrow key.


## Turn on/off the limited angles of joints (2D)
**Timestamp:** 0:32 - 1:30

You can press "m" to turn the angle limits of joints off, which means the joints can turn freely.
When the angle limit is turned off, The two arms will finally overlap. That is because the two arms sharing one root which has a fixed position.
Press "m" again can turn on the angle limits.


## 3D rendering and comparing FABRIK and CCD
**Timestamp:** 0:32 - 2:27

The model with "F" on it is implemented using FABRIK and the model with "C" on it is implemented using CCD.
You can use the arrow key and WASD to change the angle and position of the camera.
Each goal could be draged and move in a surface which is parallel to the screen using the mouse.


# difficulties encountered
* In 3D world, it is hard to calculate the angles from one direction to another. One have to choose one axis as primary rotation axis and another as secondary rotation axis.
* I tried to use the Quaternion instead, however, It is hard to use the "Quaternion to Euler transfer" to provide the correct answer I need.
* It is hard to set speed and angle limit only based on Quaternion in 3D.
* The rotation direction in 3D space is a bit confusing.
