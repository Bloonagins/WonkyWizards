/*
 *	Big Chunk
		- Cost: 100
		- Travel time: Very fast
		- Damage per hit: high
		- Firing speed: medium
		- Range: long
		- Hitbox size / shape: big circular stones
		- HP: 
		- Effect: Big, fat troll that hurls stones at enemies that come within range
					just overall good damage and range
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : Summon
{
	protected static int cost = 100;

	public override void FixedUpdate()
	{

	}

	public override int getCost() { return cost; }
}
