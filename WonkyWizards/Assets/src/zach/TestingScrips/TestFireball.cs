using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFireball : MonoBehaviour
{
    private Transform firePoint;
    //the desired prefab to cast
    public GameObject projectile;
    public float speed = 20.0f;

    //point in the direction of the player and fire
    void Awake()
    {
        var player = GameObject.FindWithTag("Player");
        firePoint = player.transform; //get player position/rotation
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * this.speed, ForceMode2D.Impulse);
    }

    //detect collision between anything that is collidable
    void OnTriggerEnter2D(Collider2D collision)
    {
        speed *= -1.5f;
    }
}
