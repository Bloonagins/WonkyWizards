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
    //----------------------- Variables -----------------------
    protected int max_health; // Maximum ammount of health enemy has
    protected int health; // Ammount of health enemy starts with
    protected int damage; // Ammount of damage enemy starts with
    protected int maxDamage; // Max amount of damage an enemy can have
    protected int minDamage; // Min amount of damage an enemy can have
    protected float move_speed; // The movement speed the enemy starts with
    protected float lowest_speed; // The lowest movement speed the enemy has
    protected float highest_speed; // The highest movement speed the enemy has
    protected float attack_speed; // The attack speed the enemy starts with
    protected float attackTimer; // Keeps track of when enemy can attack
    protected float targetDistance; // The distance the enemy starts targeting the player
    protected float stoppingDistance; // The distance the enemy stops moving towards the player
    protected bool attackConnected; // Keeps attack if attack was successful
    protected float knock_back; // Ammount of knockback applied

    //----------------------- Constructor -----------------------
    public Enemy()
    {
        max_health = health = 100;
        damage = minDamage = 10;
        maxDamage = 15;
        move_speed = 1f;
        lowest_speed = 6f;
        highest_speed = 20f;
        attack_speed = attackTimer = 1f;
        targetDistance = 20f;
        stoppingDistance = 1f;
        attackConnected = false;
        knock_back = 200f;
    }
    
    // Virtual method can be overridden by subclass
    public virtual void Message() 
    {
        Debug.Log("This is the Enemy");
    }

    // Method to update health when enemy is dealt damage
    public void RecieveDamage(int damage_recieved)
    {
        if(damage_recieved > 0) {
            health -= damage_recieved; // take away health from enemy
        }

        if(health < 0) { // Check if health is below 0
            health = 0; // set to 0
        }
    }

    // Function to change the enemy's damage by a flat amount
    public void ChangeDamage(int damage_amount){
        damage += damage_amount; // can be positive or negative
        if(damage > maxDamage) {
            damage = maxDamage;
        }
        if(damage < minDamage) {
            damage = minDamage;
        }
    }
    
    // Method that gives health to enemy
    public void AddHealth(int health_recieved)
    {
        if(health_recieved > 0) {
            health += health_recieved; // add health to enemy
        }
        if(health < 0) {
            health = 0;
        }
        if(health > GetMaxHealth()) { // Check if health is above max
            health = GetMaxHealth(); // set to max
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
    public int GetMaxDamage() 
    {
        return maxDamage;
    }
    public int GetMinDamage() 
    {
        return minDamage;
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
    public bool canAttack()
    {
        return attackTimer >= attack_speed;
    }
    public float GetTargetDistance() 
    {
        return targetDistance;
    }
    public float GetStoppingDistance()
    {
        return stoppingDistance;
    }

}
