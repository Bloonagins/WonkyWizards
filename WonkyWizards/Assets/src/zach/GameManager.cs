/**********************************************
| Game Manager V1.0.0                         |
| Author: Zach Heimbigner, T3                 |
| Description: This program manages the state |
| of the game based on player action. The     |
| Program manages 7 states:                   |
| - Main menu                                 | 
| - Settings                                  |
| - level select                              |
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

public enum GameState {
    MAIN_MENU,
    SETTINGS,
    LEVEL_SELECT,
    CUTSCENE,
    SETUP_MODE,
    PLAY_MODE,
    LOSE,
    WIN
}

public delegate void OnStateChangeHandler();

public class GameManager : MonoBehaviour
{
    protected GameManager() {}
    private static GameManager instance = null;
    public event OnStateChangeHandler OnStateChange;
    public GameState gameState { get; private set; }

    public static GameManager Instance{
        get {
            if (GameManager.instance == null){
                DontDestroyOnLoad(GameManager.instance);
                GameManager.instance = new GameManager();
            }
            return GameManager.instance;
        }
    }

    public void SetGameState(GameState state){
        this.gameState = state;
        OnStateChange();
    }

    public void OnApplicationQuit(){
        GameManager.instance = null;
    }
}
