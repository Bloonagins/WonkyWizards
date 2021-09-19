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
    private Vector3 mousePoint;
    // width and height of the screen
    public static float screenWidth;
    public static float screenHeight;

    void Awake()
    {
        // gets a link to this camera's camera component
        mainCam = GetComponent<Camera>();
        // initializes the variables to hold the size of the screen
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // updates the size of the screen every frame in case the size of the window gets changed
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        // gets coordinates of the cursor
        mousePoint = Input.mousePosition;

        // keeps the cursor's x coordinates within the bounds of the screen
        if (mousePoint.x < 0)
        {
            mousePoint.x = 0;
        }
        else if (mousePoint.x > screenWidth)
        {
            mousePoint.x = screenWidth;
        }
        // keeps the cursor's y coordinates within the bounds of the screen
        if (mousePoint.y < 0)
        {
            mousePoint.y = 0;
        }
        else if (mousePoint.y > screenHeight)
        {
            mousePoint.y = screenHeight;
        }

        // transforms the cursor's coordinates to usable points
        mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

        // calculates where the camera should shift based on where the cursor is relative to the player
        float xScale = playerPosition.position.x + (mousePoint.x - playerPosition.position.x) / 5;
        float yScale = playerPosition.position.y + (mousePoint.y - playerPosition.position.y) / 5;
        // calculates the amount the camera should zoom out based on how far away the cursor is from the player
        float zScale = Vector3.Distance(mousePoint, playerPosition.position) / 5;

        // shifts the camera 1/10th of the way towards the location of the cursor from the player
        transform.position = new Vector3(xScale, yScale, transform.position.z);
        // zooms out the camera based on how far away the cursor is from the player
        mainCam.orthographicSize = 5 + zScale;
    }
}
