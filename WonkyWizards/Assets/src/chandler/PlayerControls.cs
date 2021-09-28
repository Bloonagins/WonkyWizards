/*
 * PlayerControls.cs
 * This script handles user inputs and updates player data based on inputs
 * 
 * Current Input Map:
 * 
 * Movement: WASD
 * Casting Spells: Left Mouse
 * Placing Summons: Left Mouse
 * Switching between spellcasting and summoning mode: Tab
 * Changing Selected Spell / Summon in Hotbar: Scroll wheel
 *                                             OR
 *                                             Alpha Number keys on keyboard
 * Changing Summon Targetting Mode: Right Mouse (TODO)
 * Dash: Spacebar (TODO) (Might not add)
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    // links to inputs / controls
    private ControlScheme controls;
    private InputAction movement;
    private InputAction shoot;
    private InputAction mode;
    private InputAction hotbar1;
    private InputAction hotbar2;
    private InputAction hotbar3;
    private InputAction hotbar4;
    private InputAction hotbar5;
    private InputAction hotbar6;
    private InputAction hotbar7;
    private InputAction hotbar8;
    private InputAction hotbar9;
    private InputAction hotbar0;
    // player's rigidbody component
    private Rigidbody2D rb;
    // speed of the player
    public float movementspeed;

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
        // gets a link to the movement and mouse inputs
        movement = controls.PlayerDefault.Move;
        shoot = controls.PlayerDefault.Cast;
        mode = controls.PlayerDefault.SwitchMagicMode;

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

        movement.Enable();
        shoot.Enable();
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
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript.inBuildMode = true;
    }

    // Update is called once per frame
    void Update()
    {
        // gets the coordinates of the cursor
        PlayerScript.screenCursorPoint = Mouse.current.position.ReadValue();
        PlayerScript.worldCursorPoint = Camera.main.ScreenToWorldPoint(PlayerScript.screenCursorPoint);

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

        // when click is pressed, either summon a tower or cast a spell
        if (shoot.ReadValue<float>() > 0.0f)
        {
            // if in build mode AND game is in setup mode, the create a summon
            if (PlayerScript.inBuildMode)
            {
                // place summon
            }
            // if in casting mode, then cast a spell
            else
            {
                // shoot spell
            }
        }
    }

    // Called a set amount of times per second (60 by default)
    void FixedUpdate()
    {
        // reads WASD input from the player, multiplies that input by the movement speed, and moves the player that direction
        rb.AddForce(movement.ReadValue<Vector2>() * movementspeed, ForceMode2D.Impulse);
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

    // when tab is pressed, switch from summon building mode to spell casting mode and vice versa
    private void OnSwitchMagicMode(InputAction.CallbackContext obj)
    {
        PlayerScript.inBuildMode = !PlayerScript.inBuildMode;
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
        shoot.Disable();
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