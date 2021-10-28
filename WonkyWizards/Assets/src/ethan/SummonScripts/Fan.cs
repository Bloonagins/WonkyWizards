/*
 *	Mysterious Futuristic Wind Manipulation Device
		- Cost: 90
		- Travel time: none
		- Damage per hit: none
		- Firing speed: constant
		- Range: short
		- Hitbox size / shape: a 1 tile wide rectangle that has a length of a few tiles
		- HP: 
		- Effect: A fan that blows in 1 direction, slowing down enemies and players that walk upwind, speeding up enemies and players that walk downwind
					Player can rotate the fan when placing it down
					Useful for holding enemies in a certain area for a longer period of time
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : Summon
{
	protected static int cost = 90;

	public void FixedUpdate()
	{

	}

	public override int getCost() { return cost; }
}
