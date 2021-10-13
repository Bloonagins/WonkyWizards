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
    private Transform player;
    // The goal object the enemy is trying to reach
    private Transform goal;
    // enemy agent component
    NavMeshAgent agent;
    // Distance where enemy switches to targeting player
    private float targetDistance;
    // The minimum distance reached before stopping
    private float stoppingDistance;
    // The timer for when enemy can be knocked back
    private float timer;
    // Time knockback is delayed

    // Start is called before the first frame update
    void Start()
    {
        targetDistance = 15f;
        stoppingDistance = 1f;

        // Get the target players component
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        // Get goal's component
        goal = GameObject.FindGameObjectWithTag("Goal").GetComponent<Transform>();
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
        if(Vector2.Distance(transform.position, player.position) < targetDistance) {
            agent.SetDestination(player.position);
        }
        else {
            agent.SetDestination(goal.position);
        }
    }
}
