using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBarrierExample : MonoBehaviour
{

    public GameObject summon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(summon), PlayerScript.worldCursorPoint, Quaternion.identity);
            Debug.Log(PlayerScript.worldCursorPoint);
        }
    }
}
