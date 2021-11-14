using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSupplier2 : WaveSupplier
{
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
    public override void initializeMana()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        setWaveMana(140, 150, 160);
        setWaveSpawner();
    }
}
