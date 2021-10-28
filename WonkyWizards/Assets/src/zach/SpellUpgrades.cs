using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellUpgrades
{
    public static SpellUpgrades instance;
    public bool[,] Upgrades = {{true,false,false,false},
                               {true,false,false,false},
                               {true,false,false,false},
                               {true,false,false,false}};
    //---------SINGLETON PATTERN-------------
    void Awake()
    {
        MakeSingleton();
    }

    private SpellUpgrades MakeSingleton()
    {
        if(instance == null)
        {
            instance = this;
        }
        return instance;
    }
}
