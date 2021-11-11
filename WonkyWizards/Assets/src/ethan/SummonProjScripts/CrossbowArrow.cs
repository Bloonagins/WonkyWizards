using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowArrow : SummonProj
{
    public CrossbowArrow()
    {
        speed = 0.75f;
        damage = 5;
        kockback = 10f;
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        // deal damage as usuall
        base.OnTriggerEnter2D(col);

        // check that the gameObject is an Enemy
        if (col.gameObject.tag == "Enemy")
        {
            // if so also delete the arrow
            killSelf();
        }
    }
}
