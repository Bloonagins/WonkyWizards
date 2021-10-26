using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DemoController : MonoBehaviour
{
    private static DemoMode dm;
    private static EnemyRadar er;
    private static Rigidbody2D rb;
    public Transform demoPoint;
    public GameObject fireball;

    void Start()
    {
        dm = GetComponent<DemoMode>();
        er = GetComponentInChildren<EnemyRadar>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (dm.GetDemoActive())
        {
            PlayerScript.setBuildMode(false);
            if (er.GetInRange())
            {
                transform.eulerAngles = Vector3.forward * PlayerScript.calculateVectorAngle(transform.position, er.GetTarget().position);
                if (PlayerTimer.canCast(1) && GameManager.CheckState() != GameState.PAUSE)
                {
                    Instantiate(fireball, transform.position, Quaternion.Euler(Vector3.Normalize(er.GetTarget().position - transform.position)));
                    PlayerTimer.activateSpellCooldown(1);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (dm.GetDemoActive())
        {
            if (er.GetInRange())
            {
                rb.AddForce(Vector3.Normalize(transform.position - er.GetTarget().position) * 30, ForceMode2D.Impulse);
            }
            else
            {
                if (transform.position != demoPoint.position)
                {
                    rb.AddForce(Vector3.Normalize(demoPoint.position - transform.position) * 30, ForceMode2D.Impulse);
                }
            }
        }
    }
}
