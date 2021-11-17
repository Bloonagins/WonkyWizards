//----------------------- Private Data Class -----------------------
public class GoblinGruntData
{
    /*  Why?
        - Controls access to class atributes
        - Prevents undesirable manipulation of data
        - Separate data from methods that use it
        - Encapsulate class data initialization
        - Protects the class state by minimizing visibility of attributes
    */

    //----------------------- Private Variables -----------------------
    private int max_health; // Maximum ammount of health enemy has
    private int health; // Ammount of health enemy starts with
    private int damage; // Ammount of damage enemy starts with
    private float move_speed; // The movement speed the enemy starts with
    private float lowest_speed; // The lowest movespeed the enemy can have
    private float highest_speed; // The highest movespeed the enemy can have
    private float attack_speed; // The attack speed the enemy starts with
    private float attackTimer; // Keeps track of when enemy can attack
    private float knock_back; // Ammount of knockback applied
    private float targetDistance; // The distance the enemy starts targeting the player
    protected float stoppingDistance; // The distance the enemy stops moving towards the player
    private bool attackConnected; // Keeps attack if attack was successful

    //----------------------- Constructor -----------------------
    public GoblinGruntData(int max_health, int health, int damage, float move_speed, float lowest_speed, float highest_speed, float attack_speed, float attackTimer, float knock_back, float targetDistance, float stoppingDistance, bool attackConnected)
    {
        this.max_health = max_health;
        this.health = health;
        this.damage = damage;
        this.move_speed = move_speed;
        this.lowest_speed = lowest_speed;
        this.highest_speed = highest_speed;
        this.attack_speed = attack_speed;
        this.attackTimer = attackTimer;
        this.knock_back = knock_back;
        this.targetDistance = targetDistance;
        this.stoppingDistance = stoppingDistance;
        this.attackConnected = attackConnected;
    }

    //----------------------- Setters -----------------------
    /*  Methods for setting the attributes.
        Only want to have for attributes that need to be changed.
    */
    public void SetHealth(int health)
    {
        this.health = health;
    }
    public void SetAttackTimer(float attackTimer)
    {
        this.attackTimer = attackTimer;
    }
    public void SetAttackConnected(bool attackConnected)
    {
        this.attackConnected = attackConnected;
    }

    //----------------------- Getters -----------------------
    /*  Methods used to retrieve the attributes.
    */
    public int GetMaxHealth()
    {
        return this.max_health;
    }
    public int GetHealth()
    {
        return this.health;
    }
    public int GetDamage()
    {
        return this.damage;
    }
    public float GetMoveSpeed()
    {
        return this.move_speed;
    }
    public float GetLowestSpeed()
    {
        return this.lowest_speed;
    }    public float GetHighestSpeed()
    {
        return this.highest_speed;
    }
    public float GetAttackSpeed()
    {
        return this.attack_speed;
    }
    public float GetAttackTimer()
    {
        return this.attackTimer;
    }
    public float GetKnockBack()
    {
        return this.knock_back;
    }
    public float GetTargetDistance() 
    {
        return this.targetDistance;
    }
    public float GetStoppingDistance() 
    {
        return this.stoppingDistance;
    }
    public bool GetAttackConnected()
    {
        return this.attackConnected;
    }
}
