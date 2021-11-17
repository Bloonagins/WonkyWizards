using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonProj : MonoBehaviour
{
    protected float speed;
    protected int damage;
    protected float kockback;

    protected Transform target;

    protected GameObject prefab;
    protected GameObject effectPrefab;

    protected Vector3 velocity;

    public virtual void Start()
    {

    }    

    public void setTarget(Transform t)
    {
        target = t;
    }

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
    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        // store the gameObject of the found collision
        GameObject other = col.gameObject;

        // check that the gameObject is an Enemy
        if (other.tag == "Enemy")
        {

            if (other.GetComponent<GoblinGrunt>())
            {
                // enemy is normal. assign a local var for it
                GoblinGrunt enemy = other.GetComponent<GoblinGrunt>();
                
                // apply knockback to enemy
                enemy.GetComponent<Rigidbody2D>().AddForce((transform.position - other.transform.position) * kockback * -1.0f, ForceMode2D.Impulse);

                // make that enemy take damage
                enemy.RecieveDamage(damage);
            }
            else if (other.GetComponent<GoblinWarrior>())
            {
                // enemy is normal. assign a local var for it
                Enemy enemy = other.GetComponent<GoblinWarrior>();

                // apply knockback to enemy
                enemy.GetComponent<Rigidbody2D>().AddForce((transform.position - other.transform.position) * kockback * -1.0f, ForceMode2D.Impulse);

                // make that enemy take damage
                enemy.RecieveDamage(damage);
            }
            else if (other.GetComponent<GoblinBerserker>())
            {
                // enemy is normal. assign a local var for it
                Enemy enemy = other.GetComponent<GoblinBerserker>();

                // apply knockback to enemy
                enemy.GetComponent<Rigidbody2D>().AddForce((transform.position - other.transform.position) * kockback * -1.0f, ForceMode2D.Impulse);

                // make that enemy take damage
                enemy.RecieveDamage(damage);
            }
            else if (other.GetComponent<GoblinAssassin>())
            {
                // enemy is normal. assign a local var for it
                Enemy enemy = other.GetComponent<GoblinAssassin>();

                // apply knockback to enemy
                enemy.GetComponent<Rigidbody2D>().AddForce((transform.position - other.transform.position) * kockback * -1.0f, ForceMode2D.Impulse);

                // make that enemy take damage
                enemy.RecieveDamage(damage);
            }
            else if (other.GetComponent<GoblinGiant>())
            {
                // enemy is normal. assign a local var for it
                Enemy enemy = other.GetComponent<GoblinGiant>();

                // apply knockback to enemy
                enemy.GetComponent<Rigidbody2D>().AddForce((transform.position - other.transform.position) * kockback * -1.0f, ForceMode2D.Impulse);

                // make that enemy take damage
                enemy.RecieveDamage(damage);
            }
            else if (other.GetComponent<GoblinArcher>())
            {
                // enemy is normal. assign a local var for it
                Enemy enemy = other.GetComponent<GoblinArcher>();

                // apply knockback to enemy
                enemy.GetComponent<Rigidbody2D>().AddForce((transform.position - other.transform.position) * kockback * -1.0f, ForceMode2D.Impulse);

                // make that enemy take damage
                enemy.RecieveDamage(damage);
            }
        }
    }

    // by defualt, a projectile advances towards it's target with fixed update
    public virtual void FixedUpdate()
    {
        // check that a target exsists
        if (target)
        {
            // rotate toward target
            lookAt2D(target.transform);

            // move toward target
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
        }
        else
        {
            // no target exists, so delete this projectile
            Destroy(gameObject);
        }
    }

    protected void lookAt2D(Transform target)
    {
        Vector3 vectorToTarget = target.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        transform.rotation = q;
    }
}
