/**********************************************
| FireBall V1.0.0                             |
| Author: Zach Heimbigner, T3                 |
| Description: This program manages the       |              
| fireball spell and is attached to a         |
| prefab, is is instatianted by player        |
| Bugs:                                       |
**********************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall: Spells
{   
    public float radius = 3f;

    public FireBall()
    {
        speed = 20.0f;
        DAMAGE = 70;
        COOL_DOWN = 0.75f;
        KNOCK_BACK = 200.0f;

        if(checkUpgrades() != 0)
        {
            applyUpgrades();
        }
    }

    //-----------Firing-------------
    void Awake()
    {
        var player = GameObject.FindWithTag("Player");
        firePoint = player.transform; //get player position/rotation
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * this.speed, ForceMode2D.Impulse);
    }
    //-----------Upgrades-------------
    public void applyUpgrades()
    {
        int current = checkUpgrades();
        switch(current){
            case 1:
                DAMAGE += 10;
                break;
            case 2:
                DAMAGE += 20;
                break; 
            case 3: 
                DAMAGE += 30;
                break;
            default:
                Debug.Log("Action cannot be performed");
                break;
        }
    }

    //-----------Behaviour-------------
    void Explode()
    {
        GameObject effect = Instantiate(projectileEffect, projectile.transform.position, projectile.transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyObject in colliders)
        {
            Debug.Log("object: " + nearbyObject);
        }
        Destroy(effect,1);
    }

    //-----------Collisions-------------
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision: " + collision);
        if(collision.gameObject.tag !="Player" && collision.gameObject.tag != "Spell")
        {
            Explode();
            Destroy(projectile);
        }
    }
}
