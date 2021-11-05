using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonProj : MonoBehaviour
{
    protected float speed;
    protected int damage;
    protected float kockback;

    protected GameObject prefab;
    protected GameObject effectPrefab;

    protected Vector3 velocity;

    public int getProjDamage()
    {
        return damage;
    }

    public float getProjKnockback()
    {
        return kockback;
    }

    protected void killSelf()
    {
        Destroy(gameObject);
    }

    // when an enemy enters the collider, we should damage it
    void OnTriggerEnter2D(Collider2D col)
    {
        // store the gameObject of the found collision
        GameObject other = col.gameObject;

        // check that the gameObject is an Enemy
        if (other.tag == "Enemy")
        {
            // see if the object inherents Enemy
            // (or if it is a special case)
            if (other.GetComponent<Enemy>())
            {
                // enemy is normal. assign a local var for it
                Enemy enemy = other.GetComponent<Enemy>();
                
                // apply knockback to enemy
                enemy.GetComponent<Rigidbody2D>().AddForce((transform.position - other.transform.position) * kockback * -1.0f, ForceMode2D.Impulse);

                // make that enemy take damage
                enemy.RecieveDamage(damage);
                //Debug.Log("applying " + damage + "damage");
            }

            // special case: enemy is a GoblinGrunt
            // type (doesn't inherent Enemy)
            else if (other.GetComponent<GoblinGrunt>())
            {

            }
        }
    }
}
