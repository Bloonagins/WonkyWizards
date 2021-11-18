# Read Me
## Introduction

My features for Bloonagin's game, Wonky Wizards, is the tile map design and the enemy wave spawner.

At the start of each level, the map is loaded along with the tile map and the enemy spawner. My job is to design and create the tile maps of the map and assign a array to the map that follows its placeable tiles and non-placeable tiles. This is important so that Ethan can have accurate map data on where Summons can and cannot be placed on the map.

My other feature is the implementation of the enemy waves. Each level has different waves and each wave for the level will be different to allow for unique gameplay everytime you play the game. At the start of each round the script waits to spawn enemies until the player changes from setup phase to play phase. Then, certain assigned enemies will spawn from the enemy spawner every spawn interval until the last enemy of the current wave is spawned. After the last enemy of the wave is killed, then the game phase will change from play phase to setup phase.