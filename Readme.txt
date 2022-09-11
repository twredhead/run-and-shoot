
Run and Shoot

************************************************************************************
************************************************************************************

This is a small first person shooter project. The goal at the outset was to have a 
create a first person controller, allow the player to shoot things with raycasting,
and have enemies patrol and shoot at the player when discovered.


************************************************************************************
************************************************************************************

FPS Controller

Functionality: The FPS controller allows the player to move around the world using
the WASD keys. The player can look around, and steer with the mouse. There are 
two speed that the player can move at, and the player can jump. The movement 
controlls are in the script PlayerMovementControls.cs, and the steering and looking
mouse controls are in MouseLook.cs. These two scripts must be together on the 
player game object, and the player game object must have a camera childed to it. 
Additionally, any surface that is meant to be walked on needs to have the tag 
"surface".

Functionality that could be added:
1. Headbob, it could be nice to have some up and down motion while moving.
2. A sound controller. It would be nice to have some walking sounds that play
   while the player is moving.

************************************************************************************
PlayerMovementControls notes:

The tag, "surface" is used in OnCollisionEnter() to detect if the player 
has collided with the ground. Collisions with "surface" objects sets the boolean
flag, isAirborn, to false. The purpose of isAirborn is to turn off the ability
to run and jump immediately after the player jumps (you cannot run, walk, or jump
while still in the air). 

ToDo: right now is airBorn is only switched to true when the player jumps. 
It should also be switched if the player falls off something. 

************************************************************************************
MouseLook notes:

For the MouseLook script to work as intended, the camera must be hooked up to the
serialized field called Player Camera in the inspector when the player game object
is selected. The camera is rotated about its X axis with Y Axis mouse movement to 
simulate looking up and down. The camera is rotated, not the player object, as 
rotations of the player object could allow the player to start flying. 
The player cannot look above or below an angle specified by the serialized field 
maxVerticalAngle; the player should not be able to rotate their head 360 degrees.
If a weapon is added to the player it must be childed to the camera, otherwise
the gun will only be able to shoot in the XZ plane.

The player object is rotated about the Y axis using X Axis mouse movement. This allows
the player to change the direction that they are moving.








