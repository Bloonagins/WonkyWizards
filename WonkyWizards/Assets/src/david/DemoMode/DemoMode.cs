/**********************************************
| DemoMode V1.0.0                             |
| Author: David Bush, T5                      |
| Description: This contains the script for   |
| to active the Demo Mode and keep track of   |
| it's interactions. It also contains the     |
| singleton pattern.                          |
| Bugs:                                       |
**********************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMode : MonoBehaviour
{
    private bool demoActive; // bool to keep track if game is in demo mode
    private float inputTimer; // Keeps track of time since last input
    public float inputDelay; // Amount of time for input delay
    public bool Fail; // Turn on Fail Case


    // ------------------Singleton Pattern------------------
    // Step 1: Define a private static attribute of the single instance
    private static DemoMode single_instance; 
    // Awake is called first and even if the script is disabled
    private void Awake() 
    {
        CreateSingleton(); // create the singlton instance
    }
    // Step 2: Define a public accessor function to return instance
    public DemoMode CreateSingleton() 
    {
        // HOW TO PREVENT FOR MULTIPLE THREADS?
        // Step 3: "Lazy initialization" 
        if(single_instance == null) // check to see if its the first instance
        {
            single_instance = this;
        }
        return single_instance; // return the singleton instance
    }
    // Step 4: Clients may only use the accessor function to manipulate the Singleton.
    //------------------End of Singleton Pattern------------------

    // Start is called before the first frame update
    void Start()
    {
        demoActive = false;
        inputTimer = 0f;
        inputDelay = 3f;
        Fail = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown || Input.anyKey)
        {
            // Debug.Log("Key entered");
            inputTimer = 0f; // Reset timer

        }    
    }

    void FixedUpdate()
    {
        inputTimer += Time.fixedDeltaTime; // Increment timer
        // Debug.Log(NoInput10Seconds());

        // Check if theres been no input for 10 seconds and game is in playmode
        if(GameManager.CheckState() == GameState.PLAY && NoInput10Seconds()) {
            // Debug.Log("IN PLAY STATE and no input 10 seconds");
            // activate demo mode
            demoActive = true;
        }
        else { 
            // disable demo mode
            demoActive = false;
        }

    }

    // Function to check if timer is greater than the delay
    public bool NoInput10Seconds() 
    {
        return inputTimer >= inputDelay;
    }
    // Function to check if demo mode is active
    public bool GetDemoActive() 
    {
        return demoActive;
    }
    // Function to check if Fail is on
    public bool GetFail() 
    {
        return Fail;
    }

}
