using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private InputAction movement;
    private PlayerControls controls;

    public Rigidbody2D rb;

    public float movementspeed;
    private Vector2 moves;

    void Awake()
    {
        this.controls = new PlayerControls();
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        this.moves += movement.ReadValue<Vector2>() * movementspeed;
        this.rb.AddForce(moves, ForceMode2D.Impulse);
        this.moves = Vector2.zero;
    }

    void OnEnable()
    {
        this.movement = controls.PlayerDefault.BasicMovement;

        this.movement.Enable();
    }

    void OnDisable()
    {
        this.movement.Disable();
    }
}