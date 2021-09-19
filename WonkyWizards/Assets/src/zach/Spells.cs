/**********************************************
| Spells V1.0.0                               |
| Author: Zach Heimbigner, T3                 |
| Description: This program manages the spells|              
| of the game                                 |
| Bugs:                                       |
| - the projectiles dont cast in the correct  |
|   direction, check cast()                   |
**********************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{

    public GameObject projectile;
    public Transform firePoint;
    public float speed = 10f;
    public float DPS;
    public float FIRE_RATE;
    public float RANGE;
    public float CHARGE_TIME;
    public float HITBOX;
    public float COOL_DOWN;

    public void cast(){
        GameObject p = Instantiate(projectile, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = p.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * speed, ForceMode2D.Impulse);
    }

    public void on_hit(){

    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            cast();
        }
    }
}


public class Fireball: Spells 
{

}
