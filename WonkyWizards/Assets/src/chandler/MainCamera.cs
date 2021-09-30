using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    // transform component of the player (for getting player's coordinates)
    public Transform playerPosition;
    // this camera's camera component
    private Camera mainCam;
    // cursor coordinates
    private Vector3 cursorPoint;

    void Awake()
    {
        // gets a link to this camera's camera component
        mainCam = GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // gets coordinates of the cursor
        cursorPoint = PlayerScript.screenCursorPoint;

        // keeps the cursor's x coordinates within the bounds of the screen
        if (cursorPoint.x < 0)
        {
            cursorPoint.x = 0;
        }
        else if (cursorPoint.x > Screen.width)
        {
            cursorPoint.x = Screen.width;
        }
        // keeps the cursor's y coordinates within the bounds of the screen
        if (cursorPoint.y < 0)
        {
            cursorPoint.y = 0;
        }
        else if (cursorPoint.y > Screen.height)
        {
            cursorPoint.y = Screen.height;
        }

        // transforms the cursor's coordinates to usable points
        cursorPoint = Camera.main.ScreenToWorldPoint(cursorPoint);

        // calculates where the camera should shift based on where the cursor is relative to the player
        float xScale = playerPosition.position.x + (cursorPoint.x - playerPosition.position.x) / 5;
        float yScale = playerPosition.position.y + (cursorPoint.y - playerPosition.position.y) / 3;
        // calculates the amount the camera should zoom out based on how far away the cursor is from the player
        float zScale = Vector3.Distance(cursorPoint, playerPosition.position) / 5;

        // shifts the camera 1/10th of the way towards the location of the cursor from the player
        transform.position = new Vector3(xScale, yScale, transform.position.z);
        // zooms out the camera based on how far away the cursor is from the player
        mainCam.orthographicSize = 10 + zScale;
    }
}
