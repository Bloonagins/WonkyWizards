using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadar : MonoBehaviour
{
    private GameObject[] multipleEnemies;
    public Transform closestEnemy;
    public bool enemyContact;

    // Start is called before the first frame update
    void Start()
    {
        closestEnemy = null;
        enemyContact = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.isTrigger != true && collision.CompareTag("Enemy")) 
        {
            if(closestEnemy != null)
            {
                closestEnemy.gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
            }
            closestEnemy = getClosetEnemy();
            closestEnemy.gameObject.GetComponent<SpriteRenderer>().material.color = new Color(0, 0.7f, 3, 1);       
            enemyContact = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if(collision.isTrigger != true && collision.CompareTag("Enemy"))
        {
            enemyContact = false;
            closestEnemy.gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
        }
    }

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
