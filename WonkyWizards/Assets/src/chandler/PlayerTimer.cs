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
    private static float fireballReset = 0.75f;
    private static float fireballTimer;
    // magic missile cooldown
    private static float magicMissileReset = 1.1f;
    private static float magicMissileTimer;
    // ice beam cooldown
    private static float iceBeamReset = 0.02f;
    private static float iceBeamTimer;
    // acid splash cooldown
    private static float acidSplashReset = 0.75f;
    private static float acidSplashTimer;
    // slimeball cooldown
    private static float slimeballReset = 0.65f;
    private static float slimeballTimer;

    // Start is called before the first frame update
    void Start()
    {
        dashTimer = dashReset;
        damageTimer = damageReset;
        fireballTimer = fireballReset;
        magicMissileTimer = magicMissileReset;
        iceBeamTimer = iceBeamReset;
        acidSplashTimer = acidSplashReset;
        slimeballTimer = slimeballReset;
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

        // spells
        if (fireballTimer < fireballReset)
        {
            fireballTimer += Time.fixedDeltaTime;
        }

        if (magicMissileTimer < magicMissileReset)
        {
            magicMissileTimer += Time.fixedDeltaTime;
        }

        if (iceBeamTimer < iceBeamReset)
        {
            iceBeamTimer += Time.fixedDeltaTime;
        }

        if (acidSplashTimer < acidSplashReset)
        {
            acidSplashTimer += Time.fixedDeltaTime;
        }

        if (slimeballTimer < slimeballReset)
        {
            slimeballTimer += Time.fixedDeltaTime;
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

    public static bool canCast(int index)
    {
        switch (index)
        {
            case 0:
                return fireballTimer >= fireballReset;
            case 1:
                return magicMissileTimer >= magicMissileReset;
            case 2:
                return iceBeamTimer >= iceBeamReset;
            case 3:
                return acidSplashTimer >= acidSplashReset;
            case 4:
                return slimeballTimer >= slimeballReset;
            default:
                return false;
        }
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

    public static void activateSpellCooldown(int index)
    {
        switch (index)
        {
            case 0:
                fireballTimer = 0.0f;
                break;
            case 1:
                magicMissileTimer = 0.0f;
                break;
            case 2:
                iceBeamTimer = 0.0f;
                break;
            case 3:
                acidSplashTimer = 0.0f;
                break;
            case 4:
                slimeballTimer = 0.0f;
                break;
            default:
                return;
        }
    }
}