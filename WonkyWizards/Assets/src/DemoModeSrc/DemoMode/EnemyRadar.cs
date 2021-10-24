using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadar : MonoBehaviour
{
    private GameObject[] multipleEnemies;

    // private GameObject player;
    // private PlayerScript ps;
    public Transform closestEnemy;
    public Transform target;
    public bool enemyContact;
    public bool inRange;

    // Start is called before the first frame update
    void Start()
    {
        closestEnemy = null;
        target = null;
        enemyContact = false;
        inRange = false;
        //player = GameObject.FindGameObjectWithTag("Player");
        //ps = player.GetComponent<PlayerScript>();
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
            // if(demoActive)
                // setScreenCursorPoint(calculateAttack(closestEnemy));
                // Send attack
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

    public Vector3 calculateAttack() {
        // Use distance between enemy and player ?
        // Use direction ?
        return target.position;
    }

    public Transform GetTarget()
    {
        return target;
    }

    // Move player away from enemy if active target
    // rb.AddForce(target.position - player.position, force * -1.0f, ForceMode2D.Impulse)
    
    // If no active target move player towards goal.position

    public Transform getClosetEnemy() 
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
