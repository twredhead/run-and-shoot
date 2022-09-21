
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

************************************************************************************
Level Manager
************************************************************************************

The level manager has public methods pertaining to reloading the current level,
quitting the game, and disabling/enabling the player controls.

Public Methods:
    void ReloadLevel()
    - This can be called whenever it is required to reload the current scene.

    void QuitGame()
    - This allows the player to leave the application.
    
    void DisablePlayerControls()
    - This disables the player controls, which is useful for menu screens as I have
      them set up.
    
    void EnablePlayerControls()
    - Re-enable player controls.

************************************************************************************
Victory Condition
************************************************************************************

The victory condition is in a script attached to an empty object called EnemyContainer.
The victory condition is that the enemy container is empty. Each enemy in the scene
must be in the enemy container or else it will not count toward the victory condition
and the game will end without the (not included) being destroyed. Enemy health 
uses the public method ReduceEnemyCount() when destroyed.

************************************************************************************
Weapon
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
   - FireWeapon(Vector3 direction)
      This method uses a raycast to simulate a bullet. It handles the vfx for the gun
      being fired, and it also handles the vfx for visual feedback on what has been hit.
      This method also deals damage to any enemy hit by the raycast.
      The direction is a parameter as this helps the AI miss sometimes.


************************************************************************************
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
The player movement is physics based. 

Functionality that could be added:
1. Headbob, it could be nice to have some up and down motion while moving.
2. A sound controller. It would be nice to have some walking sounds that play
   while the player is moving.

************************************************************************************
PlayerMovementControls.cs notes:

The movement controls are turned off if the player is airborn. This is determined
using a raycast downwards. The raycast has a distance of half the height of the
collider plus a small amount. If the raycast hits anything, isAirborn is set to
false, otherwise isAirborn is set to true. 
 
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

This script calls the public method FireWeapon() from Weapon() when the left mouse 
button is pressed down. 

FireWeapon is now called in a coroutine called Shoot(). This coroutine waits forces
the player to wait between shots. 

When the weapon is fired the amount of ammo on the player is decreased by one. 
The amount of ammo can only be increased from the public method 
IncreaseAmmo(int amount). Currently, this method is called in PickUps.cs when the
pick up type is ammo. 


************************************************************************************
************************************************************************************
Player Health

Player health is taken care of in the script PlayerHealth.cs. The player has a 
preset number of hit points as a serialized field. When hit, the player health 
is decreased by a float passed into PlayerDamageTaken(float hitPointsDamage). 
When the hit points are less than zero, the player game is over. 

************************************************************************************
Notes:

Public Methods:
- void PlayerDamageTaken(float hitPointsDamage)
   This method reduces the player health by hitPointsDamage when called.

- void IncreaseHitPoints(float amount )
  This method is used for health pick ups. If the amount of health specified by the
  parameter 'float amount' plus the current number of hit points is less than the
  maximum number of hit points, the hit points are increased to hitPoints + amount.
  Otherwise, the hit points are increased to maxHitPoints. Currently, this method
  is only called in the script PickUps which is attached to the pick up object prefab.
  
- hitPoints is now a property of PlayerHealth. This is so that the Health Bar 
  can access the current number of hit points.

************************************************************************************
************************************************************************************
Player Death

When the player dies PlayerHealth.cs calls the method HandleDeath() from 
PlayerDeath.cs. This method enables the Game Over canvas, and it makes the mouse 
visible, and sets Cursor lock mode to none.

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
If damage is taken isPatrolling is set to false.

Searching for Player:

The enemy is constantly searching for the player using a ray cast to simulate vision.
Enemy vision works by telling the enemy where the player is, then sending a raycast
in the direction of the player. If the object hit by the raycast is the player set the 
bool canSeeTarget to true, otherwise set canSeeTarget to false. 

Attack Player:

If canSeeTarget is true, the enemy rotates toward the target in the xz plane. The
weapon is also rotated toward the target, but it also rotates in the yz plane so
that the weapon points toward the player if it is not on the ground. To attack
the player Shoot from EnemyAttack.cs is called. To ensure that EnemyAttack.cs.Shoot()
is not called every frame, it is now called in a coroutine. The coroutine waits 
for a time, specified by 'float waitToShoot', to pass.
If the player goes out of sight of the enemy, the enemy now chases the player. 

The enemy has some randomness in if they hit the player or not. This is controlled
by the serialized field 'float skill'. The skill is set to 5 by default. The 
aiming logic is set such that there is approximately a 50% chance for the enemy to
hit the player. The skill should never be larger than 10 or smaller than 0.

************************************************************************************
Notes:

Public Methods:
- void IsPatrolling(bool trueOrFalse) 
   This method is used to set the bool isPatrolling to true or false based on
   the input parameter.

- Vector3 PerturbedTargetDirection()
   This method returns either the exact direction from the weapon to the target
   or it returns a perturbed direction. Which it is depends on a random number
   generated within. If the number is greater or equal to 'float skill' the 
   method returns the perturbed direction. This is to simulate some sort of skill
   level in the enemy. It would be a bummer as the player to get hit everytime
   the enemy shoots. 
   This method is public such that it can be called in EnemyAttack.cs.
   

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


************************************************************************************
************************************************************************************
************************************************************************************

User Interface

************************************************************************************
************************************************************************************

Health Bar
************************************************************************************

The health bar has a script called HealthBar.cs. This script references the player
script PlayerHealth.cs to get the current number of hit points. As the player hit
points are reduced, the area filling the health bar border decreases, showing
the player that they are losing health. The colour of the health bar changes
to yellow once the player has half their starting health, and red when the player
has 1/3 of their starting health.


************************************************************************************
Ammo 
************************************************************************************

The amount of ammo that the player has is shown on the UI. The amount of ammo 
is accessed from the property AmmoAmount from PlayerAttack. This script must be on 
a TMP game object.

************************************************************************************
Game Over Canvas
************************************************************************************

The game over canvas is disabled on start by the PlayerDeath.cs script. It is re-
enabled when the player dies. The buttons depend on an empty game object called 
LevelManager. This game object has a script called LevelLoader.cs. LevelLoader.cs
supplies the scripts to use in the appropriate buttons on the canvas which are 
assigned using OnClick in the button component.

************************************************************************************
Escape Menu
************************************************************************************

When the player presses ESCAPE, an escape menu is made visible. The player is given
the option to resume, restart, or exit the application. Pressing escape again will
close the escape menu, however you can also press a UI button. The script involved
in toggling the escape menu is EscapeMenu.cs. It is attached to the EscapeMenuCanvas.

************************************************************************************
Start Screen
************************************************************************************

The start screen is just a UI canvas that has a script called StartScript.cs which
disables other UI canvases, and the player controls. When any key is pressed, the
player controls are enabled, as are the other UI canvases. The start canvas then
disables itself, and there is no way to re-enable other than to restart the
application.

************************************************************************************
Victory Screen
************************************************************************************

The victory screen is turned on when all the enemies are destroyed. This is 
controlled from the game object EnemyContainer which has the script 
VictoryCondition.cs.

************************************************************************************
************************************************************************************
************************************************************************************

Pick Ups

************************************************************************************
************************************************************************************

There are two types of pick ups in the game. Health pickups and ammo pickups. The 
type of pick up is set by choosing ticking the serialized field isHealth or isAmmo.
You can also select both. The colour of the pick up depends on the type. If the type
is both, the colour is a 50/50 combination of the colours using Color.Lerp(a,b,t).
The pick up uses OnTriggerEnter() to give the player more health, or more ammo, or 
both.















