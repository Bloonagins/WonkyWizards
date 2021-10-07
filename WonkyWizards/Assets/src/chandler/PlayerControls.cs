/*
 * PlayerControls.cs
 * This script handles user inputs and updates player data based on inputs
 * 
 * Current Input Map:
 * 
 * Movement: WASD
 * Dash: Spacebar
 * Casting Spells: Left Mouse (In casting mode)
 * Placing Summons: Left Mouse (In build mode)
 * Deleting Summons: Right Click (In build mode)
 * Changing Summon Targetting Mode: E
 * Switching between spellcasting and summoning mode: R
 * Changing Selected Spell / Summon in Hotbar: Scroll wheel
 *                                             OR
 *                                             Alpha Number keys on keyboard
 * Pause: Escape (Handled in one of Gabe's scripts)
 * Wave Info: Tab
 * Ready Up: F4 (TODO)
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
    private InputAction delete; // right mouse to delete a summon
    private InputAction target; // E to make summon switch targetting mode
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
    private InputAction info; // tab to display info about the wave
    private InputAction rup; // F4 to enter combat mode
    // player's rigidbody component
    private Rigidbody2D rb;
    // speed of the player (30)
    public float movementspeed;
    // dash speed of the player (600)
    public float dashspeed;
    // time allowed until player is allowed to dash again (2)
    public float dashreset;
    // links to spell prefabs
    public GameObject[] spells = new GameObject[5];
    // links to summon prefabs
    public GameObject[] summons = new GameObject[10];
    // link to boss prefab
    public GameObject boss;

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
        delete = controls.PlayerDefault.DeleteSummon;
        target = controls.PlayerDefault.SwitchTargetMode;
        mode = controls.PlayerDefault.SwitchMagicMode;
        info = controls.PlayerDefault.WaveInfo;
        rup = controls.PlayerDefault.ReadyUp;

        // binds certain inputs to a function to be called when that input is activated
        dash.performed += OnDash;
        summon.performed += OnSummon;
        delete.performed += OnDelete;
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
        rup.performed += OnReadyUp;

        movement.Enable();
        dash.Enable();
        shoot.Enable();
        summon.Enable();
        delete.Enable();
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
        rup.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript.setBuildMode(true);
        PlayerScript.dashtimer = dashreset;
    }

    // Update is called once per frame
    void Update()
    {
        // gets the coordinates of the cursor
        PlayerScript.setScreenCursorPoint(Mouse.current.position.ReadValue());
        // rotates the player towards the cursor
        transform.eulerAngles = Vector3.forward * PlayerScript.getCursorAngle();

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
        if (!PlayerScript.isInBuildMode())
        {
            if (PlayerTimer.canCast(PlayerScript.spellIndex) && shoot.ReadValue<float>() > 0.0f)
            {
                GameObject spell = spells[PlayerScript.spellIndex];
                Instantiate(spell, transform.position, transform.rotation);
                PlayerTimer.activateSpellCooldown(PlayerScript.spellIndex);
            }
        }

        // while tab is being held down, display wave info
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
    }

    // returns the angle between the cursor and the player
    private float getCursorAngle()
    {
        Vector3 difference = PlayerScript.getWorldCursorPoint() - transform.position;
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
        if (PlayerTimer.canDash() && movement.ReadValue<Vector2>() != Vector2.zero)
        {
            rb.AddForce(movement.ReadValue<Vector2>() * dashspeed, ForceMode2D.Impulse);
            PlayerTimer.activateDashCooldown();
        }
    }

    // when click is pressed and player is in build mode, place a summon
    private void OnSummon(InputAction.CallbackContext obj)
    {
        // makes sure player is in build mode
        if (PlayerScript.isInBuildMode())
        {
            // makes sure current spell in the array exists
            GameObject summon = summons[PlayerScript.summonIndex];
            if (summon != null)
            {
                // if the player has enough mana
                if (PlayerScript.spendMana(summon.GetComponent<Barrier>().getCost()))
                {
                    Instantiate(summon, PlayerScript.getGridCursorPoint(), Quaternion.identity);
                }
                else
                {
                    Debug.Log("Not enough mana");
                }
            }
            else
            {
                Debug.Log("Cannot spawn summon number " + PlayerScript.summonIndex);
            }
        }
    }

    // when right click is pressed, delete the summon that was clicked on
    private void OnDelete(InputAction.CallbackContext obj)
    {
        if (PlayerScript.isInBuildMode())
        {
            Debug.Log("Delete Summon");
        }
    }

    // when E is pressed, switch the targetting mode of the summon that the mouse was hovering over when E was pressed
    private void OnSwitchTarget(InputAction.CallbackContext obj)
    {
        Debug.Log("Switch Target");
    }

    // when R is pressed, switch from summon building mode to spell casting mode and vice versa
    private void OnSwitchMagicMode(InputAction.CallbackContext obj)
    {
        PlayerScript.flipBuildMode();
    }

    // when one of the alpha number keys is pressed, change the hotbar index to that number
    // when 1 is pressed
    private void OnHotbar1(InputAction.CallbackContext obj)
    {
        if (PlayerScript.isInBuildMode())
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
        if (PlayerScript.isInBuildMode())
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
        if (PlayerScript.isInBuildMode())
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
        if (PlayerScript.isInBuildMode())
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
        if (PlayerScript.isInBuildMode())
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
        if (PlayerScript.isInBuildMode())
        {
            PlayerScript.summonIndex = 5;
        }
    }
    // when 7 is pressed
    private void OnHotbar7(InputAction.CallbackContext obj)
    {
        if (PlayerScript.isInBuildMode())
        {
            PlayerScript.summonIndex = 6;
        }
    }
    // when 8 is pressed
    private void OnHotbar8(InputAction.CallbackContext obj)
    {
        if (PlayerScript.isInBuildMode())
        {
            PlayerScript.summonIndex = 7;
        }
    }
    // when 9 is pressed
    private void OnHotbar9(InputAction.CallbackContext obj)
    {
        if (PlayerScript.isInBuildMode())
        {
            PlayerScript.summonIndex = 8;
        }
    }
    // when 0 is pressed
    private void OnHotbar0(InputAction.CallbackContext obj)
    {
        if (PlayerScript.isInBuildMode())
        {
            PlayerScript.summonIndex = 9;
        }
    }

    // when F4 is pressed, ready up
    private void OnReadyUp(InputAction.CallbackContext obj)
    {
        Debug.Log("Ready Up");
    }

    // handles disabling unity input package scheme
    void OnDisable()
    {
        mode.performed -= OnSwitchMagicMode;

        movement.Disable();
        dash.Disable();
        shoot.Disable();
        summon.Disable();
        delete.Disable();
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
        rup.Disable();
    }

    // changes the spell / summon index on the hotbar when a scroll is inputted
    private void updateHotbarIndex(int change)
    {
        if (change > 0)
        {
            if (PlayerScript.isInBuildMode())
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
            if (PlayerScript.isInBuildMode())
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