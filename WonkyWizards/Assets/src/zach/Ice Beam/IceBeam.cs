using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBeam : Spells
{
    private int fire_radius;

    public float slow;

    public IceBeam()
    {
        speed = 50.0f;
        slow = .1f;
        DAMAGE = 0;
        COOL_DOWN = 0.02f;
        fire_radius = 10;
    }

    //-----------Firing-------------
    void Awake()
    {
        Cast();
    }

    //-----------Behaviour-------------
    public float Freeze()
    {
        return slow;
    }

    //-----------Collisions-------------
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(Collision_behaviour(collision))
        {
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
