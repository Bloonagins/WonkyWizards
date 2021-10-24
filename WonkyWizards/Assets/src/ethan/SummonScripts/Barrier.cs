/*
 *	Barrier
		- Cost: 10
		- Travel time: None
		- Damage per hit: None
		- Firing speed: None
		- Range: None
		- HP: ∞
		- Effect: only serves to block enemies from reaching their target directly. They can look like part of a castle wall unless someone has a cooler idea.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : Summon
{
	//static string displayName = "Barrier";
	protected static int cost = 10;
	private const int maxHealth = int.MaxValue;

    public override int getCost () { return cost; }
    public override int getMaxHealth () { return maxHealth; }
}