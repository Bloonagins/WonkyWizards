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
    //----------------------- Variables -----------------------
    public GameObject type; // Game object associated with type
    private Transform player; // The Player target the enemy is trying to follo
    private Transform goal; // The goal object the enemy is trying to reach
    NavMeshAgent agent; // enemy agent component
    private float distanceFromPlayer; // Distance enemy is from the player 
    private float distanceFromGoal; // Distance enemy is from the goal
    private float targetDistance; // Distance where enemy switches to targeting player
    private float stoppingDistance; // The minimum distance reached before stopping
    private float dashDistance; // The distance the GoblinAssasin needs to be in order to dash
    private float timer; // The timer for when enemy can be knocked back
    private bool isDash; // Keep track if GoblinAssasin
    private bool isRanged; // If the enemy is a ranged type
    private bool isGoal; // If enemy is targeting goal
    private bool isPlayer; // If enemy is targeting player

    //----------------------- Start -----------------------
    /* Called before first frame
    */    
    void Start()
    {
        // Initialize values
        distanceFromPlayer = 0f;
        stoppingDistance = 0f;
        isDash = false;
        isRanged = false;
        isPlayer = false;
        isGoal = false;
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); // Get the player's component
        goal = GameObject.FindGameObjectWithTag("Goal").GetComponent<Transform>(); // Get goal's component
        agent = GetComponent<NavMeshAgent>(); // Retrieving NavMeshAgent component

        // Initializing values and setting goal position
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(goal.position);

        // Intializing Enemy speed and targetDistance + stoppingDistance
        if(gameObject.GetComponent<GoblinGrunt>()) { // Set GoblinGrunts speed
            agent.speed = gameObject.GetComponent<GoblinGrunt>().GetMoveSpeed(); 
            agent.acceleration = gameObject.GetComponent<GoblinGrunt>().GetMoveSpeed();
            targetDistance = gameObject.GetComponent<GoblinGrunt>().GetTargetDistance();
            stoppingDistance = gameObject.GetComponent<GoblinGrunt>().GetStoppingDistance();
        }
        else if(gameObject.GetComponent<GoblinWarrior>()) { // Set GoblinWarrior speed
            agent.speed = gameObject.GetComponent<GoblinWarrior>().GetMoveSpeed(); 
            agent.acceleration = gameObject.GetComponent<GoblinWarrior>().GetMoveSpeed();
            targetDistance = gameObject.GetComponent<GoblinWarrior>().GetTargetDistance();
            stoppingDistance = gameObject.GetComponent<GoblinWarrior>().GetStoppingDistance();
        }        
        else if(gameObject.GetComponent<GoblinBerserker>()) { // Set GoblinBerserkers stats
            agent.speed = gameObject.GetComponent<GoblinBerserker>().GetMoveSpeed(); 
            agent.acceleration = gameObject.GetComponent<GoblinBerserker>().GetMoveSpeed();
            targetDistance = gameObject.GetComponent<GoblinBerserker>().GetTargetDistance();
            stoppingDistance = gameObject.GetComponent<GoblinBerserker>().GetStoppingDistance();
        }
        else if(gameObject.GetComponent<GoblinAssassin>()) { // Set GoblinAssasin stats
            agent.speed = gameObject.GetComponent<GoblinAssassin>().GetMoveSpeed(); 
            agent.acceleration = gameObject.GetComponent<GoblinAssassin>().GetMoveSpeed();
            targetDistance = gameObject.GetComponent<GoblinAssassin>().GetTargetDistance();
            dashDistance = gameObject.GetComponent<GoblinAssassin>().GetDashDistance();
            stoppingDistance = gameObject.GetComponent<GoblinAssassin>().GetStoppingDistance();
            isDash = true;
        }
        else if(gameObject.GetComponent<GoblinGiant>()) { // Set GoblinGiant stats
            agent.speed = gameObject.GetComponent<GoblinGiant>().GetMoveSpeed(); 
            agent.acceleration = gameObject.GetComponent<GoblinGiant>().GetMoveSpeed();
            targetDistance = gameObject.GetComponent<GoblinGiant>().GetTargetDistance();
            stoppingDistance = gameObject.GetComponent<GoblinGiant>().GetStoppingDistance();
        }
        else if(gameObject.GetComponent<GoblinArcher>()) { // Set GoblinArcher stats
            agent.speed = gameObject.GetComponent<GoblinArcher>().GetMoveSpeed(); 
            agent.acceleration = gameObject.GetComponent<GoblinArcher>().GetMoveSpeed();
            targetDistance = gameObject.GetComponent<GoblinArcher>().GetTargetDistance();
            stoppingDistance = gameObject.GetComponent<GoblinArcher>().GetStoppingDistance();
            isRanged = true;
        }
    }
    
    //----------------------- Update -----------------------
    /* Update is called once per frame.
    */
        void Update(){
        distanceFromPlayer = Vector2.Distance(transform.position, player.position);
        if(isRanged) {
            distanceFromGoal = Vector2.Distance(transform.position, goal.position);
        }
        // Check if enemy is within range of player
        if(distanceFromPlayer < targetDistance && distanceFromPlayer > stoppingDistance) {
            agent.isStopped = false;
            isPlayer = false;
            isGoal = false;
            agent.SetDestination(player.position);
            transform.eulerAngles = Vector3.forward * calculateVectorAngle(transform.position, player.position);
        }
        else if(isRanged && distanceFromPlayer < stoppingDistance) { // target player
            agent.isStopped = true;
            isPlayer = true;
            transform.eulerAngles = Vector3.forward * calculateVectorAngle(transform.position, player.position);
            if(gameObject.GetComponent<GoblinArcher>().canAttack() && gameObject.GetComponent<GoblinArcher>()){
                gameObject.GetComponent<GoblinArcher>().fireAttack(player);
            }
        }
        else if(isRanged && distanceFromGoal < stoppingDistance+5) { // target goal
            agent.isStopped = true;
            isGoal = true;
            transform.eulerAngles = Vector3.forward * calculateVectorAngle(transform.position, player.position);
            if(gameObject.GetComponent<GoblinArcher>().canAttack() && gameObject.GetComponent<GoblinArcher>()){
                gameObject.GetComponent<GoblinArcher>().fireAttack(goal);
            }
        }
        else {
            agent.isStopped = false;
            isPlayer = false;
            isGoal = false;
            agent.SetDestination(goal.position);
            transform.eulerAngles = Vector3.forward * calculateVectorAngle(transform.position, goal.position);
        }
        // Check if GoblinAssasin and is in range
        if (isDash && gameObject.GetComponent<GoblinAssassin>().canDash() && distanceFromPlayer < dashDistance) {
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

    public bool GetPlayer()
    {
        return isPlayer;
    }
    public bool GetGoal()
    {
        return isGoal;
    }
}
