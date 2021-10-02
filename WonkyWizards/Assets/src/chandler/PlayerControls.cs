/*
 * PlayerControls.cs
 * This script handles user inputs and updates player data based on inputs
 * 
 * Current Input Map:
 * 
 * Movement: WASD
 * Dash: Spacebar
 * Casting Spells: Left Mouse
 * Placing Summons: Left Mouse
 * Changing Summon Targetting Mode: Right Mouse
 * Switching between spellcasting and summoning mode: R
 * Changing Selected Spell / Summon in Hotbar: Scroll wheel
 *                                             OR
 *                                             Alpha Number keys on keyboard
 * Pause: Escape (Handled in one of Gabe's scripts)
 * Ready Up: F4 (TODO)
 * Wave Info: Tab (TODO)
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    // links to inputs / controls
    private ControlScheme controls; // overall input scheme
    private InputAction movement; // WASD movement
    private InputAction dash; // spacebar to dash
    private InputAction shoot; // left mouse to cast spell
    private InputAction summon; // left mouse to place summon
    private InputAction target; // right mouse to make summon switch targetting mode
    private InputAction mode; // R to switch between build mode and cast mode
    private InputAction hotbar1; // alpha number keys to select items from hotbar
    private InputAction hotbar2;
    private InputAction hotbar3;
    private InputAction hotbar4;
    private InputAction hotbar5;
    private InputAction hotbar6;
    private InputAction hotbar7;
    private InputAction hotbar8;
    private InputAction hotbar9;
    private InputAction hotbar0;
    private InputAction info;
    private InputAction spawner; // q to enable / disable test enemy spawner
    private InputAction bossB; // b to spawn a boss on the cursor
    // player's rigidbody component
    private Rigidbody2D rb;
    // speed of the player (30)
    public float movementspeed;
    // dash speed of the player (600)
    public float dashspeed;
    // time allowed until player is allowed to dash again (2)
    public float dashreset;
    // links to spell prefabs
    public GameObject testSpell;
    // links to summon prefabs
    public GameObject barrier;
    // link to boss prefab
    public GameObject boss;
    // link to the level main object (might get rid of it since it'll probably overlap with the game manager a lot)
    public GameObject lmo;

    void Awake()
    {
        // gets a link to the control scheme
        controls = new ControlScheme();
        // gets a link to this game object's rigid body component
        rb = GetComponent<Rigidbody2D>();
    }

    // handles enabling unity input package scheme
    void OnEnable()
    {
        // gets a link to the inputs
        movement = controls.PlayerDefault.Move;
        dash = controls.PlayerDefault.Dash;
        shoot = controls.PlayerDefault.Cast;
        summon = controls.PlayerDefault.Summon;
        target = controls.PlayerDefault.SwitchTargetMode;
        mode = controls.PlayerDefault.SwitchMagicMode;
        info = controls.PlayerDefault.WaveInfo;
        spawner = controls.PlayerDefault.FlipSpawner;
        bossB = controls.PlayerDefault.BossSpawn;

        // binds certain inputs to a function to be called when that input is activated
        dash.performed += OnDash;
        summon.performed += OnSummon;
        target.performed += OnSwitchTarget;
        mode.performed += OnSwitchMagicMode;
        controls.PlayerDefault.Hotbar1.performed += OnHotbar1;
        controls.PlayerDefault.Hotbar2.performed += OnHotbar2;
        controls.PlayerDefault.Hotbar3.performed += OnHotbar3;
        controls.PlayerDefault.Hotbar4.performed += OnHotbar4;
        controls.PlayerDefault.Hotbar5.performed += OnHotbar5;
        controls.PlayerDefault.Hotbar6.performed += OnHotbar6;
        controls.PlayerDefault.Hotbar7.performed += OnHotbar7;
        controls.PlayerDefault.Hotbar8.performed += OnHotbar8;
        controls.PlayerDefault.Hotbar9.performed += OnHotbar9;
        controls.PlayerDefault.Hotbar0.performed += OnHotbar0;
        spawner.performed += OnFlipSpawner;
        bossB.performed += OnBossSpawn;

        movement.Enable();
        dash.Enable();
        shoot.Enable();
        summon.Enable();
        target.Enable();
        mode.Enable();
        controls.PlayerDefault.Hotbar1.Enable();
        controls.PlayerDefault.Hotbar2.Enable();
        controls.PlayerDefault.Hotbar3.Enable();
        controls.PlayerDefault.Hotbar4.Enable();
        controls.PlayerDefault.Hotbar5.Enable();
        controls.PlayerDefault.Hotbar6.Enable();
        controls.PlayerDefault.Hotbar7.Enable();
        controls.PlayerDefault.Hotbar8.Enable();
        controls.PlayerDefault.Hotbar9.Enable();
        controls.PlayerDefault.Hotbar0.Enable();
        info.Enable();
        spawner.Enable();
        bossB.Enable();
    }

    // REMOVE AFTER DONE TESTING
    // when b is pressed, spawn a boss object on the cursor
    private void OnBossSpawn(InputAction.CallbackContext obj)
    {
        Instantiate(boss);
    }

    // REMOVE AFTER DONE TESTING
    // when q is pressed, activate / deactivate the enemy spawner
    private void OnFlipSpawner(InputAction.CallbackContext obj)
    {
        PlayerScript.allowSpawn = !PlayerScript.allowSpawn;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript.inBuildMode = true;
        PlayerScript.dashtimer = dashreset;
    }

    // Update is called once per frame
    void Update()
    {
        // gets the coordinates of the cursor
        PlayerScript.screenCursorPoint = Mouse.current.position.ReadValue();
        PlayerScript.worldCursorPoint = Camera.main.ScreenToWorldPoint(PlayerScript.screenCursorPoint);
        PlayerScript.worldCursorPoint.z = 0.0f;

        // updates the angle between the cursor and the player
        PlayerScript.cursorAngle = getCursorAngle();
        // rotates the player towards the cursor
        transform.eulerAngles = Vector3.forward * PlayerScript.cursorAngle;

        // when scroll wheel is inputted, change hotbar index
        if (Mouse.current.scroll.ReadValue().normalized.y > 0)
        {
            updateHotbarIndex(1);
        }
        else if (Mouse.current.scroll.ReadValue().normalized.y < 0)
        {
            updateHotbarIndex(-1);
        }

        // if player is in cast mode and click is pressed, cast spell
        if (!PlayerScript.inBuildMode)
        {
            if (shoot.ReadValue<float>() > 0.0f)
            {
                Instantiate(testSpell, transform.position, transform.rotation);
            }
        }

        if (info.ReadValue<float>() > 0.0f)
        {
            Debug.Log("Display Wave Info");
        }
    }

    // Called a set amount of times per second (50 by default)
    void FixedUpdate()
    {
        // reads WASD input from the player, multiplies that input by the movement speed, and moves the player that direction
        rb.AddForce(movement.ReadValue<Vector2>() * movementspeed, ForceMode2D.Impulse);

        // dash timer cooldown
        if (PlayerScript.dashtimer < dashreset)
        {
            PlayerScript.dashtimer += Time.fixedDeltaTime;
        }
    }

    // returns the angle between the cursor and the player
    private float getCursorAngle()
    {
        Vector3 difference = PlayerScript.worldCursorPoint - transform.position;
        float angle = (float)((180 / Math.PI) * Math.Atan((double)(difference.y / difference.x)));
        if (difference.x < 0.0f && difference.y > 0.0f)
        {
            angle += 180.0f;
        }
        else if (difference.x < 0.0f && difference.y < 0.0f)
        {
            angle -= 180.0f;
        }
        return angle;
    }

    // when space is pressed, add a big force to the player to make them dash
    private void OnDash(InputAction.CallbackContext obj)
    {
        // if the dash isn't on cooldown and the player is currently pressing directional movement button, then dash and activate the dash cooldown timer
        if (PlayerScript.dashtimer >= dashreset && movement.ReadValue<Vector2>() != Vector2.zero)
        {
            rb.AddForce(movement.ReadValue<Vector2>() * dashspeed, ForceMode2D.Impulse);
            PlayerScript.dashtimer = 0.0f;
        }
    }

    // when click is pressed and player is in build mode, place a summon
    private void OnSummon(InputAction.CallbackContext obj)
    {
        if (PlayerScript.inBuildMode)
        {
            Instantiate(barrier);
        }
    }

    // when right click is pressed, switch the targetting mode of the summon that was clicked on
    private void OnSwitchTarget(InputAction.CallbackContext obj)
    {
        Debug.Log("Switch Target");
    }

    // when R is pressed, switch from summon building mode to spell casting mode and vice versa
    private void OnSwitchMagicMode(InputAction.CallbackContext obj)
    {
        PlayerScript.inBuildMode = !PlayerScript.inBuildMode;
        // tells ui to update what mode to display
        lmo.GetComponent<UIManager>().UpdatePlayerModeUI();
    }

    // when one of the alpha number keys is pressed, change the hotbar index to that number
    // when 1 is pressed
    private void OnHotbar1(InputAction.CallbackContext obj)
    {
        if (PlayerScript.inBuildMode)
        {
            PlayerScript.summonIndex = 0;
        }
        else
        {
            PlayerScript.spellIndex = 0;
        }
    }
    // when 2 is pressed
    private void OnHotbar2(InputAction.CallbackContext obj)
    {
        if (PlayerScript.inBuildMode)
        {
            PlayerScript.summonIndex = 1;
        }
        else
        {
            PlayerScript.spellIndex = 1;
        }
    }
    // when 3 is pressed
    private void OnHotbar3(InputAction.CallbackContext obj)
    {
        if (PlayerScript.inBuildMode)
        {
            PlayerScript.summonIndex = 2;
        }
        else
        {
            PlayerScript.spellIndex = 2;
        }
    }
    // when 4 is pressed
    private void OnHotbar4(InputAction.CallbackContext obj)
    {
        if (PlayerScript.inBuildMode)
        {
            PlayerScript.summonIndex = 3;
        }
        else
        {
            PlayerScript.spellIndex = 3;
        }
    }
    // when 5 is pressed
    private void OnHotbar5(InputAction.CallbackContext obj)
    {
        if (PlayerScript.inBuildMode)
        {
            PlayerScript.summonIndex = 4;
        }
        else
        {
            PlayerScript.spellIndex = 4;
        }
    }
    // when 6 is pressed
    private void OnHotbar6(InputAction.CallbackContext obj)
    {
        if (PlayerScript.inBuildMode)
        {
            PlayerScript.summonIndex = 5;
        }
    }
    // when 7 is pressed
    private void OnHotbar7(InputAction.CallbackContext obj)
    {
        if (PlayerScript.inBuildMode)
        {
            PlayerScript.summonIndex = 6;
        }
    }
    // when 8 is pressed
    private void OnHotbar8(InputAction.CallbackContext obj)
    {
        if (PlayerScript.inBuildMode)
        {
            PlayerScript.summonIndex = 7;
        }
    }
    // when 9 is pressed
    private void OnHotbar9(InputAction.CallbackContext obj)
    {
        if (PlayerScript.inBuildMode)
        {
            PlayerScript.summonIndex = 8;
        }
    }
    // when 0 is pressed
    private void OnHotbar0(InputAction.CallbackContext obj)
    {
        if (PlayerScript.inBuildMode)
        {
            PlayerScript.summonIndex = 9;
        }
    }

    // handles disabling unity input package scheme
    void OnDisable()
    {
        mode.performed -= OnSwitchMagicMode;

        movement.Disable();
        dash.Disable();
        shoot.Disable();
        summon.Disable();
        target.Disable();
        mode.Disable();
        controls.PlayerDefault.Hotbar1.Disable();
        controls.PlayerDefault.Hotbar2.Disable();
        controls.PlayerDefault.Hotbar3.Disable();
        controls.PlayerDefault.Hotbar4.Disable();
        controls.PlayerDefault.Hotbar5.Disable();
        controls.PlayerDefault.Hotbar6.Disable();
        controls.PlayerDefault.Hotbar7.Disable();
        controls.PlayerDefault.Hotbar8.Disable();
        controls.PlayerDefault.Hotbar9.Disable();
        controls.PlayerDefault.Hotbar0.Disable();
        info.Disable();
        spawner.Disable();
        bossB.Disable();
    }

    // changes the spell / summon index on the hotbar when a scroll is inputted
    private void updateHotbarIndex(int change)
    {
        if (change > 0)
        {
            if (PlayerScript.inBuildMode)
            {
                if (PlayerScript.summonIndex >= 9)
                {
                    PlayerScript.summonIndex = 0;
                }
                else
                {
                    PlayerScript.summonIndex++;
                }
            }
            else
            {
                if (PlayerScript.spellIndex >= 4)
                {
                    PlayerScript.spellIndex = 0;
                }
                else
                {
                    PlayerScript.spellIndex++;
                }
            }
        }
        else if (change < 0)
        {
            if (PlayerScript.inBuildMode)
            {
                if (PlayerScript.summonIndex <= 0)
                {
                    PlayerScript.summonIndex = 9;
                }
                else
                {
                    PlayerScript.summonIndex--;
                }
            }
            else
            {
                if (PlayerScript.spellIndex <= 0)
                {
                    PlayerScript.spellIndex = 4;
                }
                else
                {
                    PlayerScript.spellIndex--;
                }
            }
        }
        else
        {
            Debug.Log("Error: Misuse of updateHotbarIndex() function from PlayerScript.cs");
        }
    }
}