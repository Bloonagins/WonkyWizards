/**********************************************
| Enemy V1.0.0                                |
| Author: David Bush, T5                      |
| Description: This is the Enemy super class  |
| that each enemy type will inherit from. It  |
| will contain the stats, and methods shared  |
| between each subclass.                      |
| Bugs:                                       |
**********************************************/

using System.Collections;
using UnityEngine;

// This is the parent class for the enemy type child classes 
public class Enemy : MonoBehaviour
{
    // Variables
    protected int health; // Ammount of health enemy starts with
    protected int damage; // Ammount of damage enemy starts with
    protected float move_speed; // The movement speed the enemy starts with

    protected float attack_speed; // The attack speed the enemy starts with

    // Methods

    // Basic constructor for Enemy class
    public Enemy()
    {
        health = 100;
        damage = 10;
        move_speed = 1;
        attack_speed = 0.5f;
    }

    // Start is called before the first frame update
    void Start()
    {
    
    }
}
