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

using System;
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
    WIN_SWITCH,
    PAUSE
}

public class GameManager : MonoBehaviour
{
    public static GameState state;
    public static GameManager instance;
    private bool[,] placementGrid; 
    public static int rows;
    public static int cols; 
    public static string sceneName; // reference to the scene's name

    public GameManager()
    {
        state = GameState.SETUP;
        placementGrid = new bool[12,12];
    }

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

    private GameManager MakeSingleton()
    {
        if(instance == null)
        {
            instance = this;
        }
        return instance;
    }

    public static GameManager getSingleton()
    {
        return instance;
    }
    //--------STATE HANDLING-----------------
    public static GameState CheckState()
    {
        return state;
    }

    //change states
    public static void ChangeState(GameState newState)
    {
        if (state != newState){
            state = newState;
        }
        Debug.Log(state);
    }

    //---------PLACEMENT GRID----------

    public bool[,] getPlacementGrid()
    {
        return placementGrid;
    }

    public void occupySpace (int x, int y)
    {
        placementGrid[x, y] = false;
    }

    public void setSpaceAvailable (int x, int y)
    {
        placementGrid[x, y] = true;
    }

    public bool checkSpace (int x, int y)
    {
        return placementGrid[x, y];
    }

    public void setLevelArray (bool[,] levelArray)
    {
        placementGrid = levelArray.Clone() as bool[,];
    }
    
    public int getCurrentLevel () {
        int level;
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

