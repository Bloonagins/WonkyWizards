using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWall : MonoBehaviour
{
    public bool propercollision = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Spell")
        {
            propercollision = true;
        }
    }
}
