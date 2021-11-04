/*
 *	Empowering Crystal
		- Cost: 70
		- Travel time: none
		- Damage per hit: none
		- Firing speed: none
		- Range: short
		- Hitbox size / shape: none
		- HP: 
		- Effect: This summon increases the firing speed of all other summons within range by a percentage amount
					good support
					could look like a giant floating crystal that pulses energy
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Summon
{
	protected static int cost = 70;

	public override void FixedUpdate()
	{

	}

	public override int getCost() { return cost; }
}
