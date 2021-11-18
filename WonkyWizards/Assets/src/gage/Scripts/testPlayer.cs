using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayer : MonoBehaviour
{
    public float speed = 3.0f;
    private Rigidbody2D rb;
    public bool opening, walls = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector3.up * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collisions)
    {
        if(collisions.gameObject.tag ==  "Wall")
        {
            walls = true;
        }
        if (collisions.gameObject.tag == "EntranceWall")
        {
            opening = true;
        }
    }

    private void FixedUpdate()
    {
        //rb.AddForce(Vector3.up * speed, ForceMode2D.Impulse);
    }
}
