
Run and Shoot

************************************************************************************
************************************************************************************
************************************************************************************

This is a small first person shooter project. The goal at the outset was to have a 
create a first person controller, allow the player to shoot things with raycasting,
and have enemies patrol and shoot at the player when discovered.


************************************************************************************
************************************************************************************
************************************************************************************

Neutral scripts (not player or enemy): 
************************************************************************************

- Weapon.cs

   This script should be attached to any weapon prefab that is to be fired like a gun.
   Weapon.cs currently has several serialized fields. The most important is called 
   'shooter'. The transform of 'shooter' is used in the raycast for the start position 
   and for the direction. The serialized field called 'gunShotVFX' should contain a 
   particle system with the desired FX. When FireWeapon() is called, the chosen particle 
   system will play, provided the field is not null. The serialized field called 'range'
   should be fiddled with in the inspector. This field is how far the raycast can go.
   
   Public Methods: 
   - FireWeapon()
      This method uses a raycast to simulate a bullet. It handles the vfx for the gun
      being fired, and it also handles the vfx for visual feedback on what has been hit.
      This method also deals damage to any enemy hit by the raycast.

      TODO: 
      1. Add a vfx for hitting the environment, not just the player or enemy.
      2. Add functionality to damage player health if player is hit by raycast.
      3. Make shooting a coroutine to stop the player, and the enemy, from spamming
         FireWeapon().


***********************************************************************************
************************************************************************************
************************************************************************************
      

Player Controls 

************************************************************************************
************************************************************************************
FPS controller

Functionality: The FPS controller allows the player to move around the world using
the WASD keys. The player can look around, and steer with the mouse. There are 
two speed that the player can move at and the player can jump. The movement 
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
PlayerMovementControls.cs notes:

The tag, "surface" is used in OnCollisionEnter() to detect if the player 
has collided with the ground. Collisions with "surface" objects sets the boolean
flag, isAirborn, to false. The purpose of isAirborn is to turn off the ability
to run and jump immediately after the player jumps (you cannot run, walk, or jump
while still in the air). 

ToDo: right now is airBorn is only switched to true when the player jumps. 
It should also be switched if the player falls off something. 

************************************************************************************
MouseLook.cs notes:

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

************************************************************************************
************************************************************************************
Attack Controls

The player is able to attack by clicking the left mouse button. The attack script
is on the player camera as the player camera has the transform that can look up or 
down. The actual firing of the weapon is taken care of by the script Weapon.cs, which
is attached to the weapon prefab. The reason I decided to put the firing script on 
the weapn is so that the enemies will be able to use the same prefab.
Currently, the only script that deals with player attacking is PlayerAttack.cs

************************************************************************************
PlayerAttack notes:

This script currently does one thing. It calls the public method FireWeapon() from
Weapon() when the left mouse button is pressed down. 

************************************************************************************
************************************************************************************
************************************************************************************

Enemy Scripts

************************************************************************************
************************************************************************************
Enemy AI

Navigation:

Enemy navigation is taken care of in the script EnemyAI.cs. Enemies can either be
patrollers (default) or sentries. Patrollers will move toward the nearest 
waypoint that is not the last waypoint visited. Waypoints are empty objects
that have a script called Waypoint as a component. Additionally, each enemy must
have a NavMeshPro component as once the closest waypoint (that is not the last 
waypoint) is found, NavMeshPro.SetDestination(destination) is used to navigate toward 
the destination waypoint. 
For navigation the EnemyAI.cs script finds each instance of Waypoint in the scene and 
iterates through an array containing their transforms in order find the closest Waypoint. 
The destination is set to be the closest waypoint that is not equal to the last 
destination. The navigation only occurs if the bool isPatrolling is set to true. 
If damage is taken isPatrolling is set to true.

Searching for Player:

The enemy is constantly searching for the player using a ray cast to simulate vision.
Enemy vision works by telling the enemy where the player is, then sending a raycast
in the direction of the player. If the object hit by the raycast is the player set the 
bool canSeeTarget to true, otherwise set canSeeTarget to false. 

Attack Player:

If canSeeTarget is true, the enemy rotates toward the target in the xz plane. The
weapon is also rotated toward the target, but it also rotates in the yz plane so
that the weapon points toward the player if it is not on the ground. To attack
the player Shoot from EnemyAttack.cs is called.

************************************************************************************
Notes:

Public Methods:
- IsPatrolling(bool trueOrFalse) 
   This method is used to set the bool isPatrolling to true or false based on
   the input parameter.


************************************************************************************
************************************************************************************
Enemy Attack

This script should be a component on a empty object that has a weapon prefab as a 
child. The empty object acts like the camera does in the player attack script.

************************************************************************************
Notes:

Public Methods:
- Shoot()
   This method calls FireWeapon from the Weapon.cs and shoots the gun.

   ToDo: 
   1. This method needs to be something like a coroutine to stop it from firing
      every frame.
   2. There needs to be a degree of randomness to whether the player is hit or not.
      Currently the player will be hit every time.


************************************************************************************
************************************************************************************
Enemy health

The enemy health is handled in the script EnemyHealth.cs. This script deals with the
hit points and eventual death of an enemy.

Currently Weapon.cs calls the public method DamageTaken from this script.

************************************************************************************
Notes:

Public Methods:
- DamageTaken(float hitPointDamage)
   This method is called in the weapon script when a raycast has hit an enemy 
   object.

Todo: it would be fun to add a funny vfx for enemy death. Currently the enemy will 
simply disappear when hitPoint <= 0. 















