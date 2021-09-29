using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenuManager : MonoBehaviour
{


    private bool bIsPaused = false;
    public GameObject PauseMenuObject;


    //Calling this func will attempt to pause/resume the game
    public void PauseOrResumeGame()
    {
        if(bIsPaused == false)
        {
            Time.timeScale = 0f;
            bIsPaused = true;
            PauseMenuObject.SetActive(true);
        }
        else
        {
            PauseMenuObject.SetActive(false);
            Time.timeScale = 1f;
            bIsPaused = false;
        }
    }


    //Calling this function will close the application
    public void ExitGame()
    {
        Debug.Log("Exiting Game\n");
        Application.Quit();
    }

}
