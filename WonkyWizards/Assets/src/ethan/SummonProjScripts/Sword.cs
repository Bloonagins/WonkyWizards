using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : SummonProj
{
    public Sword()
    {
        speed = 0;
        damage = 15;
        kockback = 100.0f;
    }

    public override void FixedUpdate()
    {
        // no functionality needed. blank override.
        //base.FixedUpdate();
    }
}
