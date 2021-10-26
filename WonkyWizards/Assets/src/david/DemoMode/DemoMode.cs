using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMode : MonoBehaviour
{
    // bool to keep track if game is in demo mode
    private bool demoActive;
    // Keeps track of time since last input
    private float inputTimer;
    // Amount of time for input delay
    private float inputDelay;

    // Start is called before the first frame update
    void Start()
    {
        demoActive = false;
        inputTimer = 0f;
        inputDelay = 3f;
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
            Debug.Log("IN PLAY STATE and no input 10 seconds");
            // activate demo mode
            demoActive = true;
        }
        else { 
            // disable demo mode
            demoActive = false;
        }

    }

    public bool NoInput10Seconds() 
    {
        return inputTimer >= inputDelay;
    }

    public void SetDemoActive(bool d) {
        demoActive = d;
    }

    public bool GetDemoActive() 
    {
        return demoActive;
    }

}
