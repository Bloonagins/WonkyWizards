using System.Collections;
using UnityEngine;

public class GoblinGrunt : Enemy
{
    // Constructor for GoblinGrunt
    public GoblinGrunt()
    {
        health = 200;
        damage = 10;
        move_speed = 5;
        attack_speed = 1.5f;
    }

    // Method to update when enemy is dealt damage
    void UpdateHealth(int damage_recieved)
    {
        health = health - damage_recieved;
    }

    // Methods for retrieving stats
    public float GetHealth()
    {
        return health;
    }
    public float GetDamage()
    {
        return damage;
    }
    public float GetMoveSpeed()
    {
        return move_speed;
    }
    public float GetAttackSpeed()
    {
        return attack_speed;
    }
    
    // Update
    //OnCollision2D
    //OnCollisionEnter2D
    //instiatiate 
}
