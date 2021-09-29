using System.Collections;
using UnityEngine;

// This is the parent class for the enemy type child classes 
public class Enemy : MonoBehaviour
{
    // Variables
    public int health; // Ammount of health enemy starts with
    public int damage; // Ammount of damage enemy starts with
    public float move_speed; // The movement speed the enemy starts with

    public float attack_speed; // The attack speed the enemy starts with

    // Methods

    // Basic constructor for Enemy class
    public Enemy()
    {
        health = 100;
        damage = 10;
        move_speed = 1;
        attack_speed = 0.5f;
    }
}
