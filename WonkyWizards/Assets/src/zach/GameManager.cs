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
    public static bool[,] placementGrid; 
    public static int rows;
    public static int cols; 
    public static Scene currentScene; // reference variable to the current scene
    public static string sceneName; // reference to the scene's name

    //---------SINGLETON PATTERN-------------
    void Awake()
    {
        MakeSingleton();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        currentScene = SceneManager.GetActiveScene(); // get the current loaded scene
        sceneName = currentScene.name;
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

    public static int getCurrentLevel () {
        int level;
        switch (sceneName)
        {
            case "Level_1": level = 0; break;
            case "Level_2": level = 1; break;
            default: level = -1; break;
            // etc
        }
        return level;
    }
}

