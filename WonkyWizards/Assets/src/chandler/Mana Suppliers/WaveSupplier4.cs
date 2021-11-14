using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSupplier4 : WaveSupplier
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
        setWaveMana(180, 190, 200);
        setWaveSpawner();
    }
}
