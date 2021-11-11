using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalManager : MonoBehaviour
{

    public GameObject GoalHealthBarCanvasUI;
    public Slider HPslider;
    private GoalManager SelfPrefab;

    protected int iGoalHp;
    protected int iGoalMaxHp;
    
    private bool isGoalDead;


    
    public virtual void Awake()
    {
        SelfPrefab = GetComponent<GoalManager>();

        /*
        //iGoalMaxHp = 10000;
        iGoalHp = iGoalMaxHp;
        isGoalDead = false;
        */
    }
    

    private void FixedUpdate()
    {
        if(iGoalHp <= 0)
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

        UpdateGoalHPSlider();        

    }

    private void UpdateGoalHPSlider()
    {
        HPslider.value = CalcGoalHealthDecimal();
        if (SelfPrefab.GetGoalHp() < SelfPrefab.GetGoalMaxHp() && SelfPrefab.GetGoalHp() > 0)
        {
            GoalHealthBarCanvasUI.SetActive(true);
        }
        else
        {
            GoalHealthBarCanvasUI.SetActive(false);
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

                other.GetComponent<GoblinGrunt>().SetAttack(true);

                //GoalTakeDamageAnimation();

            }
            else if(other.GetComponent<GoblinWarrior>() && other.GetComponent<GoblinWarrior>().canAttack())
            {
                iDamageNum = other.GetComponent<GoblinGrunt>().GetDamage();

                GoalTakeDamage(iDamageNum);           

                other.GetComponent<GoblinGrunt>().SetAttack(true);

            }
            else if(other.GetComponent<GoblinBerserker>() && other.GetComponent<GoblinBerserker>().canAttack())
            {
                iDamageNum = other.GetComponent<GoblinBerserker>().GetDamage();

                GoalTakeDamage(iDamageNum);

                other.GetComponent<GoblinBerserker>().SetAttack(true);
            }
            else if(other.GetComponent<GoblinAssassin>() && other.GetComponent<GoblinAssassin>().canAttack())
            {
                iDamageNum = other.GetComponent<GoblinAssassin>().GetDamage();

                GoalTakeDamage(iDamageNum);

                other.GetComponent<GoblinAssassin>().SetAttack(true);
            }
            else if (other.GetComponent<GoblinGiant>() && other.GetComponent<GoblinGiant>().canAttack())
            {
                iDamageNum = other.GetComponent<GoblinGiant>().GetDamage();

                GoalTakeDamage(iDamageNum);

                other.GetComponent<GoblinGiant>().SetAttack(true);
            }
            else if (other.GetComponent<Arrow>())
            {
                iDamageNum = other.GetComponent<Arrow>().GetDamage();

                GoalTakeDamage(iDamageNum);
            }



        }
        
    }

    public void ResetGoalHpToMax()
    {
        iGoalHp = iGoalMaxHp;
    }
    public virtual void GoalTakeDamage(int damage)
    {
        iGoalHp -= damage;
    }
    public void GoalAddHp(int HpToAdd)
    {
        iGoalHp += HpToAdd;
    }
   

    /// <summary>
    /// Calling this function will return float of goal remaining hp / total hp
    /// </summary>
    float CalcGoalHealthDecimal()
    {
        return ((float)SelfPrefab.GetGoalHp() / (float)SelfPrefab.GetGoalMaxHp());
    }


    public virtual void GoalTakeDamageAnimation()
    {
        //play general goal taking damage animation
    }

}
