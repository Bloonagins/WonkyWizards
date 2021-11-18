using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public bool propercollision = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Spell")
        {
            if(collision.GetComponent<FireBall>()){
                propercollision = true;
            }else if(collision.GetComponent<MagicMissle>()){
                propercollision = true;
            }else if(collision.GetComponent<AcidSpray>()){
                propercollision = true;
            }else if(collision.GetComponent<IceBeam>()){
                propercollision = true;
            }
        }
    }
}
