# GoblinBeserker Prefab

## Keywords
AI, enemy, unit, npc, targeting, pathfinding, movement

## Introduction
This ReadMe contains all necessary information on the GoblinBeserker prefab, explaining each component and how to replicate it.

## Prefab Components
The components listed below are needed for the GoblinBeserker prefab.

### Transform
The Transform component is added when you create an game object. It's used for keeping track of the position, rotation and scale of the game object.

### Sprite Render
The Sprite Render component is used to add a visual sprite to the game object. The color, order or orientation of the sprite can be changed.

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
 
