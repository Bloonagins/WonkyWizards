using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = PlayerScript.getWorldCursorPoint(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
