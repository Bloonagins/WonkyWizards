using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissle : Spells
{
    public MagicMissle()
    {
        speed = 100.0f;
        DAMAGE = 80;
        COOL_DOWN = 1.1f;
        KNOCK_BACK = 100.0f;
    }

    //-----------Firing-------------
    void Awake()
    {
        Cast();
    }

    //-----------Behaviour-------------
    void Explode()
    {
        GameObject Effect = Instantiate(projectileEffect, projectile.transform.position, projectile.transform.rotation);
        //Damage enemy
        Destroy(Effect,1);
    }

    //-----------Collisions-------------
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Enemy")
        {
            Explode();
        }
        if(Collision_behaviour(collision))
        {
            Destroy(projectile);
        }
    }
}
