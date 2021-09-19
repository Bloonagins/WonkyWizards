using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    // cursor coordinates
    private Vector3 mousePoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // gets the coordinates of the cursor
        mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // teleports the crosshair to the cursor's current position
        transform.position = new Vector3(mousePoint.x, mousePoint.y, transform.position.z);
    }
}
