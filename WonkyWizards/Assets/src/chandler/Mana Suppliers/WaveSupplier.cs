using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSupplier : MonoBehaviour
{
    protected static WaveSupplier singleton;
    protected int wave1Mana;
    protected int wave2Mana;
    protected int wave3Mana;
    protected int wave;
    protected static readonly Object obj = new Object();
    public GameObject waveSpawner;
    protected WaveSpawner ws;

    // Start is called before the first frame update
    void Start()
    {
        initializeMana();
    }

    // Update is called once per frame
    void Update()
    {
        checkToResupply();
    }

    // initializes values for the wave supplier
    public virtual void initializeMana()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        setWaveMana(120, 130, 140);
        setWaveSpawner();
    }

    // sets the amount of mana that the player gets for each wave
    protected static void setWaveMana(int wave1, int wave2, int wave3)
    {
        lock (obj)
        {
            singleton.wave1Mana = wave1;
            singleton.wave2Mana = wave2;
            singleton.wave3Mana = wave3;
            singleton.wave = 1;
        }
    }

    // initializes the link to the wave spawner so the player can check to see if it has been given mana yet or not
    protected void setWaveSpawner()
    {
        lock (obj)
        {
            singleton.ws = waveSpawner.GetComponent<WaveSpawner>();
        }
    }

    // called every frame update so see if the player should be given mana and hp due to the level entering a new wave
    protected static void checkToResupply()
    {
        lock (obj)
        {
            if (!singleton.ws.checkMana())
            {
                if (singleton.wave == 1)
                {
                    PlayerScript.giveMana(singleton.getWave1Mana());
                    singleton.ws.addedMana(true);
                    singleton.wave++;
                }
                else if (singleton.wave == 2)
                {
                    PlayerScript.giveMana(singleton.getWave2Mana());
                    singleton.ws.addedMana(true);
                    singleton.wave++;
                }
                else if (singleton.wave == 3)
                {
                    PlayerScript.giveMana(singleton.getWave3Mana());
                    singleton.ws.addedMana(true);
                    singleton.wave++;
                }
                PlayerScript.resetPlayerHP();
            }
        }
    }

    // returns the mana given for wave 1
    public int getWave1Mana()
    {
        return singleton.wave1Mana;
    }

    // returns the mana given for wave 2
    public int getWave2Mana()
    {
        return singleton.wave2Mana;
    }

    // returns the mana given for wave 3
    public int getWave3Mana()
    {
        return singleton.wave3Mana;
    }
}
