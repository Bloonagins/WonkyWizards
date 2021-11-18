using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleGoalScript : GoalManager
{    
    double dDamageReductionPercent = 0.25;


    public override void Awake()
    {
        base.Awake();

        iGoalHp = 500;
        iGoalMaxHp = 5000;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// This function will damage the castle goal while including its' current damage reduction
    /// </summary>
    public override void GoalTakeDamage(int damage)
    {
        iGoalHp -= (damage - (int)( (double)damage * dDamageReductionPercent ) );
    }

    public override void GoalTakeDamageAnimation()
    {
        //Play this goal's specific taking damage animation
    }
    public override void GoalTakeDamageSound()
    {
        SoundManager.playSound("goal_hit_castle");
    }

}
