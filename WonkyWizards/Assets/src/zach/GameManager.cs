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
    public GameState state;
    public static GameManager instance;

    //---------SINGLETON PATTERN-------------
    void Awake()
    {
        MakeSingleton();
        Cursor.lockState = CursorLockMode.Confined;
    }

    void MakeSingleton()
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
        this.state = newState;
        Game();
    }

    //change game
    private void Game()
    {
        switch(this.state)
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
        Debug.Log("STATE1 " + this.state);
    }

    //-------------SETUP---------------
    private void Setup()
    {
        Debug.Log("STATE2 " + this.state);
    }

    //--------------WIN----------------
    private void Win()
    {
        Debug.Log("STATE3 " + this.state);
    }

    //--------------LOSE---------------
    private void Lose()
    {
        Debug.Log("STATE4 " + this.state);
    }

    //-------------PAUSE---------------
    private void Pause()
    {
        Debug.Log("STATE5 " + this.state);
    }
}

