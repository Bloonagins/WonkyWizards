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
 * Wave Info: Tab (Handled in one of Gabe's scripts)
 * Ready Up: F4 / L (Handled in one of Gabe's scripts)
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
    // player's rigidbody component
    private Rigidbody2D rb;
    // speed of the player (30)
    public float movementSpeed;
    // dash speed of the player (600)
    public float dashSpeed;
    // links to spell prefabs
    public GameObject[] spells = new GameObject[5];
    // links to summon prefabs
    public GameObject[] summons = new GameObject[10];

    private PlayerControls()
    { }

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
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript.setBuildMode(true);
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

        // if click is pressed, the player is in casting mode, and the game isn't paused, then see if a spell can be casted
        if (shoot.ReadValue<float>() > 0.0f && !PlayerScript.isInBuildMode() && GameManager.CheckState() != GameState.PAUSE)
        {
            // if the spell isn't on a cooldown, then cast it
            if (PlayerTimer.canCast(PlayerScript.spellIndex))
            {
                GameObject spell = spells[PlayerScript.spellIndex];
                Instantiate(spell, transform.position, Quaternion.Euler(Vector3.Normalize(PlayerScript.getWorldCursorPoint() - transform.position)));
                PlayerTimer.activateSpellCooldown(PlayerScript.spellIndex);
            }
        }
    }

    // Called a set amount of times per second (50 by default)
    void FixedUpdate()
    {
        // reads WASD input from the player, multiplies that input by the movement speed, and moves the player that direction
        rb.AddForce(movement.ReadValue<Vector2>() * movementSpeed, ForceMode2D.Impulse);
    }

    // when space is pressed, add a big force to the player to make them dash
    private void OnDash(InputAction.CallbackContext obj)
    {
        if (PlayerTimer.canDash() && movement.ReadValue<Vector2>() != Vector2.zero)
        {
            rb.AddForce(movement.ReadValue<Vector2>() * dashSpeed, ForceMode2D.Impulse);
            PlayerTimer.activateDashCooldown();
        }
    }

    // when click is pressed and player is in build mode, place a summon
    private void OnSummon(InputAction.CallbackContext obj)
    {
        // makes sure player is in build mode and the game isn't paused
        if (PlayerScript.isInBuildMode() && GameManager.CheckState() != GameState.PAUSE)
        {
            // makes sure current spell in the array exists
            GameObject summon = summons[PlayerScript.summonIndex];
            if (summon != null)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                // if the raycast either hits nothing, or the thing it hit wasn't a summon
                if
                (hit.collider == null || (hit.collider.tag != "Summon" && hit.collider.tag != "Goal" && hit.collider.tag != "SummonNoPlace"))
                {
                    if (PlayerScript.getMana() >= summon.GetComponent<Summon>().getCost())
                    {
                        // if the cursor is within the bounds of the level
                        if (PlayerScript.cursorWithinBounds())
                        {
                            Tuple<int, int> summonPosition = new Tuple<int, int>
                            (
                                (int)PlayerScript.getArrayCursorPoint().y,
                                (int)PlayerScript.getArrayCursorPoint().x
                            );

                            // if the square is a valid location, then attempt to place the summon, and if that succeeds, spend mana
                            if (Summon.attemptPlacement(summon, PlayerScript.getGridCursorPoint(), summonPosition.Item1, summonPosition.Item2))
                            {
                                PlayerScript.spendMana(summon.GetComponent<Summon>().getCost());
                            }
                            else
                            {
                                Debug.Log("Invalid Position");
                            }
                        }
                        else
                        {
                            Debug.Log("Cursor Out of Bounds");
                        }
                    }
                    else
                    {
                        Debug.Log("Not enough mana");
                    }
                }
                else
                {
                    Debug.Log("Raycast hit. tag: " + hit.transform.tag);
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
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null) Debug.Log("trying to delete (tag): " + hit.collider.tag);
            else Debug.Log("trying to delete null collider.");

            // if that ray cast hit a Summon...
            if
            (hit.collider != null && hit.collider.tag == "Summon")
            {
                // if it is then give the player their mana back and delete it
                PlayerScript.giveMana(hit.transform.gameObject.GetComponent<Summon>().getCost());
                Destroy(hit.transform.gameObject);

                // find the position being removed
                int x = (int)PlayerScript.getArrayCursorPoint().y;
                int y = (int)PlayerScript.getArrayCursorPoint().x;

                // delete the summon
                Debug.Log("[PlayerControls] trying to delete " + hit.transform.name);
                hit.transform.gameObject.GetComponent<Summon>().deleteSummon(x, y);
            }
        }
    }

    // when E is pressed, switch the targetting mode of the summon that the mouse was hovering over when E was pressed
    private void OnSwitchTarget(InputAction.CallbackContext obj)
    {
        if (PlayerScript.isInBuildMode())
        {
            // create a ray cast based on the mouse location pointing down
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            // if that ray cast hit a summon...
            if (hit.collider != null && hit.collider.tag == "Summon")
            {
                // then change that summon's targetting mode
                hit.transform.gameObject.GetComponent<Summon>().cycleTargetMode();
            }
        }
    }

    // when R is pressed, switch from summon building mode to spell casting mode and vice versa
    private void OnSwitchMagicMode(InputAction.CallbackContext obj)
    {
        if (GameManager.CheckState() == GameState.SETUP)
        {
            PlayerScript.flipBuildMode();
        }
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