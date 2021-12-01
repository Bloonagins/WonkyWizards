/**********************************************
* Boss_superClass V1.0.0                      *
* Author: Chase Gornick, T4                   *
* Description: The Boss_superClass will       *
* be the class in which each boss will inherit*
* base values from such as stats              *
**********************************************/
using System.Collections;
using UnityEngine;

// This is the superclass for the Enemies
public class BossSuperClass : MonoBehaviour
{
    //----------------------- Variables -----------------------
    protected int maxHealth; // Maximum ammount of health enemy has
    protected int health; // Ammount of health enemy starts with
    protected int damage; // Ammount of damage enemy starts with
    protected int maximumDamage; // Max amount of damage an enemy can have
    protected int minimumDamage; // Min amount of damage an enemy can have
    protected float moveSpeed; // The movement speed the enemy starts with
    protected float minimumSpeed; // The lowest movement speed the enemy has
    protected float maximumSpeed; // The highest movement speed the enemy has
    protected float attackSpeed; // The attack speed the enemy starts with
    protected float attackTimer; // Keeps track of when enemy can attack
    protected float targetDistance; // The distance the enemy starts targeting the player
    protected float stoppingDistance; // The distance the enemy stops moving towards the player
    protected bool attackConnected; // Keeps attack if attack was successful
    protected float knockBack; // Ammount of knockback applied

    //----------------------- Constructor -----------------------
    public BossSuperClass()
    {
        maxHealth = health = 1200;
        damage = minimumDamage = 10;
        maximumDamage = 15;
        moveSpeed = 1f;
        minimumSpeed = 6f;
        maximumSpeed = 20f;
        attackSpeed = attackTimer = 1f;
        targetDistance = 20f;
        stoppingDistance = 1f;
        attackConnected = false;
        knockBack = 200f;
    }

    // Virtual method can be overridden by subclass
    public virtual void Message()
    {
        Debug.Log("Boss Instantiated");
    }

    // Method to update health when enemy is dealt damage
    public void DamageRecieved(int damageRecieved)
    {
        if (damageRecieved > 0)
        {
            health -= damageRecieved; //The Great Gob can take damage
        }

        if (health < 0)
        { //Checks the health to make sure it isn't 0
            health = 0;
        }
    }

    // Damage is changed base on how and where the enemy is hit
    public void ChangeDamage(int amountOfDamage)
    {
        damage += amountOfDamage;
        if (damage > maximumDamage)
        {
            damage = maximumDamage;
        }
        if (damage < minimumDamage)
        {
            damage = minimumDamage;
        }
    }

    // The Great Gob can regain health overtime
    public void gainHealth(int gainedHealth)
    {
        if (gainedHealth > 0)
        {
            health += gainedHealth; // The Great Gob gains health
        }
        if (health < 0)
        {
            health = 0;
        }
        if (health > GetMaxHealth())
        { //Checks if health is above maxHealth
            health = GetMaxHealth(); //if it is it will set it back to its max health
        }
    }
    // Checks if The Great Gob hit the player or goal
    public void SetAttack(bool success)
    {
        attackConnected = success;
    }

    // Gets the methods below
    public int GetHealth()
    {
        return health;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public int GetDamage()
    {
        return damage;
    }
    public int GetMaxDamage()
    {
        return maximumDamage;
    }
    public int GetMinDamage()
    {
        return minimumDamage;
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    public float GetAttackSpeed()
    {
        return attackSpeed;
    }
    public float GetKnockBack()
    {
        return knockBack;
    }
    public float GetAttackTimer()
    {
        return attackTimer;
    }
    public bool canAttack()
    {
        return attackTimer >= attackSpeed;
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

