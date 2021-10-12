/**********************************************
| Game Manager V1.0.0                         |
| Author: Zach Heimbigner, T3                 |
| Description: This program manages the state |
| of the game based on player action. The     |
| Program manages 5 states:                   | 
| - cutscene                                  |
| - setup mode                                |
| - play mode                                 |
| - lose                                      |
| - win                                       |
|                                             |
| Bugs:                                       |
**********************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // package is used to check the scene name in unity

public enum GameState{
    CUTSCENE,
    SETUP,
    PLAY,
    LOSE,
    WIN,
    PAUSE
}

public class GameManager : MonoBehaviour
{
    public static GameState state;
    public static GameManager instance;
    private static bool[,] placementGrid; 
    public static int rows;
    public static int cols; 
    public static string sceneName; // reference to the scene's name

    //---------SINGLETON PATTERN-------------
    void Awake()
    {
        MakeSingleton();
        sceneName = SceneManager.GetActiveScene().name;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void MakeSingleton()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    //--------STATE HANDLING-----------------

    //change states
    public void ChangeState(GameState newState)
    {
        state = newState;
        Game();
    }

    //change game
    private void Game()
    {
        switch(state)
        {
            case GameState.CUTSCENE:
                break;

            case GameState.PLAY:
                Play();
                break;

            case GameState.SETUP:
                Setup();
                break;
            
            case GameState.WIN:
                Win();
                break;
            
            case GameState.LOSE:
                Lose();
                break;

            case GameState.PAUSE:
                Pause();
                break;

            default:
                break;
        }
    }

    //-------------PLAY----------------
    private void Play()
    {
        Debug.Log("STATE1 " + state);
    }

    //-------------SETUP---------------
    private void Setup()
    {
        Debug.Log("STATE2 " + state);
    }

    //--------------WIN----------------
    private void Win()
    {
        Debug.Log("STATE3 " + state);
    }

    //--------------LOSE---------------
    private void Lose()
    {
        Debug.Log("STATE4 " + state);
    }

    //-------------PAUSE---------------
    private void Pause()
    {
        Debug.Log("STATE5 " + state);
    }

    //---------PLACEMENT GRID----------

    public static bool[,] getPlacementGrid()
    {
        return placementGrid;
    }

    public static void setLevelArray (bool[,] levelArray)
    {
        placementGrid = levelArray;
    }
    public static int getCurrentLevel () {
        int level;
        Debug.Log("Current Scene" + sceneName);
        switch (sceneName)
        {
            case "FirstLevel": level = 0; break;
            case "SecondLevel": level = 1; break;
            default: level = -1; break;
            // etc
        }
        return level;
    }
}

