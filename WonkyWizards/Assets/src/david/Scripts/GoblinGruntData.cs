public class GoblinGruntData
{
    // Variables
    private int max_health; // Maximum ammount of health enemy has
    private int health; // Ammount of health enemy starts with
    private int damage; // Ammount of damage enemy starts with
    private float move_speed; // The movement speed the enemy starts with
    private float attack_speed; // The attack speed the enemy starts with
    private float attackTimer; // Keeps track of when enemy can attack
    private float knock_back; // Ammount of knockback applied
    private bool attackConnected; // Keeps attack if attack was successful

    // Constructor for GoblinGruntData
    public GoblinGruntData(int max_health, int health, int damage, float move_speed, float attack_speed, float attackTimer, float knock_back, bool attackConnected)
    {
        this.max_health = max_health;
        this.health = health;
        this.damage = damage;
        this.move_speed = move_speed;
        this.attack_speed = attack_speed;
        this.attackTimer = attackTimer;
        this.knock_back = knock_back;
        this.attackConnected = attackConnected;
    }

    // Methods for setting stats (Setters)
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

    // Methods for retrieving stats (Getters)
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
    public bool GetAttackConnected()
    {
        return this.attackConnected;
    }
}
