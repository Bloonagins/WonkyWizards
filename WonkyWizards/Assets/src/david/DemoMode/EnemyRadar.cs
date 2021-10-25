using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRadar : MonoBehaviour
{
    private Transform player;
    private GameObject[] multipleEnemies;
    private Transform closestEnemy;
    private Transform target;
    public bool enemyContact;
    public bool inRange;

    // Start is called before the first frame update
    void Start()
    {
        closestEnemy = null;
        target = null;
        enemyContact = false;
        inRange = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(inRange);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.isTrigger != true && collision.CompareTag("Enemy")) 
        {
            inRange = true;
            if(closestEnemy != null)
            {
                closestEnemy.gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
            }
            closestEnemy = getClosetEnemy();
            target = closestEnemy;
            closestEnemy.gameObject.GetComponent<SpriteRenderer>().material.color = new Color(0, 0.7f, 3, 1);       
            enemyContact = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if(collision.isTrigger != true && collision.CompareTag("Enemy"))
        {
            inRange = false;
            enemyContact = false;
            closestEnemy.gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
        }
    }

    // Function that calculates the position the target will be
    public Vector3 calculateAttack() {
        // Use distance between enemy and player ?
        // float distanceBetween = Vector3.Distance(target.position, player.position);
        // NavMeshAgent agent = target.GetComponent<NavMeshAgent>();
        // agent.speeds
        // Use direction ?
        return target.position;
    }

    public Transform GetTarget()
    {
        return target;
    }

    public bool GetInRange()
    {
        return inRange;
    }

    // Function that returns the transform of the closest enemy in range
    private Transform getClosetEnemy() 
    {
        multipleEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform enemyTransform = null;

        foreach (GameObject go in multipleEnemies) 
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, go.transform.position);
            if(currentDistance < closestDistance) 
            {
                closestDistance = currentDistance;
                enemyTransform = go.transform;
            }
        }
        return enemyTransform;
    }

}
