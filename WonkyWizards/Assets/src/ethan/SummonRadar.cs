using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TargetModeNamespace;

public class SummonRadar : MonoBehaviour
{
    private float radius = 1.0f;
    private bool inRange;

    private CircleCollider2D radarCollider;
    private List<GameObject> enemies;


    // Start is called before the first frame update
    void Start()
    {
        radarCollider = GetComponent<CircleCollider2D>();
        radarCollider.radius = radius;

        inRange = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.isTrigger != true && collision.CompareTag("Enemy"))
        {
            inRange = true;

            enemies.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemies.Remove(collision.gameObject);

            if (enemies.Count == 0) inRange = false;
        }
    }

    public void setRadius(float r)
    {
        radius = r;
        radarCollider.radius = radius;
    }

    public bool getInRange()
    {
        return inRange;
    }

    public List<GameObject> getEnemies()
    {
        return enemies;
    }

    public GameObject GetEnemy(targetingMode mode)
    {
        if (enemies.Count == 0)
            return null;

        switch (mode)
        {
            case targetingMode.WEAK: default:    return findWeakest();
            //case targetingMode.STRONG:  return findStrongest();
            //case targetingMode.FIRST:   return findFirst();
            //case targetingMode.LAST:    return findLast();
        }
    }

    private GameObject findWeakest()
    {
        GameObject weakest = null;
        int lowestHealth = int.MaxValue;

        /*foreach (GameObject enemy in enemies)
        {
            if (weakest == null) weakest = enemy;

            if (enemy.GetComponent<Enemy>.getHealth() < lowestHealth)
            {
                weakest = enemy;
                lowestHealth = enemy.GetComponent<Enemy>.getHealth();
            }
        }*/

        return weakest;
    }
    /*
    private GameObject findSfindStrongest()
    {

    }

    private GameObject findFirst()
    {

    }

    private GameObject findLast()
    {

    }
    */
}