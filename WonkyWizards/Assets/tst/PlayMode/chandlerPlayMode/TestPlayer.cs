using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public float speed = 30.0f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D wall)
    {
        speed *= -1.0f;
        if (speed < 0)
        {
            speed -= 2.0f;
        }
        else
        {
            speed += 2.0f;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
    }
}
