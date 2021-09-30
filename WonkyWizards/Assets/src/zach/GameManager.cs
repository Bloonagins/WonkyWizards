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
            case GameState.PLAY:
                Play();
                break;

            case GameState.SETUP:
                Setup();
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
}

