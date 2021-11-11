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
    private HashSet<Collider2D> enemies;


    // Start is called before the first frame update
    void Start()
    {
        radarCollider = GetComponent<CircleCollider2D>();
        radarCollider.radius = radius;

        enemies = new HashSet<Collider2D>();

        inRange = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true && col.CompareTag("Enemy"))
        {
            inRange = true;

            enemies.Add(col);

        } //printList();
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            enemies.Remove(col);

            if (enemies.Count == 0) inRange = false;
        } //printList();
    }

    public void setRadius(float r)
    {
        radius = r;

        radarCollider = GetComponent<CircleCollider2D>();
        radarCollider.radius = radius;
    }

    public bool getInRange()
    {
        return inRange;
    }

    public List<GameObject> getEnemies()
    {
        List<GameObject> list = new List<GameObject>();

        foreach (Collider2D enemy in enemies)
        {
            list.Add(enemy.gameObject);
        }
        return list;
    }

    public GameObject GetEnemy(targetingMode mode)
    {
        if (!inRange) return null;

        switch (mode)
        {
            case targetingMode.WEAK: default:    return findWeakest().gameObject;
            //case targetingMode.STRONG:  return findStrongest();
            //case targetingMode.FIRST:   return findFirst();
            //case targetingMode.LAST:    return findLast();
        }
    }

    private Collider2D findWeakest()
    {
        Collider2D weakest = null;
        int lowestHealth = int.MaxValue;

        foreach (Collider2D enemy in enemies)
        {
            if (weakest == null) weakest = enemy;

            if (enemy.GetComponent<Enemy>())
            {
                if (enemy.GetComponent<Enemy>().GetHealth() < lowestHealth)
                {
                    weakest = enemy;
                    lowestHealth = enemy.GetComponent<Enemy>().GetHealth();
                }
            } else if (enemy.GetComponent<GoblinGrunt>())
            {
                if (enemy.GetComponent<GoblinGrunt>().GetHealth() < lowestHealth)
                {
                    weakest = enemy;
                    lowestHealth = enemy.GetComponent<GoblinGrunt>().GetHealth();
                }
            }
        }

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

    private void printList()
    {
        Debug.Log("Updated list:");
        foreach (GameObject enemy in getEnemies())
        {
            Debug.Log(enemy.name);
        }
    }
}