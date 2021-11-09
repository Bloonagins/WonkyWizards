using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSupplier3 : ManaSupplier
{
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

    public override void initializeMana()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        setWaveMana(160, 170, 180);
        setWaveSpawner();
    }
}
