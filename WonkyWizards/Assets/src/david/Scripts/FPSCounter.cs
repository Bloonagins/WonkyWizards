using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public Text fpsDisplay;
    

    public float fps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fps = 1 / Time.unscaledDeltaTime;
        fpsDisplay.text = "" + fps;
        //Debug.Log(1/Time.unscaledDeltaTime);
    }

    public float GetFPS()
    {
        return fps;
    }
}
