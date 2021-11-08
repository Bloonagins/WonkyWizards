using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSupplier : MonoBehaviour
{
    protected static ManaSupplier singleton;
    protected int wave1Mana;
    protected int wave2Mana;
    protected int wave3Mana;
    protected int wave;

    // Start is called before the first frame update
    void Start()
    {
        initializeMana();
    }

    // Update is called once per frame
    void Update()
    {
        checkToGiveMana();
    }

    public virtual void initializeMana()
    {
        if (singleton == null)
        {
            singleton = new ManaSupplier();
        }
        setWaveMana(120, 130, 140);
    }

    protected static void setWaveMana(int wave1, int wave2, int wave3)
    {
        singleton.wave1Mana = wave1;
        singleton.wave2Mana = wave2;
        singleton.wave3Mana = wave3;
        singleton.wave = 1;
    }

    protected static void checkToGiveMana()
    {
        if (/*WaveSpawner.checkMana()*/false)
        {
            if (singleton.wave == 1)
            {
                PlayerScript.giveMana(singleton.getWave1Mana());
                singleton.wave++;
            }
            else if (singleton.wave == 2)
            {
                PlayerScript.giveMana(singleton.getWave2Mana());
                singleton.wave++;
            }
            else if (singleton.wave == 3)
            {
                PlayerScript.giveMana(singleton.getWave3Mana());
                singleton.wave++;
            }
        }
    }

    public int getWave1Mana()
    {
        return singleton.wave1Mana;
    }

    public int getWave2Mana()
    {
        return singleton.wave2Mana;
    }

    public int getWave3Mana()
    {
        return singleton.wave3Mana;
    }
}
