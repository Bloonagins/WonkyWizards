using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // teleports the crosshair to the cursor's current position
        transform.position = new Vector3(PlayerScript.worldCursorPoint.x, PlayerScript.worldCursorPoint.y, transform.position.z);
    }
}
