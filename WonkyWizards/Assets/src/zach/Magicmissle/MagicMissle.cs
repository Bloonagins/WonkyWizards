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

    //can now pass through enemies
    public override bool Collision_behaviour(Collider2D collision)
    {
        if(collision.gameObject.tag !="Player" && collision.gameObject.tag != "Spell" && collision.gameObject.tag != "Zone" && collision.gameObject.tag != "SummonProjectile" && collision.gameObject.tag != "SummonNoPlace"  && collision.gameObject.tag != "Summon" && collision.gameObject.tag != "Enemy")
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    //-----------Collisions-------------
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(Collision_behaviour(collision))
        {
            Explode();
            Destroy(projectile);
        }
    }
}
