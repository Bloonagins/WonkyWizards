using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI PlayerMode;
    public bool CastMode;
    public bool BuildMode;

    private void Awake()
    {
        
    }

    //Calling this funtion will set cast/build bools and set UI text to correspond to those values
    public void UpdatePlayerModeUI()
    {
        if(BuildMode)
        {
            CastMode = true;
            BuildMode = false;
            PlayerMode.text = "Cast Mode";
            PlayerMode.color = new Color32(209, 73, 30, 255);
            //Debug.Log("changed to cast mode");
            //D1491E

        }
        else {
            CastMode = false;
            BuildMode = true;
            PlayerMode.text = "Build Mode";
            PlayerMode.color = new Color32(52, 209, 30, 255);
            //Debug.Log("changed to build mode");
            //34D11E
        }

    }
    public void ChangeCastMode()
    {
        CastMode = !CastMode;
    }
    public void ChangeBuildMode()
    {
        BuildMode = !BuildMode;
    }





}
