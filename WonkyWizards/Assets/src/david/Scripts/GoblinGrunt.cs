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
| Bugs: First collision doesn't register      |
**********************************************/
//To get Enemies damage
// GoblinGrunt goblinGrunt;
// goblinGrunt = FindObjectOfType<GoblinGrunt>();
// goblinGrunt.GetDamage();

using System.Collections;
using UnityEngine;

public class GoblinGrunt : Enemy
{
    // Used to store RigidBody2d Component
    private Rigidbody2D rb;
    private CircleCollider2D enemy_collider;
    private CircleCollider2D goal_collider;

    // Constructor for GoblinGrunt
    public GoblinGrunt()
    {
        max_health = 200;
        health = 200;
        damage = 30;
        move_speed = 10f;
        attack_speed = attackTimer = 1.5f;
        attackConnected = false;
        knock_back = 300f;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // Gets the Rigid Body component
        rb = GetComponent<Rigidbody2D>();
        enemy_collider = gameObject.GetComponent<CircleCollider2D>();
        //Debug.Log("Bounds of Enemy: "+enemy_collider.bounds);
        goal_collider = GameObject.FindGameObjectWithTag("Goal").GetComponent<CircleCollider2D>();
        //Debug.Log("Bounds of Goal: "+goal_collider.bounds);
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

        if(enemy_collider.bounds.Intersects(goal_collider.bounds)) {
            Debug.Log("Bounds violated");
        }

    }
    // Function that checks for collisions
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if(collision.gameObject.tag == "Spell") { // Check if enemy collided with spell
            if(other.GetComponent<FireBall>()) { // Check if spell was Fireball
                RecieveDamage(other.GetComponent<FireBall>().getSpellDamage()); // Recieve damage 
                rb.AddForce((other.transform.position - transform.position) * 200f * -1.0f, ForceMode2D.Impulse); // FireBall.getKnockback();
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if(collision.gameObject.tag == "Goal") { // Checks if collided with Goal
            rb.AddForce((other.transform.position - transform.position) * 50f * -1.0f, ForceMode2D.Impulse);
            if (attackConnected) { // Make sure attack is available and attack is successful
                attackTimer = 0.0f; // Reset timer
                attackConnected = false; // Reset attack 
                Debug.Log("Attack");
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
    public bool IsOnGoal(Vector3 goalPosition)
    {
        return GetPosition() == goalPosition;
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
