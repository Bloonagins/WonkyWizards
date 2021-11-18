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

public class FireBall: Spells, IDamage
{   
    public float radius = 3f;
    private List<SpellModifier> myModifiers = new List<SpellModifier>(); 

    public FireBall()
    {
        speed = 20.0f;
        DAMAGE = 70;
        COOL_DOWN = 0.75f;
        KNOCK_BACK = 200.0f;
        RegisterSpellModifier(new BonusDamage(this, 3));
    }

    //-----------Firing-------------
    void Awake()
    {
        Cast();
    }

    //-----------Behaviour-------------
    public virtual void Explode()
    {
        GameObject effect = Instantiate(projectileEffect, projectile.transform.position, projectile.transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyObject in colliders)
        {
        }
        Destroy(effect,1);
    }

    //-----------Collisions-------------
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(Collision_behaviour(collision))
        {
            Explode();
            Destroy(projectile);
        }
    }

    public void RegisterSpellModifier(SpellModifier newModifier){
        myModifiers.Add(newModifier);
    }

    public void setDamage(int multiplier){
        DAMAGE *= multiplier;
    }

    public void apply(IDamageMaker source){
        for(int i=0; i < myModifiers.Count; i++){
            myModifiers[i].apply(source);
        }
    }
}
