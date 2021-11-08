using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradedFB: FireBall
{   
    private readonly FireBall upgradedfb;

    public UpgradedFB(FireBall fireball)
    {
        this.upgradedfb = fireball;
        DAMAGE = 90;
    }

    void Awake()
    {
        Cast();
    }

    //can now travel through enemies
    public override bool Collision_behaviour(Collider2D collision)
    {
        if(collision.gameObject.tag !="Player" && collision.gameObject.tag != "Spell" && collision.gameObject.tag != "Zone" && collision.gameObject.tag != "SummonProjectile" && collision.gameObject.tag != "SummonNoPlace"&& collision.gameObject.tag != "Enemy" )
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    //chains fireballs
    public override void Explode()
    {
        GameObject effect = Instantiate(projectileEffect, projectile.transform.position, projectile.transform.rotation);
        Destroy(effect,1);
        Instantiate(projectile, projectile.transform.position, projectile.transform.rotation);
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