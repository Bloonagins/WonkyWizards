using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBeam : Spells
{
    private int fire_radius;

    public IceBeam()
    {
        speed = 50.0f;
        DAMAGE = 80;
        COOL_DOWN = 0.02f;
        fire_radius = 10;
    }

    //-----------Firing-------------
    void Awake()
    {
        Cast();
    }

    //-----------Behaviour-------------
    void Freeze()
    {
        GameObject effect = Instantiate(projectileEffect, projectile.transform.position, projectile.transform.rotation);
        //slow enemy
        Destroy(effect,1);
    }

    //-----------Collisions-------------
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision: " + collision);
        if(collision.gameObject.tag !="Player" && collision.gameObject.tag != "Spell")
        {
            Freeze();
            Destroy(projectile);
        }
    }

    void FixedUpdate()
    {
        if(projectile.transform.position.x > (firePoint.position.x + fire_radius) || projectile.transform.position.x < (firePoint.position.x - fire_radius))
        {
            Destroy(projectile);
        }

        if(projectile.transform.position.y > (firePoint.position.y + fire_radius) || projectile.transform.position.y < (firePoint.position.y - fire_radius))
        {
            Destroy(projectile);
        }
    }
}
