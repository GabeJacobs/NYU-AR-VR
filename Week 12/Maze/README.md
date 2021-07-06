NYU VR-AR Developmenmt  
Gabe Jacobs   
07/06/21  
# Assignment 12.1: Create a Maze  

This is my submission to Assignment 12.1: Create a Maze. The project is a C# Unity project that runs using Unity 2019.4.26f1 and an Oculus Quest 2.  
  
  This is a prototype of a simple VR game that places the user into a maze within an office. The user can teleport around and use the joystick to rotate their view 45 degrees. Certain paths lead to dead ends, and other paths lead to interactable object such as a key. The user can teleport using the left controller's trigger, and pickup the key using the rigth controller's trigger. To open the door, the user must place the key into the handle of the door. Finally, walking into the green boxed area finishes the game.
  
  This prototype uses a number of helpful packages:  
  - XR Plugin Management
  - XR Interaction Toolkit
  - Oculus XR Plugin
  - ProBuilder
  - ProGrids
  - [Snaps Prototype | Office](https://assetstore.unity.com/packages/3d/environments/snaps-prototype-office-137490)

Using these packages helped create a seamless XR experience with the oculus quest. Progrid was especially useful in creating the maze since it quickly allined all of the floors and walls that were used in the level. Furthermore, Probuilder was helpful in creating the key object. Rather than having to find or pay for an external asset, probuilder allowed me to make the key very quickly.

  In order to make the scene more immersive, I also added a looping sound effect of [office noise](https://www.soundsnap.com/office_wardrop_lite_voices_activity_4). This brings the scene to life a little more, rather than it being silent throughout the experience.
  
  Here is a [video walkthrough](https://drive.google.com/file/d/1-2rizCVzm83JwTTY8G615JEkudONS1_L/view?usp=sharing) of the experience.
