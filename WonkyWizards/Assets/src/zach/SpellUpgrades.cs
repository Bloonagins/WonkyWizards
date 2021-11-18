using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage {
    void setDamage(int modifier);
    void apply(IDamageMaker source);
}

public interface IDamageMaker{
    void applyDamage();
}

public class SpellUpgrades
{
}
