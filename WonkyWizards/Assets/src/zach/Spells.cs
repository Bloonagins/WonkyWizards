/**********************************************
| Spells V1.4.0                               |
| Author: Zach Heimbigner, T3                 |
| Description: This program manages the spells|              
| of the game                                 |
| Bugs:                                       |
**********************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    //spell vars
    protected float speed; //travel speed
    protected int DAMAGE; //damage the spells do
    protected float RANGE; //how far it can travel
    protected float CHARGE_TIME; //pause before actual cast
    protected float COOL_DOWN; //cooldown until next cast
    protected float KNOCK_BACK;
    protected int spell_number;
    protected bool[] UpgradeList;
    protected Transform firePoint; //stores the value of the players position/rotation
    public GameObject projectile;
    public GameObject projectileEffect;

    public int getSpellDamage()
    {
        return DAMAGE;
    }

    public float getSpellKnockBack()
    {
        return KNOCK_BACK;
    }

    public float getSpellCoolDown()
    {
        return COOL_DOWN;
    }

    public void Cast()
    {
        var player = GameObject.FindWithTag("Player");
        firePoint = player.transform; //get player position/rotation
        projectile.transform.rotation = firePoint.rotation; 
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * this.speed, ForceMode2D.Impulse);
    }

    public bool Collision_behaviour(Collider2D collision)
    {
        if(collision.gameObject.tag !="Player" && collision.gameObject.tag != "Spell" && collision.gameObject.tag != "Zone" && collision.gameObject.tag != "SummonProjectile" && collision.gameObject.tag != "SummonNoPlace")
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

}

