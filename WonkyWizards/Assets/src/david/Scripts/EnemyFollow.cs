using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public GameObject self;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        // Find players component
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update(){
        if(Vector2.Distance(transform.position, target.position) > stoppingDistance){
            // Move position towards target position
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else {
            Destroy(self);
        }
    }

}
