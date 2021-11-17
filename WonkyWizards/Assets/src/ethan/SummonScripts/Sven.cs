/*
 *	The Sven
		- Cost: 50
		- Travel time: very fast
		- Damage per hit: high to medium (distance from center of explosion determines damage)
		- Firing speed: medium
		- Range: medium
		- Hitbox size / shape: circle
		- HP: 
		- Effect: A snowman that shoots giant snowballs at enemies. When a snowball collides with something, it explodes into ice shards. Enemies within the snowball's explosion radius take damage.
					the amount of damage enemies take from the snowball is determined by their distance from the center of the explosion.
 */

public class Sven : Summon
{
	protected static int cost = 50;

	public override void Start()
	{
		base.Start();

		cooldown = 0.65f;
		summonRadar.setRadius(2.0f);
	}

	public override int getCost() { return cost; }
}
