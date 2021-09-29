using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    // The speed associated with the unit
    public float speed;
    // The minimum distance reached before stopping
    public float stoppingDistance;
    // The game object associated with the unit
    public GameObject self;
    // The target that enemy will follow
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        // Get the target players component
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    
    // Update is called once per frame
    void Update(){
        // Check if the stopping distance hasn't been reached
        if(Vector2.Distance(transform.position, target.position) > stoppingDistance){
            // Move unit towards target player's position
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else { // Self destructs when too close to player
            //Destroy(self);
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.transform.tag == "Spell")
        {
            Destroy(self);
        }
    }

}
