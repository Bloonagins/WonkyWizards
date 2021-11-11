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
	protected static int cost = 50;

	private bool linked;
	private GameObject otherPortal;

	private bool noTele;

    public override void Start()
    {
		// not teleporting by default
		noTele = false;

		// assume that no "first" portal exists
		linked = false;

		// attempt to find first portal
		otherPortal = GameObject.Find("First Portal");

		if (otherPortal)
        {
			// if found, mark that we are now linked
			linked = true;
			otherPortal.GetComponent<Portal>().setLinked(true);

			// and give the other portal a reference to this one
			otherPortal.GetComponent<Portal>().link(this.gameObject);
		}
		else
        {
			// no first portal found, so this must be the first portal
			transform.name = "First Portal";
        }
    }

    public override void FixedUpdate()
	{

	}

    public void OnCollisionEnter2D(Collision2D col)
    {
		// check for player tag, and that the portal is linked
        if (col.transform.tag == "Player" && linked && !noTele)
        {
			// set noTele for other portal
			otherPortal.GetComponent<Portal>().setNoTele(true);

			// teleport player
			col.transform.position = otherPortal.transform.position;
        }
    }

    public void OnCollisionExit2D(Collision2D col)
	{
		// check for player tag
		if (col.transform.tag == "Player")
        {
			// player just left, reactivate tele
			noTele = false;
        }

	}

    public override void deleteSummon(int x, int y)
    {
		if (linked)
        {
			// tell the other portal that we aren't linked now
			otherPortal.GetComponent<Portal>().setLinked(false);

			// if the other portal wasn't the first portal
			if (otherPortal.transform.name == "Portal(Clone)")
            {
				// ... then it now should be (it's the only portal)
				otherPortal.transform.name = "First Portal";
            }
        }

        base.deleteSummon(x, y);
    }

    public void setLinked(bool l)
    {
		linked = l;
    }

	public void link(GameObject other)
    {
		otherPortal = other;
    }

	public void setNoTele(bool t)
	{
		noTele = t;
	}
	public bool getNoTele()
	{
		return noTele;
	}

	public override int getCost() { return cost; }
}
