/**********************************************
| GoblinBerserker V1.0.0                      |
| Author: David Bush, T5                      |
| Description: This is the GoblinBerserker    |
| class that inherits from the Enemy          |
| superclass. This will contain all variables |
| and methods associtiated with the           |
| GoblinBerserker enemy type. Each            |
| GoblinBerserker has the unique ability Rage |
| Mode. When it is hit by a spell it's damage |
| and speed increase by a flat ammount each   |
| time.                                       |         
| Bugs:                                       |
**********************************************/
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GoblinBerserker : Enemy
{
    // Used to store RigidBody2d Component
    private Rigidbody2D rb;
    // Used to store Agent component
    private NavMeshAgent agent;

    // Variable used for flat damage boost
    private int damage_boost;
    //
    private int maxDamage;
    // Variable used for flat speed increase
    private float speed_boost;

    // Constructor for GoblinBerserker
    public GoblinBerserker()
    {
        max_health = health = 350;
        damage = 45;
        damage_boost = 5; 
        maxDamage = 65;
        move_speed = 14f;
        lowest_speed = 6f; 
        highest_speed = 22f; 
        speed_boost = 2f;
        attack_speed = attackTimer = 1.75f; // 1900 damage per minute
        targetDistance = 25f;
        stoppingDistance = 1f;
        attackConnected = false;
        knock_back = 350f;
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
            Destroy(gameObject); // Destroy unit
        }
    }

    // Function that checks for collisions
    void OnTriggerEnter2D(Collider2D collision)
    {        
        GameObject other = collision.gameObject;
        if(other.tag == "Spell") { // Check if enemy collided with spell
            // Add damage and speed each time its hit by spell
            if(!other.GetComponent<IceBeam>()) {
                ChangeDamage(damage_boost);
                ChangeMoveSpeed(speed_boost);
            }

            if(other.GetComponent<FireBall>()) { // Check if spell was Fireball
                RecieveDamage(other.GetComponent<FireBall>().getSpellDamage()); // Recieve damage 
                rb.AddForce((other.transform.position - transform.position) * other.GetComponent<FireBall>().getSpellKnockBack() * -1.0f, ForceMode2D.Impulse); // Apply Knockback;
            }
            else if(other.GetComponent<MagicMissle>()) { // Check if spell was MagicMissle
                //Debug.Log("Collided Magic Missle");
                RecieveDamage(other.GetComponent<MagicMissle>().getSpellDamage()); // Recieve damage 
                rb.AddForce((other.transform.position - transform.position) * other.GetComponent<MagicMissle>().getSpellKnockBack() * -1.0f, ForceMode2D.Impulse); // Apply Knockback;
            }
            else if(other.GetComponent<IceBeam>()) { // Check if spell was IceBeam
                //Debug.Log("Collided Ice Beam");
                ChangeMoveSpeed(other.GetComponent<IceBeam>().Freeze() * -1); // Recieve slow 
            }
            else if(other.GetComponent<AcidSpray>()) { // Check if spell was AcidSpray
                //Debug.Log("Collided Acid Spray");
                RecieveDamage(other.GetComponent<AcidSpray>().getSpellDamage()); // Recieve damage 
                rb.AddForce((other.transform.position - transform.position) * other.GetComponent<AcidSpray>().getSpellKnockBack() * -1.0f, ForceMode2D.Impulse); // Apply Knockback;
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

    public int GetDamageBoost()
    {
        return damage_boost;
    }

}
