using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonProj : SummonProj
{

    public void Start()
    {
        speed = 0;
        damage = 1;
        kockback = 0f;

        Invoke("killSelf", 0.04f);
    }
}
