/*
 *	Shrubbery from the Realm of NI!
		- Cost: 60
		- Travel time: very fast
		- Damage per hit: low
		- Firing speed: slow - medium
		- Range: medium
		- Hitbox size / shape: circle
		- HP: 
		- Effect: A giant plant that shoots out slimeballs. Enemies hit with a slimeball from this plant get slowed down temporarily.
					(I'm imagining that this looks kinda like the peashooter from plants vs zombies)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrubbery : Summon
{
	protected static int cost = 60;

	public override void FixedUpdate()
	{

	}

	public override int getCost() { return cost; }
}
