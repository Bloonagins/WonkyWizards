/*
 *	Enchanted Swords
		- Cost: 60
		- Travel time: hitscan
		- Damage per hit: high
		- Firing speed: fast
		- Range: very short
		- Hitbox size / shape: 2 slim swords
		- HP: 
		- Effect: 2 swords spin around very quickly like a rotor. Enemies takes damage each time a sword hits them. Swords magicaly pass through nearby summons and the player without harming them.
					good for even higher dps but even shorter range
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swords : Summon
{
	protected static int cost = 60;
	private Transform rotAnchor;
	private float rotAmount = -3.5f;

	public override void Start()
    {

		rotAnchor = gameObject.GetComponentInChildren<RotAnchor>().transform;
    }

    public override void FixedUpdate()
    {
		rotAnchor.transform.Rotate(0.0f, 0.0f, rotAmount);
	}


	public override int getCost() { return cost; }
}
