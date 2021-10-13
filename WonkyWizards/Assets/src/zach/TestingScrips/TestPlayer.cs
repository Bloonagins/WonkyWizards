using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public GameObject Spell;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Spell, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
