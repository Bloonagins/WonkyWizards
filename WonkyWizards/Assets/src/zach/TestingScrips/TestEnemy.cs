using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public bool propercollision = false;
    void FixedUpdate()
    {
        if(GameObject.Find("BlastRadius(Clone)") != null)
        {
            propercollision = true;
        }
    }
}
