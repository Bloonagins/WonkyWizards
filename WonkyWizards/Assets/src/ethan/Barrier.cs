using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : Summon
{
    private const int cost = 10;

    public override int getCost () { return cost; }
}