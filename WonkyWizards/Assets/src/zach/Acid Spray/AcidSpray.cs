using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSpray : Spells
{
    //the desired prefab to cast
    public GameObject projectile;
    public GameObject projectileEffect;
    public float radius = 4f;

    public AcidSpray()
    {
        DAMAGE = 70;
        COOL_DOWN = 0.75f;
    }

    //point in the direction of the player and fire
    public int getSpellDamage()
    {
        return DAMAGE;
    }

    void Acid()
    {
        GameObject effect = Instantiate(projectileEffect, projectile.transform.position, projectile.transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyObject in colliders)
        {
            Debug.Log("object: " + nearbyObject);
        }
        Destroy(effect,1);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision: " + collision);
        if(collision.gameObject.tag !="Player" && collision.gameObject.tag != "Spell")
        {
            Acid();
            Destroy(projectile, 7);
        }
    }
}
