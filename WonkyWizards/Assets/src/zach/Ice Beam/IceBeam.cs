using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBeam : Spells
{
    //stores the value of the players position/rotation
    private Transform firePoint;
    //the desired prefab to cast
    public GameObject projectile;
    public GameObject projectileEffect;
    private int fire_radius;

    public IceBeam()
    {
        speed = 50.0f;
        DAMAGE = 0;
        COOL_DOWN = 0f;
        fire_radius = 10;
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

    void Freeze()
    {
        GameObject effect = Instantiate(projectileEffect, projectile.transform.position, projectile.transform.rotation);
        //slow enemy
        Destroy(effect,1);
    }

    //detect collision between anything that is collidable
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
