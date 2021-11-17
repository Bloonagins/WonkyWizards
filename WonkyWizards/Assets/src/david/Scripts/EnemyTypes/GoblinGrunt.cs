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
using UnityEngine.AI;

public class GoblinGrunt : MonoBehaviour
{
    private Rigidbody2D rb; // Used to store NavMeshAgent Component
    private NavMeshAgent agent; // Used to store NavMeshAgent Component

    //----------------------- Private Data Class Pattern -----------------------
    /* How?
        Step 1: Create data class. Move to data class all atributes that need hiding.
        Step 2: Create in main class instance of data class.
        Step 3: Main class must initialize data class through the data class's contstructor.
        Step 4: Expose each attribute of data class using a getter.
        Step 5: Expose each attribute that will change in further through a setter.
    */
    private GoblinGruntData goblinGruntData; // Private instance of data class

    // Constructor for GoblinGrunt
    public GoblinGrunt()
    {
        // Set initial values of goblinGruntData class
        this.goblinGruntData = new GoblinGruntData(200, 200, 30, 16f, 6f, 22f, 1.5f, 1.5f, 300f, 20f, 1f, false);
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
        //Debug.Log("Collided");
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
                ChangeMoveSpeed(other.GetComponent<IceBeam>().Freeze() * -1); // Recieve slow 
            }
            else if(other.GetComponent<AcidSpray>()) { // Check if spell was AcidSpray
                //Debug.Log("Collided Acid Spray");
                RecieveDamage(other.GetComponent<AcidSpray>().getSpellDamage()); // Recieve damage 
                rb.AddForce((other.transform.position - transform.position) * other.GetComponent<AcidSpray>().getSpellKnockBack() * -1.0f, ForceMode2D.Impulse); // FireBall.getKnockback();
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
    public void ChangeMoveSpeed(float s) {
        if (agent.speed >= this.goblinGruntData.GetLowestSpeed() && s < 0) { // decrease
            agent.speed += s;
            agent.acceleration += s;
        }
        else if (agent.speed <= this.goblinGruntData.GetHighestSpeed() && s > 0) { // increase
            agent.speed += s;
            agent.acceleration += s;
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
    public float GetStoppingDistance()
    {
        return this.goblinGruntData.GetStoppingDistance();
    }
}
