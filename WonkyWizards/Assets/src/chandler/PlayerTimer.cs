using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimer : MonoBehaviour
{
    // dash cooldown
    private static float dashReset = 2.0f;
    private static float dashTimer;
    // damage cooldown
    private static float damageReset = 0.5f;
    private static float damageTimer;
    // Spells
    // fireball cooldown
    private static float fireballReset = 0.5f;
    private static float fireballTimer;

    // Start is called before the first frame update
    void Start()
    {
        dashTimer = dashReset;
        damageTimer = damageReset;
        fireballTimer = fireballReset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called at a fixed interval (50 times / second)
    // Increments the timers if they're on a cooldown
    void FixedUpdate()
    {
        if (dashTimer < dashReset)
        {
            dashTimer += Time.fixedDeltaTime;
        }

        if (damageTimer < damageReset)
        {
            damageTimer += Time.fixedDeltaTime;
        }

        if (fireballTimer < fireballReset)
        {
            fireballTimer += Time.fixedDeltaTime;
        }
    }

    // functions that return whether or not certain player actions are on a cooldown or not
    // return true if they are not on a cooldown, returns false if they are still cooling down
    public static bool canDash()
    {
        return dashTimer >= dashReset;
    }

    public static bool canDamage()
    {
        return damageTimer >= damageReset;
    }

    public static bool canCastFireball()
    {
        return fireballTimer >= fireballReset;
    }

    // functions that return the current value of each timer
    public static float getDashTimer()
    {
        return dashTimer;
    }

    public static float getDamageTimer()
    {
        return damageTimer;
    }

    public static float getFireballTimer()
    {
        return fireballTimer;
    }

    // functions that return the cooldown time of each action
    public static float getDashCooldown()
    {
        return dashReset;
    }

    public static float getDamageCooldown()
    {
        return damageReset;
    }

    public static float getFireballCooldown()
    {
        return fireballReset;
    }

    // functions that activate the cooldown of each timer
    public static void activateDashCooldown()
    {
        dashTimer = 0.0f;
    }

    public static void activateDamageCooldown()
    {
        damageTimer = 0.0f;
    }

    public static void activateFireballCooldown()
    {
        fireballTimer = 0.0f;
    }
}
