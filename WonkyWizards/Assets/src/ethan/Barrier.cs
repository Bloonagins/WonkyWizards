using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : Summon
{
    private const int cost = 10;
    private const int maxHealth = 100;

    public override int getCost () { return cost; }
    public override int getMaxHealth () { return maxHealth; }
}