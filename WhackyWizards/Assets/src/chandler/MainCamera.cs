using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform playerPosition;
    // mouse position
    private static Vector3 mousePoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // gets the coordinates of the mouse cursor
        mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // uncomment this once tilemaps get added so that the camera following the player will be visible
        this.transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y, this.transform.position.z);
    }

    public static Vector3 getMousePoint()
    {
        return mousePoint;
    }
}
