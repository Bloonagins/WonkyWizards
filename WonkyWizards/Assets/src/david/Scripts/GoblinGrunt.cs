/**********************************************
| GoblinGrunt V1.0.0                          |
| Author: David Bush, T5                      |
| Description: This is the GoblinGrunt class  |
| that inherits from the Enemy superclass.    |
| This will contain all variables and methods |
| associtiated with the GoblinGrunt enemy     |
| type. This also updates the health for each |
| GoblinGrunt and removes the object if health|
| is zero.                                    |
| Bugs:                                       |
**********************************************/
using System.Collections;
using UnityEngine;

public class GoblinGrunt : Enemy
{
    // Used to store RigidBody2d Component
    private Rigidbody2D rb;

    // Constructor for GoblinGrunt
    public GoblinGrunt()
    {
        max_health = health = 200;
        damage = 30;
        move_speed = 18f;
        attack_speed = attackTimer = 1.5f; // 1200 damage per minute
        attackConnected = false;
        knock_back = 300f;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // Gets the Rigid Body component
        rb = GetComponent<Rigidbody2D>();
    }
    // Called at a fixed interval (50 times / second)
    // Increments the timers if they're on a cooldown
    void FixedUpdate()
    {
        if (attackTimer <= attack_speed) {
            attackTimer += Time.fixedDeltaTime;
        }
    }
    // Update is called once per frame
    void Update()
    {
        // Check if unit has no health left
        if (health <= 0) {
            // SoundManagerScript.PlaySound("enemyDeath");
            Destroy(gameObject); // Destroy unit
        }
    }
    // Function that checks for collisions
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if(other.tag == "Spell") { // Check if enemy collided with spell
            if(other.GetComponent<FireBall>()) { // Check if spell was Fireball
                RecieveDamage(other.GetComponent<FireBall>().getSpellDamage()); // Recieve damage 
                rb.AddForce((other.transform.position - transform.position) * 200f * -1.0f, ForceMode2D.Impulse); // FireBall.getKnockback();
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if(other.tag == "Goal") { // Checks if collided with Goal
            rb.AddForce((other.transform.position - transform.position) * 50f * -1.0f, ForceMode2D.Impulse);
            if (attackConnected) { // Make sure attack is available and attack is successful
                attackTimer = 0.0f; // Reset timer
                attackConnected = false; // Reset attack 
                //Debug.Log("Attack");
            }
        }
    }

    // Keeps track of when enemy can attack
    public bool canAttack()
    {
        return attackTimer >= attack_speed;
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
    // Function to return current position of GoblinGrunt unit
    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }

    // Methods for retrieving stats
    public int GetMaxHealth()
    {
        return max_health;
    }
    public int GetHealth()
    {
        return health;
    }
    public int GetDamage()
    {
        return damage;
    }
    public override float GetMoveSpeed()
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
}
