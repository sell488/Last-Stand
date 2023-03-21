# Last-Stand
A wave defense fps
## Overview
Last Stand is a game designed and developed in a short, six week sprint in order to develop an understanding of basic real time physics simulation, AI pathfinding, and
game design and development ideas. As a wave defense, the core loop of Last Stand is surviving infinite waves of enemy robots attempting to destroy you 
and your base while you attempt to disable all robot spawners. If either the base or the player reaches zero health, the player loses.
As a first person shooter game, Last Stand features various weapons and weapon categories. 

## Disclaimer:
  This project was developed over a very short time period. Although efforts were made to use best practice when implementing features, band-aid fixes and stop-gap implementations
  became increasingly common as the deadlines approached. Specifically, many variables were made public or assignable in the editor when they very much should _not_ have been.
  In addition, comments and organization following the first few weeks are sorely lacking. Many mechanics are dependent on each other when they should not be (i.e, the shop
  not working unless a certain variable that it never uses is assigned in the editor). Many of these issues are addressed in discussions addressing future work, but there are others
  that are not. This disclaimer acts as a clarification that the primary goal of this project was to experiment with implementations of various features and stringing them
  together into a playable experience, as well as an exploration of the various facets of game design from visual art to scripting mechanics in a very short amount of time and not necessarily as a representation of what is considered best practice.

### Mechanics
A list of mechanics that a substantial amount of development time was allocated towards.
- Physics based ballistics:
  - Considerable development time was spent working on a raycast based projectile system. This system functions through a script
  that calculates a projectile's current, future, and past positions taking into account gravity and wind drag entirely independent of Unity's native RigidBody system. 
  This was done with inspiration from the GitHub user
  [Shaded Technology](https://github.com/ShadedTechnology/SniperShootingTutorial). Although the system implemented in Last Stand is based on
  In Shaded Technology's work, much of the original code was rewritten and updated, specifically regarding unaccounted for edge cases in the source code. 
  Additionally the calculation of the force exerted by air drag on a projectile, and the calculation of damage done to an enemy based on the velocity and mass of a projectile
  is entirely original. Future work on this mechanic could involve: 
      - Refinement of [air drag](https://en.wikipedia.org/wiki/Drag_(physics))
        calculations. Currently, air drag is calculated based on a Cd that remains constant. However, this isn't consistent with the real world, where Cd is dependant most importantly
        on velocity. Thus, linear drag is applied to high velocity projectiles when in reality it would be exponential drag. Due to time constraints, a more accurate calculation
        of air drag with reasonable space/time complexity was postponed.
      - Wind effect on projectiles. This would be relatively simple to implement as the only requirements would be a Wind Manager that tracks all active wind zones in a game instance
      and logic to apply the force of the wind in the correct direction on any projectile in a wind zone.
  - Additional future work:
    - Bouncing projectiles independent of Unity's native method for bouncing projectiles
- Weapon system:
  - The weapon system is based on 2 general weapon scripts with unique functionality. The main script has functionality to determine basic weapon stats such as fire rate,
  muzzle velocity, ammo information, and most uniquely, reload behavior. One script, the 'Firearm' script, implements a simple reload system of playing a reload animation, waiting until the animation
  is finished, and then refilling ammo. This system is useful for common weapons such as pistols and rifles. The second script, the 'Shotgun' script extends the first and overrides the reload method
  so that one round is loaded into the player's gun at a time in order to represent weapons such as shotguns. The reload loops automatically for the player until
  the gun has reached capacity or the player has interrupted the reload by firing. This second script also overrides Firearm's shoot script in order to instantiate multiple
  bullets at once with an adjustable spread for every button click of the Fire button. Weapons are built to be highly customizable; however, time constraints permitted for only
  general weapons to be implemented. Thus, future progress could be focused on developing and implementing the already existing foundations of some systems. This would include
  implementation of various ammo types for a single weapon, such as a player being able to switch between both slow, explosive ammo and fast, kinetic ammo. Currently,
  weapons can accept specific ammo types. Ammo types can determine damage and range of a weapon. However, this is not currently obvious in game play. Additional future work
  could be focused on various bug and code coherence fixes, such as reloads being based on how long a weapon's reload animation. Currently, the reload time is assignable in the editor
  and does not depend on its animation. This leads to a potential issue where the reload animation takes longer than the reload time, those allowing the player to fire while
  the animation is still playing, or the animation is too short and leaves the player unable to fire when it seems like they have reloaded.
- Spawner system:
  - The spawning system of this game is built upon a Spawner Manager that controls every Spawner parented to it. The Manager controls how often spawners should be enabled
  as well as for how long. The individual spawners handle how often a group of enemies should spawn, how large the group of enemies is, and what type of enemies spawn. Example:
    - The Manager enables all of its spawners every 60 seconds for 10 seconds. During the 10 seconds that a specific individual Spawner is active, it spawns a group of 1-3 enemies every
    5 seconds. Therefore, the spawner spawns two groups of 1-3 enemies in 10 seconds and then is deactivated. This system allows for fine tuning of how the game's difficulty
    will scale. Each spawner can have its own unique behavior while the Manager controls how long every spawner is activated for. Therefore, the difficulty of the game can 
    be increased or decreased simply by changing how often the Manager enables its spawners and for how long. Future work in this area could involve randomly selecting between
    different enemy types that are instantiated in each group as well as scaling difficulty based on how many spawners the player has destroyed so that the game becomes harder
    as they progress instead of easier.
  - Players must destroy all spawners on the map in order to win the game. However, spawners cannot simply be shot from a distance until destroyed. A player must
  first get close enough to a spawner to disable its "shield". Once the player is close enough to a spawner, the "shield" is disabled and a boss wave is spawned which consists of a large number of enemies.
  With the shield disabled, the player can freely damage the spawner until it is destroyed. Future work in this area could be to fix shader errors in builds as spawners
  currently turn black when playing their destruction animation.
- Enemy system:
  - Enemies in this game are relatively simple. Using Unity's native NavMesh system, enemies pick and navigate to either the player or the player's base, depending on which is closer.
  Enemies traverse at a constant traveling speed until they are within a certain radius of their target, at which point they accelerate until they reach the target.
  Once at the target, an enemy will damage the target at a specified rate. Future work on this mechanic could be the implementation of various enemies. Although an enemy
  that launches damaging projectiles at the player exists in code, time constraints prevented it from being implemented in the game. Furthermore, more performant behavior
  should be implemented for enemy pathfinding behavior as currently two paths are calculated and then compared to each other in each frame to determine which target to choose.
  This is unnecessarily complex and could be easily reduced to a check based on seconds instead of frames.
- Shop system:
  - The player gains points for every robot they defeat. They can then spend these points in the shop. Players can purchase health (for themselves), ammo, defensive towers, and weapons.
- Defensive Towers:
  - Purchased in the shop, defensive towers are automatically placed around the player's base and automatically attack nearby enemies. Further work on this mechanic could
  be:
    - Implementation of line-of-sight limits. Currently towers can attack and damage enemies that are obstructed by obstacles and which the tower should technically not be able to
  "see". 
  - Upgrade system to towers to increase their attack speed and damage.
  - Health system. Allowing enemies to target and damage towers, eventually destroying them.
- Minimap:
  - The minimap acts as a way to pinpoint where a player is on the maze-like map as well as to inform them of where currently active enemies are. Enemies that are outside
  the player's minimap are clamped to the edge of the map, allowing the player to notice when enemies are active but at a distance. Future work on this system could be
  to expand it into a full screen map that can be toggled on or off if the map becomes larger. Additionally, the enemy markers are currently bugged in the build version
  of the game, but not within the Unity editor, which is an issue.
- Tutorial:
  - The playable tutorial acts as a way to familiarize the player with the game's controls and mechanics. Although bare bones, it is designed to be more efficient and interactive than
  simply reading a guide. Future work on this mechanic could be to make goals within the tutorial more clear. Currently, visual cues within the tutorial are not very apparent.
  
## Visuals
List of premade assets used:
-
- [Gun models and textures](https://assetstore.unity.com/packages/3d/props/guns/low-poly-weapons-vol-1-151980)
- [Enemy models, textures, and animations](https://assetstore.unity.com/packages/3d/animations/melee-warrior-animations-free-165785)
- [Enemy damage effect sprites](https://assetstore.unity.com/packages/vfx/particles/hit-impact-effects-free-218385)
- [Trees and rocks](https://assetstore.unity.com/packages/3d/vegetation/lowpoly-trees-and-rocks-88376)
- [UI Buttons](https://assetstore.unity.com/packages/2d/gui/icons/ui-button-pack-2-1200-button-130422)
- [UI icons](https://assetstore.unity.com/packages/2d/gui/icons/fps-icons-pack-45240)
- [Guide for spawners' shield effect](https://www.youtube.com/@GameAcademySchool)

In-house creations:
- Weapon reload animations
- Enemy hit effect (using premade sprites)
- Smoke effects
- Bullet ground hit effect
- Spawner destruction animation

## Download instructions
In order to play the game, a windows computer is required, preferably running Windows 10 or above. Only the [included zip file](Last%20Stand%20Final.zip) is necessary. 
Once downloaded, unzip the folder, open it, and run the Last Stand executable.  **Play the tutorial first!**

