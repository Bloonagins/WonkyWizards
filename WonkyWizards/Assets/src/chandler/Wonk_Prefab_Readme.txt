Unity Store keywords:
	Wizard
	Wizards
	2D
	2-D
	2 Dimensional
	2-Dimensional
	Top Down
	Top-Down
	Fantasy
	Spellcaster
	Sorceror
	Old Man
	Pre-made
	Cooldown
	Cooldowns
	Controls

Component Documentation:
	Transform:
		Controls the position of the player in the scene
	
	Sprite Renderer:
		Determines what sprite is displayed for the prefab
	
	Box Collider 2D:
		2D rectangular shaped hit-box for the prefab (determines the zone where things can collide with the prefab)
	
	Rigidbody 2D:
		Handles Unity physics and collisions
	
	Player Script:
		Handles data about the player (like HP, mana, what spell is selected, where the cursor is on screen, what mode the player is in, etc.)
	
	Player Controls:
		Determines what to do with player inputs (movement, shooting, switching modes, etc.)
	
	Player Timer:
		Keeps track of cooldowns for dashing, taking damage, and spell casting for the player
	
	Wave Supplier:
		Gives the player full HP and a set amount of mana at the start of setup of each wave

How to setup:
	Player Controls:
		- Movement Speed: Set desired Movement Speed value (30 is recommended)
		- Dash Speed: Set desired Dash Speed value (600 is recommended)
		- Spells: Set the size of the spell array to the number of spells you'd like to have, and then drag the prefabs for the spells into each slot in the array
		- Summons: Set the size of the summon array to the number of spells you'd like to have, and then drag the prefabs for the summons into each slot in the array
		- Bee Movie Script: drag desired text object from scene hierarchy into slot when placing prefab into hierarchy
	
	Wave Supplier:
		- Wave Spawner: drag wave spawner object from scene hierarchy into slot when placing prefab into hierarchy