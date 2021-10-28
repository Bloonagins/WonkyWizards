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
using System;

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
    
    // Distance enemy is from the player 
    private float distanceFrom;
    // Distance where enemy switches to targeting player
    private float targetDistance;
    // The minimum distance reached before stopping
    private float stoppingDistance;
    // The distance the GoblinAssasin needs to be in order to dash
    private float dashDistance;
    // The timer for when enemy can be knocked back
    private float timer;
    // Keep track if GoblinAssasin
    private bool isDash;

    // Start is called before the first frame update
    void Start()
    {
        distanceFrom = 0f;
        stoppingDistance = 1f;
        isDash = false;

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
        
        if(gameObject.GetComponent<GoblinGrunt>()) { // Set GoblinGrunts speed
            agent.speed = gameObject.GetComponent<GoblinGrunt>().GetMoveSpeed(); 
            agent.acceleration = gameObject.GetComponent<GoblinGrunt>().GetMoveSpeed();
            targetDistance = gameObject.GetComponent<GoblinGrunt>().GetTargetDistance();
        }
        else if(gameObject.GetComponent<GoblinWarrior>()) { // Set GoblinWarrior speed
            agent.speed = gameObject.GetComponent<GoblinWarrior>().GetMoveSpeed(); 
            agent.acceleration = gameObject.GetComponent<GoblinWarrior>().GetMoveSpeed();
            targetDistance = gameObject.GetComponent<GoblinWarrior>().GetTargetDistance();
        }        
        else if(gameObject.GetComponent<GoblinBerserker>()) { // Set GoblinBerserkers speed
            agent.speed = gameObject.GetComponent<GoblinBerserker>().GetMoveSpeed(); 
            agent.acceleration = gameObject.GetComponent<GoblinBerserker>().GetMoveSpeed();
            targetDistance = gameObject.GetComponent<GoblinBerserker>().GetTargetDistance();
        }
        else if(gameObject.GetComponent<GoblinAssassin>()) { // Set GoblinAssasin speed
            agent.speed = gameObject.GetComponent<GoblinAssassin>().GetMoveSpeed(); 
            agent.acceleration = gameObject.GetComponent<GoblinAssassin>().GetMoveSpeed();
            targetDistance = gameObject.GetComponent<GoblinAssassin>().GetTargetDistance();
            dashDistance = gameObject.GetComponent<GoblinAssassin>().GetDashDistance();
            isDash = true;
        }
    }
    
    // Update is called once per frame
    void FixedUpdate(){
        distanceFrom = Vector2.Distance(transform.position, player.position);
        // Check if enemy is within range of player
        if(distanceFrom < targetDistance) {
            agent.SetDestination(player.position);
            transform.eulerAngles = Vector3.forward * calculateVectorAngle(transform.position, player.position);
        }
        else {
            agent.SetDestination(goal.position);
            transform.eulerAngles = Vector3.forward * calculateVectorAngle(transform.position, goal.position);
        }
        // Check if GoblinAssasin and is in range
        if (isDash && gameObject.GetComponent<GoblinAssassin>().canDash() && distanceFrom < dashDistance) {
            gameObject.GetComponent<GoblinAssassin>().ApplyDash(player.position);
        }
    }

    public static float calculateVectorAngle(Vector3 origin, Vector3 away)
    {
        Vector3 difference = away - origin;
        float angle = (float)(Math.Atan(difference.y / difference.x) * (180 / Math.PI));

        if (difference.x < 0)
        {
            if (difference.y > 0)
            {
                angle += 180.0f;
            }
            else
            {
                angle -= 180.0f;
            }
        }

        return angle;
    }
}
