# Read Me 
## EnemySpawner Prefab

### Keywords
Enemy, Spawner, Enemy Spawner, Wave Spawner, Boss Wave Spawner

### Script: WaveSpawner.cs
This script allows the user to create their own unique enemy waves. Each wave, the user has the ability to edit values for the waves in the inspector window of Unity. This makes it easier and quicker for the user to implement enemy waves of their choosing. 

### Prefab Components
* The components listed below are needed for the EnemySpawner Prefab

#### Spawn Points
The spawn point is a game object that can be moved around on the map which is included in the prefab. To have more than one enemy spawn point, clone the original spawn point and move it to desired location.

#### Surface 2D
Surface 2D is a NavMesh game object that has the NavMesh Surface 2D script attached to it for the NavMesh to be re-baked after the end of every round.