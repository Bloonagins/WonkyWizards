using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{


    //Calling this function will close the application
    public void ExitGame()
    {
        Debug.Log("Exiting Game\n");
        Application.Quit();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("PlayerTestLevel");
    }

    //Calling this func will return the mouse to the center of screen width and height
    public void MousePositionZero()
    {
        Vector2 ScreenMiddle = new Vector2();
        ScreenMiddle.x = Screen.width / 2;
        ScreenMiddle.y = Screen.height / 2;

        Mouse.current.WarpCursorPosition(ScreenMiddle);
    }
}
