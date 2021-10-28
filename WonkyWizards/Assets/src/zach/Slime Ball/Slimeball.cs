using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slimeball : Spells
{
    private bool bounced = false;
    public Slimeball()
    {
        speed = 20.0f;
        DAMAGE = 80;
        COOL_DOWN = 1.1f;
        KNOCK_BACK = 100.0f;
    }
    void Start()
    {
        Cast();
    }

    void Bounce()
    {
        speed *= -1;
        bounced = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(Collision_behaviour(collision) && bounced == true)
        {
            Destroy(projectile);
        }
        if(Collision_behaviour(collision))
        {
            Bounce();
        }
    }
}
