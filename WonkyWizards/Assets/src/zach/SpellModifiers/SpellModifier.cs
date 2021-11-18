using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellModifier : IDamage
{
    protected IDamage modifiedDamage;

    public SpellModifier(IDamage modifiedDamage)
    {
        this.modifiedDamage = modifiedDamage;
    }

    public void apply(IDamageMaker source){
        if (!modifiedDamage.Equals(null)){
            MODIFY();
        }
    }

    public void setDamage(int modifier){

    }

    protected abstract void MODIFY();
}
