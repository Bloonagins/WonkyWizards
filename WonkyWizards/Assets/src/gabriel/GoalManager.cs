using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    private int iGoalHp;




    public int GetGoalHp()
    {
        return iGoalHp;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        if (collision.gameObject.tag == "Enemy")
        {
            int iDamageNum;
            //if(other.GetComponent<GoblinGrunt>() && other.GetComponent<GoblinGrunt>().GetCanAttack())
            {
                iDamageNum = other.GetComponent<GoblinGrunt>().GetDamage();
                GoalTakeDamage(iDamageNum);
                
                //GoalTakeDamageAnimation();

            }
            //else if (other.GetComponent<GoblinGrunt>())
            {

            }
            //else if (other.GetComponent<GoblinGrunt>())
            {

            }
            //else if (other.GetComponent<GoblinGrunt>())
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
