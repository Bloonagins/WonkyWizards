# TheGreatGob Prefab

## Introduction
This ReadMe file contains all of the necessary information about TheGreatGob Pre Fab.

## Prefab Components
Items listed below are what make up the necessary items of TheGreatGob which would need to be replicated to create him.

### Transform
The Transform component is added when you create a game object. Keeps track of The Great Gobs movements.

### Sprite Render
The Sprite Render component is there so you can show teh aspects of a sprite, some of these are color, order and orientatipn of the sprite.

### Circler Collider 2D
The Circler Collider 2D component is used for detecting collisions in Unity. The material, isTrigger, and radius can all be modified.

### Rigidbody 2D
The Rigidbody 2D component is used for the built in Unity physics. The mass, linear drag, angular drag, gravity scale, and collision detection can be changed.

### Nav Mesh Agent
The Nav Mesh Agent component is from the NavMesh library and is used to apply the NavMesh features to an agent. It controls the agents speed, angular speed, acceleration, stopping distance, radius, height, and pathfinding. 

### Enemy Targeting Script
This script is used to control the GoblinBeserkers movement, targeting, and behaviour.

### Goblin Beserker Script
This script is used to update and track the GoblinBeserkers attributes like its health, damage, move speed, ect.. It also contains the unique methods for the GoblinBeserker.

## Health Bar Script
This script is used to control and update the UI health bar for the GoblinBeserker.
 