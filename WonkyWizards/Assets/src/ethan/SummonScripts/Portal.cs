/*
 *	Portal
		- Cost: 50 (per portal)
		- Travel time: none
		- Damage per hit: none
		- Firing speed: none
		- Range: none
		- Hitbox size / shape: none
		- HP: 
		- Effect: It is a tile that only the player can enter, and when the player touches the center of the tile then they are teleported to the center of another portal tile.
					The player cannot shoot through the portal tile though. This makes it so that players can't just sit inside a portal tile to attack enemies while enemies can't attack back.
					Enemies should not be able to target players inside of a portal tile either.
						Otherwise, enemies could lock onto a player sitting inside a portal block as they pass by, making them press up against the wall forever until they die.
						Or, a player that is overwhelmed with enemies near the target could get all of the enemies to lock onto them, and then take a portal back to the start, drawing them all back.
					Portals can look like a small room with magic force fields as walls and a bright, spiraling pit in the center
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Summon
{
	protected static int cost = 0;

	public void FixedUpdate()
	{

	}

	public override int getCost() { return cost; }
}
