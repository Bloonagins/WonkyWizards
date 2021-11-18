using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusDamage : SpellModifier
{
    private int Bonusdamage;

    public BonusDamage(IDamage modifiedDamage, int Bonusdamage) : base(modifiedDamage)
    {
        this.Bonusdamage = Bonusdamage;
    }

    protected override void MODIFY()
    {
        modifiedDamage.setDamage(Bonusdamage);
    }
}
