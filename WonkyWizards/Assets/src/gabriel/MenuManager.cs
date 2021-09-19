using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{


    public void ExitGame()
    {
        Debug.Log("Exiting Game\n");
        Application.Quit();
    }

}
