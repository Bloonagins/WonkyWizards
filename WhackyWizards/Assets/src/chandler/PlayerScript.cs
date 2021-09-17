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

    // player's rigidbody component
    public Rigidbody2D rb;
    // speed of the player
    public float movementspeed;

    void Awake()
    {
        this.controls = new PlayerControls();
        this.rb = this.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // gets the position of the mouse and rotates the player towards the mouse
        Vector3 mousePoint = MainCamera.getMousePoint();
        Vector3 difference = mousePoint - this.transform.position;
        float angle = (float) ((180 / Math.PI) * Math.Atan((double)(difference.y / difference.x)));
        this.transform.eulerAngles = Vector3.forward * angle;
    }

    void FixedUpdate()
    {
        // reads WASD input from the player, multiplies that input by the movement speed, and moves the player that direction
        this.rb.AddForce(movement.ReadValue<Vector2>() * movementspeed, ForceMode2D.Impulse);
    }

    // handles enabling unity input package scheme
    void OnEnable()
    {
        this.movement = controls.PlayerDefault.BasicMovement;
        this.movement.Enable();
    }

    // handles disabling unity input package scheme
    void OnDisable()
    {
        this.movement.Disable();
    }
}