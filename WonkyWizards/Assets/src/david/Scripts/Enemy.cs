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
    protected int max_health; // Maximum ammount of health enemy has
    protected int health; // Ammount of health enemy starts with
    protected int damage; // Ammount of damage enemy starts with
    protected float move_speed; // The movement speed the enemy starts with

    protected float attack_speed; // The attack speed the enemy starts with

    protected float knock_back; // Ammount of knockback applied

    // Basic constructor for Enemy class
    public Enemy()
    {
        max_health = 100;
        health = 100;
        damage = 10;
        move_speed = 10;
        attack_speed = 1f;
        knock_back = 200f;
    }

    // Methods
    virtual public float GetMoveSpeed() 
    {
        return move_speed;
    }

    // Start is called before the first frame update
    void Start()
    {
    
    }
}
