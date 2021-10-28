/**********************************************
| GoblinBerserker V1.0.0                      |
| Author: David Bush, T5                      |
| Description: This is the GoblinGiant class  |
| that inherits from the Enemy superclass.    |
| This will contain all variables and methods |
| for the GoblinGiant type. The GoblinGiant is|
| a large enemy that has the unique ability to|
| explode on death, damaging the player.      | 
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
        }
        else if(other.tag == "SummonProjectile") {
            if(other.GetComponent<Sword>()) {
                //Debug.Log("COLLIDED WITH SWORD");
                RecieveDamage(other.GetComponent<Sword>().getProjDamage()); // Recieve damage
                //rb.AddForce((other.transform.position - transform.position) * other.GetComponent<Sword>().getProjKnockback() * -1.0f, ForceMode2D.Impulse); // Apply Knockback;
            }
            /*else if(other.GetComponent<DragonProj>()) { // Check if collided with Dragon projectile
                Debug.Log("COLLIDED Dragon");
                RecieveDamage(other.GetComponent<DragonProj>().getProjDamage()); // Recieve damage
            }
            else if(other.GetComponent<CrossbowProj>()) { // Check if collided with Crossbow projectile
                Debug.Log("COLLIDED Crosbow");
                RecieveDamage(other.GetComponent<CrossbowProj>().getProjDamage()); // Recieve damage
            }
            else if(other.GetComponent<ShrubberyProj>()) { // Check if collided with Shrubbery projectile
                Debug.Log("COLLIDED Shrubbery");
                RecieveDamage(other.GetComponent<ShrubberyProj>().getProjDamage()); // Recieve damage
            }
            else if(other.GetComponent<SvenProj>()) { // Check if collided with Sven projectile
                Debug.Log("COLLIDED Sven");
                RecieveDamage(other.GetComponent<SvenProj>().getProjDamage()); // Recieve damage
            }
            else if(other.GetComponent<ChunkProj>()) { // Check if collided with Chunk projectile
                Debug.Log("COLLIDED Chunk");
                RecieveDamage(other.GetComponent<ChunkProj>().getProjDamage()); // Recieve damage
            }
            */
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
    // Function to change the enemy's damage by a flat amount
    public void ChangeDamage(int damage_amount){
        damage += damage_amount; // can be positive or negative
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
    public float GetTargetDistance() 
    {
        return targetDistance;
    }   
    public float GetDeathDamage() 
    {
        return death_damage;
    }
}
