/**********************************************
| Enemy V1.0.0                                |
| Author: David Bush, T5                      |
| Description: This is the Enemy super class  |
| that each enemy type will inherit from. It  |
| will contain the stats, and methods shared  |
| between each subclass.                      |
| Bugs:                                       |
**********************************************/
using System.Collections;
using UnityEngine;

// This is the superclass for the Enemies
public class Enemy : MonoBehaviour
{
    // Variables
    protected int max_health; // Maximum ammount of health enemy has
    protected int health; // Ammount of health enemy starts with
    protected int damage; // Ammount of damage enemy starts with
    protected float move_speed; // The movement speed the enemy starts with

    protected float lowest_speed; // The lowest movement speed the enemy has

    protected float highest_speed; // The highest movement speed the enemy has

    protected float attack_speed; // The attack speed the enemy starts with

    protected float attackTimer; // Keeps track of when enemy can attack

    protected float targetDistance; // The distance the enemy starts targeting the player


    protected bool attackConnected; // Keeps attack if attack was successful

    protected float knock_back; // Ammount of knockback applied

    // Basic constructor for Enemy class
    public Enemy()
    {
        max_health = health = 100;
        damage = 10;
        move_speed = 10;
        attack_speed = 1f;
        knock_back = 200f;
        targetDistance = 20f;
    }

    // Method to update health when enemy is dealt damage
    public void RecieveDamage(int damage_recieved)
    {
        health -= damage_recieved; // take away health from eneny

        if(health < 0) { // Check if health is below 0
            health = 0; // set to 0
        }
    }
    // Method that gives health to enemy
    public void AddHealth(int health_recieved)
    {
        health += health_recieved; // add health to enemy

        if(health > max_health) { // Check if health is above max
            health = max_health; // set to max
        }
    }
    // Function to confirm attack was sucessful
    public void SetAttack(bool success)
    {
        attackConnected = success;
    }
    
    // Methods for Getting Stats
    public int GetHealth() 
    {
        return health;
    }
    public int GetMaxHealth()
    {
        return max_health;
    }
    public int GetDamage()
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
    public float GetKnockBack()
    {
        return knock_back;
    }
    public float GetAttackTimer()
    {
        return attackTimer;
    }
    public float GetTargetDistance() 
    {
        return targetDistance;
    }

}
