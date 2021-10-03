/**********************************************
| EnemyTargeting V1.0.0                       |
| Author: David Bush, T5                      |
| Description: This contains the enemy's      |
| targeting and pathfinding. It will also be  |
| able to determine whether enemy changes its |
| target from goal to player.                 |
| Bugs:                                       |
**********************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTargeting : MonoBehaviour
{
    // Game object associated with type
    public GameObject type;
    // The Player target the enemy is trying to follow
    private Transform target;
    // The goal object the enemy is trying to reach
    private Transform goal;
    // enemy agent component
    NavMeshAgent agent;
    // The speed associated with the unit
    private float speed;
    // Distance where enemy switches to targeting player
    public float targetDistance;
    // The minimum distance reached before stopping
    public float stoppingDistance;
    // The timer for when enemy can be knocked back
    private float timer;
    // Time knockback is delayed

    // Start is called before the first frame update
    void Start()
    {
        targetDistance = 1f;
        stoppingDistance = 1f;

        // Get the target players component
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //if(type.GetComponent<GoblinGrunt>()) // Might need to add this depending on if multiple types works
        // Get the enemy's speed 
        speed = type.GetComponent<Enemy>().GetMoveSpeed();

        // Retrieving NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();
        // Initializing values and setting goal position
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(goal.position);
    }
    
    // Update is called once per frame
    void Update(){
        // Check if enemy is within range of player
        if(Vector2.Distance(transform.position, target.position) < targetDistance)
        {
            agent.SetDestination(target.position);
            // Check if the stopping distance hasn't been reached
            //if(Vector2.Distance(transform.position, target.position) > stoppingDistance){
                // Move unit towards target player's position
            //    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            //}
        }
    }
}
