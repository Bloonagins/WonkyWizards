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

//-------------------------- GoblinBeserker --------------------------
public class GoblinBerserker : Enemy
{
    //----------------------- Variables -----------------------
    private Rigidbody2D rb; // Used to store RigidBody2d Component
    private NavMeshAgent agent; // Used to store NavMeshAgent component
    private int damageBoost; // The flat amount the damage increases
    private float speedBoost; // The flat amount the speed increases 

    //----------------------- Constructor -----------------------
    /* This is where all the attributes are set for the GoblinBeserker
       type. 
    */
    public GoblinBerserker()
    {
        health = 350;
        max_health = 350;
        damage = 45;
        minDamage = 45;
        damageBoost = 5; 
        maxDamage = 65;
        move_speed = 14f;
        lowest_speed = 6f; 
        highest_speed = 22f; 
        speedBoost = 2f;
        attack_speed = 1.75f; // 1900 damage per minute
        attackTimer = 1.75f; 
        targetDistance = 25f;
        stoppingDistance = 1f;
        attackConnected = false;
        knock_back = 350f;
    }

    //----------------------- Start -----------------------
    /* Called before first frame
    */
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Gets the Rigid Body component
        agent = GetComponent<NavMeshAgent>(); // Gets the Agent component
    }

    //----------------------- FixedUpdate -----------------------
    /* Called at a fixed interval (50 times / second).
       Increments the timers if they're on a cooldown.
    */
    void FixedUpdate()
    {
        if (attackTimer <= attack_speed) {
            attackTimer += Time.fixedDeltaTime;
        }
    }

    //----------------------- Update -----------------------
    /* Update is called once per frame.
    */
    void Update()
    {
        if (health <= 0) { // Check if unit has no health left
            Destroy(gameObject); // Destroy unit
        }
    }

    //----------------------- OnTriggerEnter2D -----------------------
    /* Function that checks for when an object collides.
    */
    void OnTriggerEnter2D(Collider2D collision)
    {        
        GameObject other = collision.gameObject;
        if(other.tag == "Spell") { // Check if enemy collided with spell
        
            // Add damage and speed each time its hit by spell
            if(!other.GetComponent<IceBeam>()) {
                ChangeDamage(damageBoost);
                ChangeMoveSpeed(speedBoost);
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

    //----------------------- OnTriggerStay2D -----------------------
    /* Function that checks for if collision is constant
    */
    void OnTriggerStay2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if(other.tag == "Goal") { // Checks if collided with Goal
            if (attackConnected) { // Make sure attack is available and attack is successful
                attackTimer = 0.0f; // Reset timer
                attackConnected = false; // Reset attack 
            }
        }
    }

    //----------------------- ChangeMoveSpeed -----------------------
    /* Function to change the enemy's movespeed by a flat amount
    */
    public void ChangeMoveSpeed(float speed_amount) {
        if (agent.speed >= lowest_speed && speed_amount < 0) { // Check that its in bounds
            agent.speed += speed_amount;
            agent.acceleration += speed_amount;
        }
        else if (agent.speed <= highest_speed && speed_amount > 0) { // Check that its in bounds
            agent.speed += speed_amount;
            agent.acceleration += speed_amount;
        }
    }

    //----------------------- GetDamageBoost -----------------------
    /*  Function to return the damageBoost amount
    */
    public int GetDamageBoost()
    {
        return damageBoost;
    }

    // Overrides the Superclass declaration
    public override void Message() 
    {
        Debug.Log("This is the GoblinBeserker");
    }

    //------------------------ Dynamic/Static Binding ------------------------
    //  Enemy enemy = new Enemy();
    // <static>          <dynamic> 
    // enemy.Message() == "This is the Enemy"

    //  Enemy enemy = new GoblinBeserker();
    // <static>          <dynamic>
    // enemy.Message() == "This is the GoblinBeserker"

}
