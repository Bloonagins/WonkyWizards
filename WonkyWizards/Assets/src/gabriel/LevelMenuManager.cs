using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelMenuManager : MonoBehaviour
{
    private ControlScheme controls;
    private InputAction PauseAction;
    private InputAction ReadyAction;
    private InputAction WaveScreenAction;



    //Per level this pause menu object will have to be assigned
    public GameObject PlayerHUDObject;

    public GameObject PauseMenuObject;
    public GameObject ConfirmMenuObject;
    public GameObject OptionsMenuObject;

    public bool bGameWasInSetup;

    private void Awake()
    {
        controls = new ControlScheme();
    }

    private void OnEnable()
    {
        PauseAction = controls.PlayerDefault.Pause;
        PauseAction.performed += POrResGame;
        ReadyAction = controls.PlayerDefault.ReadyUp;
        ReadyAction.performed += ReadyUpState;
        WaveScreenAction = controls.PlayerDefault.WaveInfo;
        WaveScreenAction.performed += DisplayWaveInfo;


        WaveScreenAction.Enable();
        ReadyAction.Enable();
        PauseAction.Enable();
    }

    

    private void OnDisable()
    {
        PauseAction.performed -= POrResGame;
        PauseAction.Disable();

        ReadyAction.performed -= ReadyUpState;
        PauseAction.Disable();

        WaveScreenAction.performed -= DisplayWaveInfo;
        WaveScreenAction.Disable();
    }
    private void DisplayWaveInfo(InputAction.CallbackContext obj)
    {
        //Debug.Log("attempting to display wave info\n");

    }

    private void ReadyUpState(InputAction.CallbackContext obj)
    {
        //Debug.Log("attempting to ready up...\n");
        ReadyUpGame();
    }

    private void POrResGame(InputAction.CallbackContext obj)
    {
        //Debug.Log("attempting to pause/unpause game...\n");
        PauseOrResumeGame();
    }

    /// <summary>
    /// Calling this function will attempt to set the gamestate to PLAY
    /// </summary>
    public void ReadyUpGame()
    {
        if(GameManager.CheckState() == GameState.SETUP)
        {
            GameManager.ChangeState(GameState.PLAY);
            PlayerScript.setBuildMode(false);
            bGameWasInSetup = false;
        }
    }


    /// <summary>
    /// Calling this func will attempt to pause/resume the game based on current game state
    /// </summary>
    public void PauseOrResumeGame()
    {
        //Debug.Log("trying to switch pause state\n");

        if (GameManager.CheckState() == GameState.SETUP)
        {
            //Debug.Log("trying to switch pause state")
            PauseGame();

            bGameWasInSetup = true;

            GameManager.ChangeState(GameState.PAUSE);
        }
        else if (GameManager.CheckState() == GameState.PLAY)
        {
            //Debug.Log("trying to switch pause state")
            PauseGame();

            GameManager.ChangeState(GameState.PAUSE);
            bGameWasInSetup = false;
        }        
        else if (GameManager.CheckState() == GameState.PAUSE)
        {
            UnPauseGame();

            if (bGameWasInSetup)
            {
                GameManager.ChangeState(GameState.SETUP);

                bGameWasInSetup = true;
            }
            else
            {
                GameManager.ChangeState(GameState.PLAY);
                
                bGameWasInSetup = false;
            }
        }
        else { }
    }
    /// <summary>
    /// Sets correct UI elements to be displayed when paused.
    /// Does NOT set GameState
    /// </summary>
    private void PauseGame()
    {   
        Time.timeScale = 0f;
        
        PauseMenuObject.SetActive(true);

        PlayerHUDObject.SetActive(false);        
    }
    /// <summary>
    /// Sets correct UI elements to be displayed when unpaused.
    /// Does NOT set GameState
    /// </summary>
    private void UnPauseGame()
    {
        PauseMenuObject.SetActive(false);
        OptionsMenuObject.SetActive(false);
        ConfirmMenuObject.SetActive(false);

        PlayerHUDObject.SetActive(true);

        Time.timeScale = 1f;
    }

    /// <summary>
    /// Calling this function will close the application
    /// </summary>
    public void ExitGame()
    {
        //Debug.Log("Exiting Game\n");
        Application.Quit();
    }









}
