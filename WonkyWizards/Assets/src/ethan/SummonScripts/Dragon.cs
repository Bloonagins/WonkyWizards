/*
 *	The Dragon of the West
		- Cost: 50
		- Travel time: hitscan
		- Damage per hit: low
		- Firing speed: very fast (continuous)
		- Range: short - medium
		- Hitbox size / shape: cone that shoots out from the dragon
		- HP: 
		- Effect: A dragon that breathes fire at enemies that come within range.
					good for short range high dps
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Summon
{
	protected static int cost = 50;

    public override void Start()
    {
        base.Start();

		cooldown = 0.1f;
		summonRadar.setRadius(2.0f);
	}

	public override int getCost() { return cost; }
}
