## Spinning Swords Summon Prefab Documentation

## Keywords
Tower Defence, Tower, Sumon, Sword, Spinning Swords, Sword Tower, Spinning Sword Tower

## Summon Information
		- Cost: 60
		- Travel time: instant
		- Damage per hit: high (15)
		- Firing speed: fast (spins quickly)
		- Range: very short (about one tile)
		- Hitbox size / shape: 4 slim swords
		- Effect: 4 swords spin around very quickly like a rotor. Enemies
				  takes damage each time a sword hits them. Swords magicaly
				  pass through nearby summons and the player without harming
				  them. Good for even higher dps but very short range.

## Prefab Components
* **Swords** *Object*
	[1]: Transform (Standard transform compnent on all Unity GameObjects)
	[2]: Sprite Renderer (Renders the sprite for the main tower)
	[3]: Box Collider 2D (Summon Collider)
	[4]: Rigidbody 2D (Summon Rigidbody)
	[5]: NavMeshModifier (allows summon to take up a space in the NavMesh for enemy navigation)
	[6]: Swords (Script)
		* **RotAnchor** *Object*
 			[1]: Sword1
			[2]: Sword2
			[3]: Sword3
			[4]: Sword4

			* Where each *Sword* has the components…
				[1]: Transform
				[2]: Sprite Rendere
				[3]: Box Collider 2D
				[4]: Rigidbody 2D
				[5]: Sword (Script);

##