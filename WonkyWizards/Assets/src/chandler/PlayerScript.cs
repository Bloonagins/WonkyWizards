using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    // links to inputs / controls
    private InputAction movement;
    private PlayerControls controls;
    // cursor coordinates
    private Vector3 mousePoint;
    // player's rigidbody component
    public Rigidbody2D rb;
    // speed of the player
    public float movementspeed;

    void Awake()
    {
        // gets a link to the control scheme
        controls = new PlayerControls();
        // gets a link to this game object's rigid body component
        rb = GetComponent<Rigidbody2D>();
    }

    // handles enabling unity input package scheme
    void OnEnable()
    {
        // gets a link to the movement controls
        movement = controls.PlayerDefault.BasicMovement;
        movement.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // gets the coordinates of the cursor
        mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // calculates the angle between the cursor and the player and turns the player towards the cursor
        Vector3 difference = mousePoint - transform.position;
        float angle = (float) ((180 / Math.PI) * Math.Atan((double)(difference.y / difference.x)));
        transform.eulerAngles = Vector3.forward * angle;
    }

    void FixedUpdate()
    {
        // reads WASD input from the player, multiplies that input by the movement speed, and moves the player that direction
        rb.AddForce(movement.ReadValue<Vector2>() * movementspeed, ForceMode2D.Impulse);
    }

    // handles disabling unity input package scheme
    void OnDisable()
    {
        movement.Disable();
    }
}