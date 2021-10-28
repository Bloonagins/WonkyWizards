using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slimeball : Spells
{
    public Slimeball()
    {
        speed = 20.0f;
        DAMAGE = 80;
        COOL_DOWN = 1.1f;
        KNOCK_BACK = 100.0f;
    }
    void Awake()
    {
        Cast();
        Destroy(projectile, 4);
    }
}
