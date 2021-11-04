/**********************************************
| GoblinGiant V1.0.0                          |
| Author: David Bush, T5                      |
| Description: This is the GoblinGiant class  |
| that inherits from the Enemy superclass.    |
| This will contain all variables and methods |
| for the GoblinGiant type. The GoblinGiant is|
| a large enemy that has the unique ability to|
| explode on death damaging the player.       | 
| Bugs:                                       |
**********************************************/
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GoblinGiant : Enemy
{
    // Used to store RigidBody2d Component
    private Rigidbody2D rb;
    // Used to store Agent component
    private NavMeshAgent agent;
    // Used to store deathExplosion object
    public GameObject deathExplosion;
    // The amount of damage GoblinGiant deals on death
    private int death_damage;

    // Constructor for GoblinGiant
    public GoblinGiant()
    {
        max_health = health = 600;
        damage = 80;
        death_damage = 100;
        move_speed = 14f;
        attack_speed = attackTimer = 3f; // 2000 damage per minute
        targetDistance = 30f;
        attackConnected = false;
        knock_back = 600f;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // Gets the Rigid Body component
        rb = GetComponent<Rigidbody2D>();
        // Gets the Agent component
        agent = GetComponent<NavMeshAgent>();
    }
    // Called at a fixed interval (50 times / second)
    // Increments the timers if they're on a cooldown
    void FixedUpdate()
    {
        if (attackTimer <= attack_speed) {
            attackTimer += Time.fixedDeltaTime;
        }
        //Debug.Log("Current Damage: "+GetDamage());
        //Debug.Log("Current speed: "+agent.acceleration);
    }
    // Update is called once per frame
    void Update()
    {
        // Check if unit has no health left
        if (health <= 0) {
            // SoundManagerScript.PlaySound("enemyDeath");
            DeathExplosion(); // creates explosion
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
                //rb.AddForce((other.transform.position - transform.position) * other.GetComponent<FireBall>().getSpellKnockBack() * -1.0f, ForceMode2D.Impulse); // Apply Knockback;
            }
            else if(other.GetComponent<MagicMissle>()) { // Check if spell was MagicMissle
                //Debug.Log("Collided Magic Missle");
                RecieveDamage(other.GetComponent<MagicMissle>().getSpellDamage()); // Recieve damage 
                //rb.AddForce((other.transform.position - transform.position) * other.GetComponent<MagicMissle>().getSpellKnockBack() * -1.0f, ForceMode2D.Impulse); // Apply Knockback;
            }
            else if(other.GetComponent<IceBeam>()) { // Check if spell was IceBeam
                //Debug.Log("Collided Ice Beam");
                ChangeMoveSpeed(other.GetComponent<IceBeam>().Freeze() * -1); // Recieve slow 
            }
            else if(other.GetComponent<AcidSpray>()) { // Check if spell was AcidSpray
                //Debug.Log("Collided Acid Spray");
                RecieveDamage(other.GetComponent<AcidSpray>().getSpellDamage()); // Recieve damage 
                //rb.AddForce((other.transform.position - transform.position) * other.GetComponent<AcidSpray>().getSpellKnockBack() * -1.0f, ForceMode2D.Impulse); // Apply Knockback;
            }
            else if(other.GetComponent<Slimeball>()) { // Check if spell was SlimeBall
                //Debug.Log("Collided SlimeBall");
                RecieveDamage(other.GetComponent<Slimeball>().getSpellDamage()); //Recieve damage
                rb.AddForce((other.transform.position - transform.position) * other.GetComponent<Slimeball>().getSpellKnockBack() * -1.0f, ForceMode2D.Impulse);
            }
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if(other.tag == "Goal") { // Checks if collided with Goal
            //rb.AddForce((other.transform.position - transform.position) * 50f * -1.0f, ForceMode2D.Impulse);
            if (attackConnected) { // Make sure attack is available and attack is successful
                attackTimer = 0.0f; // Reset timer
                attackConnected = false; // Reset attack 
                //Debug.Log("Attack");
            }
        }
    }

    // Function instantiates an explosion when enemy giant dies
    void DeathExplosion()
    {
        float radius = 1f;
        GameObject explosion = Instantiate(deathExplosion, gameObject.transform.position, gameObject.transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyObject in colliders)
        {
        }
        Destroy(explosion,1);
    }

    // Function to change the enemy's movespeed by a flat amount
    public void ChangeMoveSpeed(float speed_amount) {
        if (agent.speed >= lowest_speed && speed_amount< 0) { 
            agent.speed += speed_amount;
            agent.acceleration += speed_amount;
        }
        else if (agent.speed <= highest_speed && speed_amount > 0) {
            agent.speed += speed_amount;
            agent.acceleration += speed_amount;
        }
    }
    public float GetDeathDamage() 
    {
        return death_damage;
    }
}
