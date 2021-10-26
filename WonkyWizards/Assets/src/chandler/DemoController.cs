using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoController : MonoBehaviour
{
    private static DemoMode dm;
    private static EnemyRadar er;
    public GameObject fireball;

    void Start()
    {
        dm = GetComponent<DemoMode>();
        er = GetComponent<EnemyRadar>();
    }

    void Update()
    {
        if (dm.GetDemoActive())
        {
            if (er.GetInRange())
            {
                //cast fireball in direction of closest enemy
            }
        }
    }

    void FixedUpdate()
    {
        if (dm.GetDemoActive())
        {
            /*if (enemy nearby)
            {
                move away from enemy
            }
            else
            {
                move towards goal point
            }*/
        }
    }
}
