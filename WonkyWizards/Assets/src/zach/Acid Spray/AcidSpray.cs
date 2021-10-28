using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSpray : Spells
{
    public AcidSpray()
    {
        DAMAGE = 70;
        COOL_DOWN = 8f;
    }

    //-----------Firing-------------
    void Awake()
    {
        Cast();
    }
    //-----------Behaviour-------------
    void Acid()
    {
        GameObject effect = Instantiate(projectileEffect, projectile.transform.position, projectile.transform.rotation);
        Destroy(effect,1);
    }

    //-----------Collisions-------------
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(Collision_behaviour(collision))
        {
            Acid();
        }
        Destroy(projectile, 3);
    }
}
