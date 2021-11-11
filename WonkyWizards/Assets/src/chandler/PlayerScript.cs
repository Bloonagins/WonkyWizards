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
    // player's health and mana point values
    private static int MAXHP = 1000;
    private static int hp = 1000;
    private static int mana = 0;
    // index number of which item is currently selected in the hotbar
    private static int spellIndex;
    private static int summonIndex;
    // determines whether the player places summons or casts spells with click
    private static bool inBuildMode;
    // determines whether the player is in Dr. BC mode or not
    private static bool inBCMode;

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
        inBuildMode = true;
        inBCMode = false;
    }

    // Updateis called once every frame
    void Update()
    {
        // updates world cursor point
        worldCursorPoint = Camera.main.ScreenToWorldPoint(screenCursorPoint);
        worldCursorPoint.z = 0.0f;
        // updates the cursor angle
        cursorAngle = calculateVectorAngle(transform.position, worldCursorPoint);
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
            // if the player's damage cooldown isn't in effect
            if (PlayerTimer.canDamage())
            {
                // if the player collided with something that can cause damage, then damage and knockback the player
                if (other.GetComponent<GoblinGrunt>())
                {
                    GoblinGrunt enemy = other.GetComponent<GoblinGrunt>();
                    damagePlayer(enemy.GetDamage());
                    rb.AddForce(Vector3.Normalize(other.transform.position - transform.position) * enemy.GetKnockBack() * -1.0f, ForceMode2D.Impulse);
                }
                else if (other.GetComponent<GoblinBerserker>())
                {
                    GoblinBerserker enemy = other.GetComponent<GoblinBerserker>();
                    damagePlayer(enemy.GetDamage());
                    rb.AddForce(Vector3.Normalize(other.transform.position - transform.position) * enemy.GetKnockBack() * -1.0f, ForceMode2D.Impulse);
                }
                else if (other.GetComponent<GoblinAssassin>())
                {
                    GoblinAssassin enemy = other.GetComponent<GoblinAssassin>();
                    damagePlayer(enemy.GetDamage());
                    rb.AddForce(Vector3.Normalize(other.transform.position - transform.position) * enemy.GetKnockBack() * -1.0f, ForceMode2D.Impulse);
                }
                else if (other.GetComponent<GoblinGiant>())
                {
                    GoblinGiant enemy = other.GetComponent<GoblinGiant>();
                    damagePlayer(enemy.GetDamage());
                    rb.AddForce(Vector3.Normalize(other.transform.position - transform.position) * enemy.GetKnockBack() * -1.0f, ForceMode2D.Impulse);
                }
                else if (other.GetComponent<GoblinWarrior>())
                {
                    GoblinWarrior enemy = other.GetComponent<GoblinWarrior>();
                    damagePlayer(enemy.GetDamage());
                    rb.AddForce(Vector3.Normalize(other.transform.position - transform.position) * enemy.GetKnockBack() * -1.0f, ForceMode2D.Impulse);
                }
                else if (other.GetComponent<GoblinArcher>())
                {
                    GoblinArcher enemy = other.GetComponent<GoblinArcher>();
                    damagePlayer(enemy.GetDamage());
                    rb.AddForce(Vector3.Normalize(other.transform.position - transform.position) * enemy.GetKnockBack() * -1.0f, ForceMode2D.Impulse);
                }
                else if (other.GetComponent<Arrow>())
                {
                    Arrow arrow = other.GetComponent<Arrow>();
                    damagePlayer(arrow.GetDamage());
                    //rb.AddForce(Vector3.Normalize(other.transform.position - transform.position) * arrow.GetKnockBack() * -1.0f, ForceMode2D.Impulse);
                }
                else if (other.transform.tag == "explosion")
                {
                    GoblinGiant.GetDeathDamage();
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

    // returns the currently selected summon
    public static int getSummonIndex()
    {
        return summonIndex;
    }

    // returns the currently selected spell
    public static int getSpellIndex()
    {
        return spellIndex;
    }

    // returns whether or not the player is currently in build mode
    public static bool isInBuildMode()
    {
        return inBuildMode;
    }

    // returns whether or not the player is currently in Dr. BC mode
    public static bool isInBCMode()
    {
        return inBCMode;
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
        if (!inBCMode)
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

            if (hp <= 0)
            {
                GameManager.ChangeState(GameState.LOSE);
            }

            PlayerTimer.activateDamageCooldown();
        }
    }

    // sets the mana to a specific value
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
    public static float calculateVectorAngle(Vector3 origin, Vector3 away)
    {
        Vector3 difference = away - origin;
        float angle = (float)(Math.Atan(difference.y / difference.x) * (180 / Math.PI));

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

        return angle;
    }

    // selects a specific summon
    public static void setSummonIndex(int index)
    {
        if (index >=0 && index <= 9)
        {
            summonIndex = index;
        }
    }

    // selects a spcific spell
    public static void setSpellIndex(int index)
    {
        if (index >= 0 && index <= 4)
        {
            spellIndex = index;
        }
    }

    // changes the spell / summon index on the hotbar when a scroll is inputted
    public static void updateHotbarIndex(int change)
    {
        if (change > 0)
        {
            if (isInBuildMode())
            {
                if (summonIndex >= 9)
                {
                    summonIndex = 0;
                }
                else
                {
                    summonIndex++;
                }
            }
            else
            {
                if (spellIndex >= 4)
                {
                    spellIndex = 0;
                }
                else
                {
                    spellIndex++;
                }
            }
        }
        else if (change < 0)
        {
            if (isInBuildMode())
            {
                if (summonIndex <= 0)
                {
                    summonIndex = 9;
                }
                else
                {
                    summonIndex--;
                }
            }
            else
            {
                if (spellIndex <= 0)
                {
                    spellIndex = 4;
                }
                else
                {
                    spellIndex--;
                }
            }
        }
        else
        {
            Debug.Log("Error: Misuse of updateHotbarIndex() function from PlayerScript.cs");
        }
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

    // makes the player toggle Dr. BC mode on / off
    public static void flipBCMode()
    {
        inBCMode = !inBCMode;
    }
}