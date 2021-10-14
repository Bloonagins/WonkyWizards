using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissle : Spells
{
    public MagicMissle()
    {
        speed = 20.0f;
        DAMAGE = 80;
        COOL_DOWN = 1.1f;
        KNOCK_BACK = 100.0f;
    }

    //-----------Firing-------------
    void Awake()
    {
        var player = GameObject.FindWithTag("Player");
        firePoint = player.transform; //get player position/rotation
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * this.speed, ForceMode2D.Impulse);
    }

    public int getSpellDamage()
    {
        return DAMAGE;
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
        Debug.Log("collision: " + collision);
        if(collision.gameObject.tag =="Enemy")
        {
            Explode();
        }
        if(collision.gameObject.tag !="Player" && collision.gameObject.tag !="Spell")
        {
        Destroy(projectile);
        }
    }
}
