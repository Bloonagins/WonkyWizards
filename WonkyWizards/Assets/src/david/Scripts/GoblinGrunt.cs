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

public class GoblinGrunt : MonoBehaviour
{
    // Used to store RigidBody2d Component
    private Rigidbody2D rb;

    // Private Data Class Pattern 
    private GoblinGruntData goblinGruntData; // Stores all GoblinGrunts attributes privately

    // Constructor for GoblinGrunt
    public GoblinGrunt()
    {
        // Set initial values of goblinGruntData class
        this.goblinGruntData = new GoblinGruntData(200, 200, 30, 18f, 1.5f, 1.5f, 300f, 20f, false);
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
        // If attackTimer is less than attackSpeed then increment timer
        if (this.goblinGruntData.GetAttackTimer() <= this.goblinGruntData.GetAttackSpeed()) {
            // Increments time on attack timer
            this.goblinGruntData.SetAttackTimer(this.goblinGruntData.GetAttackTimer()+Time.fixedDeltaTime);
        }
    }
    // Update is called once per frame
    void Update()
    {
        // Check if unit has no health left
        if (this.goblinGruntData.GetHealth() <= 0) {
            // SoundManagerScript.PlaySound("enemyDeath");
            Destroy(gameObject); // Destroy unit
        }
    }
    // Function that checks for collisions
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if(other.tag == "Spell") { // Check if enemy collided with spell
            //Debug.Log("Collided with spell");
            if(other.GetComponent<FireBall>()) { // Check if spell was Fireball
                RecieveDamage(other.GetComponent<FireBall>().getSpellDamage()); // Recieve damage 
                rb.AddForce((other.transform.position - transform.position) * other.GetComponent<FireBall>().getSpellKnockBack() * -1.0f, ForceMode2D.Impulse); // FireBall.getKnockback();
            }
            else if(other.GetComponent<MagicMissle>()) { // Check if spell was MagicMissle
                //Debug.Log("Collided Magic Missle");
                RecieveDamage(other.GetComponent<MagicMissle>().getSpellDamage()); // Recieve damage 
                rb.AddForce((other.transform.position - transform.position) * other.GetComponent<MagicMissle>().getSpellKnockBack() * -1.0f, ForceMode2D.Impulse); // FireBall.getKnockback();
            }
            else if(other.GetComponent<IceBeam>()) { // Check if spell was IceBeam
                //Debug.Log("Collided Ice Beam");
                RecieveDamage(other.GetComponent<IceBeam>().getSpellDamage()); // Recieve damage 
                rb.AddForce((other.transform.position - transform.position) * other.GetComponent<IceBeam>().getSpellKnockBack() * -1.0f, ForceMode2D.Impulse); // FireBall.getKnockback();
            }
            else if(other.GetComponent<AcidSpray>()) { // Check if spell was AcidSpray
                //Debug.Log("Collided Acid Spray");
                RecieveDamage(other.GetComponent<AcidSpray>().getSpellDamage()); // Recieve damage 
                rb.AddForce((other.transform.position - transform.position) * other.GetComponent<AcidSpray>().getSpellKnockBack() * -1.0f, ForceMode2D.Impulse); // FireBall.getKnockback();
            }
        }
        else if(other.tag == "SummonProjectile") {
            if(other.GetComponent<Sword>()) {
                RecieveDamage(other.GetComponent<Sword>().getProjDamage()); // Recieve damage
                //rb.AddForce((other.transform.position - transform.position) * other.GetComponent<Sword>().getProjKnockback() * -1.0f, ForceMode2D.Impulse); // Apply Knockback;
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if(other.tag == "Goal") { // Checks if collided with Goal
            //rb.AddForce((other.transform.position - transform.position) * 50f * -1.0f, ForceMode2D.Impulse);
            if (this.goblinGruntData.GetAttackConnected()) { // Make sure attack is available and attack is successful
                this.goblinGruntData.SetAttackTimer(0.0f); // Reset timer
                this.goblinGruntData.SetAttackConnected(false); // Reset attack 
                //Debug.Log("Attack");
            }
        }
    }

    // Keeps track of when enemy can attack
    public bool canAttack()
    {
        return this.goblinGruntData.GetAttackTimer() >= this.goblinGruntData.GetAttackSpeed();
    }

    // Method to update health when enemy is dealt damage
    public void RecieveDamage(int damage_recieved)
    {
        if(damage_recieved > 0) {
            this.goblinGruntData.SetHealth(this.goblinGruntData.GetHealth()-damage_recieved); // take away health from enemy
        }
        if(this.goblinGruntData.GetHealth() < 0) { // Check if health is below 0
            this.goblinGruntData.SetHealth(0); // set to 0
        }

    }
    // Method that gives health to enemy
    public void AddHealth(int health_recieved)
    {
        if(health_recieved > 0) {
            this.goblinGruntData.SetHealth(this.goblinGruntData.GetHealth()+health_recieved); // add health to enemy
        }
        if(this.goblinGruntData.GetHealth() > this.goblinGruntData.GetMaxHealth()) { // Check if health is above max
            this.goblinGruntData.SetHealth(this.goblinGruntData.GetMaxHealth()); // set to max
        }
    }
    // Function to confirm attack was sucessful
    public void SetAttack(bool success)
    {
        this.goblinGruntData.SetAttackConnected(success);
    }
    // Function to return current position of GoblinGrunt unit
    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }

    // Methods for retrieving stats
    public int GetMaxHealth()
    {
        return this.goblinGruntData.GetMaxHealth();
    }
    public int GetHealth()
    {
        return this.goblinGruntData.GetHealth();
    }
    public int GetDamage()
    {
        return this.goblinGruntData.GetDamage();
    }
    public float GetMoveSpeed()
    {
        return this.goblinGruntData.GetMoveSpeed();
    }
    public float GetAttackSpeed()
    {
        return this.goblinGruntData.GetAttackSpeed();
    }
    public float GetKnockBack()
    {
        return this.goblinGruntData.GetKnockBack();
    }
    public float GetAttackTimer()
    {
        return this.goblinGruntData.GetAttackTimer();
    }
    public float GetTargetDistance() 
    {
        return this.goblinGruntData.GetTargetDistance();
    }
}
