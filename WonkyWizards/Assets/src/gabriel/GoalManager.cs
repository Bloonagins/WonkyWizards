using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    private int iGoalHp;
    private int iGoalMaxHp = 10000;
    
    private bool isGoalDead;


    /*
    public void Awake()
    {
        //iGoalMaxHp = 10000;
        iGoalHp = iGoalMaxHp;
        isGoalDead = false;
    }
    */

    private void FixedUpdate()
    {
        if(iGoalHp < 0)
        {
            iGoalHp = 0;
            isGoalDead = true;
            //Debug.Log("hp lower bound check\n");
        }

        if (iGoalHp > iGoalMaxHp)
        {
            iGoalHp = iGoalMaxHp;
            //Debug.Log("hp upper bound check\n");
        }
    }


    public int GetGoalHp()
    {
        return iGoalHp;
    }
    public int GetGoalMaxHp()
    {
        return iGoalMaxHp;
    }
    public bool GetIsGoalDead()
    {
        return isGoalDead;
    }

    
    private void Start()
    {
        //iGoalMaxHp = 10000;
        iGoalHp = iGoalMaxHp;
        isGoalDead = false;
    }
    


    void OnTriggerStay2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        
        if (collision.gameObject.tag == "Enemy")
        {            
            int iDamageNum;

            if(other.GetComponent<GoblinGrunt>() && other.GetComponent<GoblinGrunt>().canAttack())
            {
                //Debug.Log("collided 3");

                iDamageNum = other.GetComponent<GoblinGrunt>().GetDamage();

                GoalTakeDamage(iDamageNum);

                //Debug.Log(other.GetComponent<GoblinGrunt>().SetAttack());

                other.GetComponent<GoblinGrunt>().SetAttack(true); ;

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

    public void ResetGoalHpToMax()
    {
        iGoalHp = iGoalMaxHp;
    }
    public void GoalTakeDamage(int damage)
    {
        iGoalHp -= damage;
    }
    public void GoalAddHp(int HpToAdd)
    {
        iGoalHp += HpToAdd;
    }


    public void GoalTakeDamageAnimation()
    {
        //play goal taking damage animation
    }

}
