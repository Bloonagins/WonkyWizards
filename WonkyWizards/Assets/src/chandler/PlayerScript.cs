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
    public static Vector3 screenCursorPoint;
    public static Vector3 worldCursorPoint;
    // angle between cursor and player
    public static float cursorAngle;
    // player's rigidbody component
    public Rigidbody2D rb;
    // speed of the player
    public float movementspeed;
    // player's health and mana point values
    public static int hp = 1000;
    public static int mana = 0;
    // index number of which item is currently selected in the hotbar
    public static int spellIndex;
    public static int summonIndex;
    // determines whether the player places summons or casts spells with click
    public static bool inBuildMode;
    // keeps track of how long until the player is allowed to dash again
    public static float dashtimer;
    // temp variable that determines whether or not enemies are allowed to be spawned
    public static bool allowSpawn;

    // called when the game loads up
    void Awake()
    {
        // gets a link to this game object's rigid body component
        rb = GetComponent<Rigidbody2D>();
        // Confines the cursor to within the screen (NOTE: send this to Zach to put in GameManager)
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Start is called before the first frame update
    void Start()
    {
        spellIndex = 0;
        summonIndex = 0;
    }

    // moves the camera to the middle of the screen when the game is alt-tabbed out
    void OnApplicationPause()
    {
        worldCursorPoint = transform.position;
        screenCursorPoint = Camera.main.WorldToScreenPoint(worldCursorPoint);
    }

    private void OnStayEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Enemy")
        {
            if (other.GetComponent<GoblinGrunt>())
            {
                hp -= other.GetComponent<GoblinGrunt>().GetDamage();
                // AddForce
            }
        }
    }
}