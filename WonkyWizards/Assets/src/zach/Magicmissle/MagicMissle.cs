using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissle : Spells
{
    //stores the value of the players position/rotation
    private Transform firePoint;
    //the desired prefab to cast
    public GameObject projectile;
    public GameObject projectileEffect;

    public MagicMissle()
    {
        speed = 20.0f;
        DAMAGE = 80;
        COOL_DOWN = 1f;
    }

    //point in the direction of the player and fire
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

    void Explode()
    {
        GameObject Effect = Instantiate(projectileEffect, projectile.transform.position, projectile.transform.rotation);
        //Damage enemy
        Destroy(Effect,1);
    }

    //detect collision between anything that is collidable
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision: " + collision);
        if(collision.gameObject.tag !="Player" && collision.gameObject.tag != "Spell")
        {
            Explode();
        }

        if(collision.gameObject.tag =="Summon" || collision.gameObject.tag =="Wall")
        {
            Destroy(projectile);
        }
    }
}
