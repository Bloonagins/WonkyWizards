using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrubberySlimeball : SummonProj
{
    private float slowness;
    private bool stopped;

    public ShrubberySlimeball()
    {
        speed = 2f;
        damage = 25;
        kockback = 0f;

        slowness = .2f;
        stopped = false;
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        // deal damage as usuall
        base.OnTriggerEnter2D(col);
        
        // store the gameObject of the found collision
        GameObject other = col.gameObject;

        // check that the gameObject is an Enemy
        if (col.gameObject.tag == "Enemy")
        {
            // additionally, deal slowness (for whichever type of enemy is present)
            if (other.GetComponent<GoblinGrunt>())
            {
                // enemy is normal. assign a local var for it
                GoblinGrunt enemy = other.GetComponent<GoblinGrunt>();

                enemy.ChangeMoveSpeed(-slowness);
            }
            else if (other.GetComponent<GoblinWarrior>())
            {
                // enemy is normal. assign a local var for it
                GoblinWarrior enemy = other.GetComponent<GoblinWarrior>();

                enemy.ChangeMoveSpeed(-slowness);
            }
            else if (other.GetComponent<GoblinBerserker>())
            {
                // enemy is normal. assign a local var for it
                GoblinBerserker enemy = other.GetComponent<GoblinBerserker>();

                enemy.ChangeMoveSpeed(-slowness);
            }
            else if (other.GetComponent<GoblinAssassin>())
            {
                // enemy is normal. assign a local var for it
                GoblinAssassin enemy = other.GetComponent<GoblinAssassin>();

                enemy.ChangeMoveSpeed(-slowness);
            }
            else if (other.GetComponent<GoblinGiant>())
            {
                // enemy is normal. assign a local var for it
                GoblinGiant enemy = other.GetComponent<GoblinGiant>();

                enemy.ChangeMoveSpeed(-slowness);
            }
            else if (other.GetComponent<GoblinArcher>())
            {
                // enemy is normal. assign a local var for it
                GoblinArcher enemy = other.GetComponent<GoblinArcher>();

                enemy.ChangeMoveSpeed(-slowness);
            }

            stopped = true;

            // delete the slimeball soon after collision
            Invoke("killSelf", 0.25f);
        }
    }

    public override void FixedUpdate()
    {
        // check that a target exsists, and that we aren't stopped
        if (target && !stopped)
        {
            // rotate toward target
            lookAt2D(target.transform);

            // move toward target
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
        }
    }
}
