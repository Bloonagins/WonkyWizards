/*
 *	Astral Crossbow
		- Cost: 80
		- Travel time: hitscan
		- Damage per hit: high
		- Firing speed: slow
		- Range: Very long
		- Hitbox size / shape: straight line, basically a very skinny retangle
		- HP: 
		- Effect: A crossbow that fires a magic beam of light at enemies. It can also target enemies through tiles with collision. In other words, it can shoot through walls.
					good for long range
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : Summon
{
	protected static int cost = 80;

	public void FixedUpdate()
	{

	}

	public override int getCost() { return cost; }
}
