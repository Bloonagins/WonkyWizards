/*
 * PlayerScript.cs
 * This script contains all relevant information about the player that other people may need
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerScript : MonoBehaviour
{
    // cursor coordinates
    private static Vector3 screenCursorPoint;
    private static Vector3 worldCursorPoint;
    private static Vector3 gridCursorPoint;
    private static Vector3 arrayCursorPoint;
    // angle between cursor and player
    private static float cursorAngle;
    // player's rigidbody component
    public Rigidbody2D rb;
    // speed of the player
    public float movementspeed;
    // player's health and mana point values
    private static int MAXHP = 1000;
    private static int hp = 1000;
    private static int mana = 100;
    // index number of which item is currently selected in the hotbar
    public static int spellIndex;
    public static int summonIndex;
    // determines whether the player places summons or casts spells with click
    private static bool inBuildMode;

    // called when the game loads up
    void Awake()
    {
        // gets a link to this game object's rigid body component
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        resetPlayerHP();
        spellIndex = 0;
        summonIndex = 0;
    }

    // Updateis called once every frame
    void Update()
    {
        // updates world cursor point
        worldCursorPoint = Camera.main.ScreenToWorldPoint(screenCursorPoint);
        worldCursorPoint.z = 0.0f;
        // updates the cursor angle
        calculateCursorAngle();
        // updates cursor point for what square the cursor is currently in
        gridCursorPoint = Summon.SnapOffset(worldCursorPoint, new Vector3(4,4,0), 8.0f);
        // updates the indexes of the level array that the cursor is currently over
        arrayCursorPoint = (gridCursorPoint - new Vector3(4, -4, 0)) / 8.0f;
        arrayCursorPoint.y *= -1.0f;
    }

    // moves the camera to the middle of the screen when the game is alt-tabbed out
    void OnApplicationPause()
    {
        worldCursorPoint = transform.position;
        screenCursorPoint = Camera.main.WorldToScreenPoint(worldCursorPoint);
    }

    // checks for collisions with enemies and applies damage / knockback
    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Enemy")
        {
            if (PlayerTimer.canDamage())
            {
                if (other.GetComponent<GoblinGrunt>())
                {
                    GoblinGrunt enemy = other.GetComponent<GoblinGrunt>();
                    damagePlayer(enemy.GetDamage());
                    rb.AddForce((other.transform.position - transform.position) * enemy.GetKnockBack() * -1.0f, ForceMode2D.Impulse);
                }
                else if (other.GetComponent<GoblinBerserker>())
                {
                    GoblinBerserker enemy = other.GetComponent<GoblinBerserker>();
                    damagePlayer(enemy.GetDamage());
                    rb.AddForce((other.transform.position - transform.position) * enemy.GetKnockBack() * -1.0f, ForceMode2D.Impulse);
                }
                else if (other.GetComponent<GoblinAssasin>())
                {
                    GoblinAssasin enemy = other.GetComponent<GoblinAssasin>();
                    damagePlayer(enemy.GetDamage());
                    rb.AddForce((other.transform.position - transform.position) * enemy.GetKnockBack() * -1.0f, ForceMode2D.Impulse);
                }
            }
        }
    }

    // Getter functions

    // returns the player's max amoutn of HP
    public static int getMAXHP()
    {
        return MAXHP;
    }

    // returns the player's current HP
    public static int getHP()
    {
        return hp;
    }

    // returns the amount of mana the player currently has
    public static int getMana()
    {
        return mana;
    }

    // returns screenCursorPoint
    public static Vector3 getScreenCursorPoint()
    {
        return screenCursorPoint;
    }

    // returns WorldCursorPoint
    public static Vector3 getWorldCursorPoint()
    {
        return worldCursorPoint;
    }

    // returns gridCursorPoint
    public static Vector3 getGridCursorPoint()
    {
        return gridCursorPoint;
    }

    // returns arrayCursorPoint
    public static Vector3 getArrayCursorPoint()
    {
        return arrayCursorPoint;
    }

    public static bool cursorWithinBounds()
    {
        return arrayCursorPoint.x >= 0 && arrayCursorPoint.x < 12 && arrayCursorPoint.y >= 0 && arrayCursorPoint.y < 12;
    }

    // returns the angle between the cursor and the player
    public static float getCursorAngle()
    {
        return cursorAngle;
    }

    // returns whether or not the player is currently in build mode
    public static bool isInBuildMode()
    {
        return inBuildMode;
    }

    // Setter functions

    // sets the player's hp back to full
    public static void resetPlayerHP()
    {
        hp = MAXHP;
    }

    // decreases the player's health when damaged
    public static void damagePlayer(int damage)
    {
        hp -= damage;

        if (hp < 0)
        {
            hp = 0;
        }
        else if (hp > MAXHP)
        {
            hp = MAXHP;
        }

        PlayerTimer.activateDamageCooldown();
    }

    public static void setMana(int m)
    {
        if (m >= 0)
        {
            mana = m;
        }
    }

    // gives the player mana
    public static void giveMana(int m)
    {
        mana += m;

        if (mana < 0)
        {
            mana = 0;
        }
    }

    // requests the player to spend mana, returns false if the player cannot spend the amount requested
    public static bool spendMana(int m)
    {
        if (mana - m < 0)
        {
            return false;
        }
        else
        {
            mana -= m;
            return true;
        }
    }

    // used for updating the location of the cursor
    public static void setScreenCursorPoint(Vector3 point)
    {
        screenCursorPoint = point;
    }

    // used for updating the cursor angle
    private void calculateCursorAngle()
    {
        Vector3 difference = worldCursorPoint - transform.position;
        float angle = (float) (Math.Atan(difference.y / difference.x) * (180 / Math.PI));

        if (difference.x < 0)
        {
            if (difference.y > 0)
            {
                angle += 180.0f;
            }
            else
            {
                angle -= 180.0f;
            }
        }

        cursorAngle = angle;
    }

    // sets the inBuildMode bool to a certain value
    public static void setBuildMode(bool b)
    {
        inBuildMode = b;
    }

    // flips the inBuildMode value
    public static void flipBuildMode()
    {
        inBuildMode = !inBuildMode;
    }
}