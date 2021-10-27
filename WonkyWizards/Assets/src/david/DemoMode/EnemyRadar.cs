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
    private static DemoMode demo;
    private bool enemyContact;
    private bool inRange;
    private int minVal;
    private int maxVal;

    // Start is called before the first frame update
    void Start()
    {
        closestEnemy = null;
        target = null;
        enemyContact = false;
        inRange = false;
        minVal = 2;
        maxVal = 6;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        demo = GetComponent<DemoMode>();
    }

    // Function that checks when collsion has entered
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
            //closestEnemy.gameObject.GetComponent<SpriteRenderer>().material.color = new Color(0, 0.7f, 3, 1);       
            enemyContact = true;
        }
    }
    // Function checks when collision has exited
    private void OnTriggerExit2D(Collider2D collision) 
    {
        if(collision.isTrigger != true && collision.CompareTag("Enemy"))
        {
            inRange = false;
            enemyContact = false;
            //closestEnemy.gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
        }
    }

    // Function that calculates the position the target will be
    public Vector3 calculateAttack() {
        Vector3 attack = target.position;
        if(demo.GetFail()) {
            attack += new Vector3 (Random.Range(minVal, maxVal), Random.Range(minVal, maxVal), Random.Range(minVal, maxVal));
        }
        return attack;
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
