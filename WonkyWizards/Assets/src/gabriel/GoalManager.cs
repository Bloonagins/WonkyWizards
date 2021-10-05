using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public int iGoalHp;

    


    public int GetGoalHp()
    {
        return iGoalHp;
    }


    private void Start()
    {
        iGoalHp = 10000;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        
        if (collision.gameObject.tag == "Enemy")
        {
            
            int iDamageNum;
            if(other.GetComponent<GoblinGrunt>() && other.GetComponent<GoblinGrunt>().canAttack())
            {
                Debug.Log("collided 3");

                iDamageNum = other.GetComponent<GoblinGrunt>().GetDamage();

                GoalTakeDamage(iDamageNum);

                Debug.Log(other.GetComponent<GoblinGrunt>().attackConnected);

                other.GetComponent<GoblinGrunt>().attackConnected = true;

                //GoalTakeDamageAnimation();

            }
            //else //if(other.GetComponent<GoblinGrunt>() && other.GetComponent<GoblinGrunt>().GetCanAttack())
            {

            }
            //else //if(other.GetComponent<GoblinGrunt>() && other.GetComponent<GoblinGrunt>().GetCanAttack())
            {

            }
            //else //if(other.GetComponent<GoblinGrunt>() && other.GetComponent<GoblinGrunt>().GetCanAttack())
            {

            }

          

        }
        
    }

    public void GoalTakeDamage(int damage)
    {
        iGoalHp -= damage;

    }

    public void GoalTakeDamageAnimation()
    {
        //play goal taking damage animation
    }

}
