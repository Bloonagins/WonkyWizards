using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health;
    public float moveSpeed;
    public int maxHealth;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = PlayerScript.getWorldCursorPoint(); 
    }
public void damage(int hitvalues)
    {
        if (health > hitvalues)
        {
           health -= hitvalues;
        }
        else
        {
            health = 0;
        }
        

    }
public void heal(int healvalues)
    {
        if (health + healvalues > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += healvalues;
        }
    }
}