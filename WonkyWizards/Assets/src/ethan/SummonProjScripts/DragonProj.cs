using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonProj : SummonProj
{
    //Vector3 vectorToTarget;
    //float angle;
    //Quaternion q;

    public DragonProj()
    {
        speed = 0;
        damage = 10;
        kockback = 0f;
    }

    public void Start()
    {
        Debug.Log("target.position: " + target.position + ", transform.position: " + transform.position);
        //vectorToTarget = new Vector3();
        Vector3 vectorToTarget = target.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        transform.rotation = q;

        Invoke("killSelf", 0.5f);
    }
}
