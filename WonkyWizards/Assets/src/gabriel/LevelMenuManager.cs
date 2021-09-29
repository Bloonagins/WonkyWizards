using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelMenuManager : MonoBehaviour
{
    private ControlScheme controls;
    private InputAction pause;

    private bool bIsPaused = false;

    //Per level this pause menu object will have to be assigned
    public GameObject PauseMenuObject;
    public GameObject ConfirmMenuObject;
    public GameObject OptionsMenuObject;



    private void Awake()
    {
        controls = new ControlScheme();
    }
    private void OnEnable()
    {
        pause = controls.PlayerDefault.Pause;
        pause.performed += POrResGame;
        pause.Enable();
    }
    private void OnDisable()
    {
        pause.performed -= POrResGame;
        pause.Disable();
    }

    private void POrResGame(InputAction.CallbackContext obj)
    {
        Debug.Log("attempting to pause/unpause game...\n");
        PauseOrResumeGame();
    }

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
            OptionsMenuObject.SetActive(false);
            ConfirmMenuObject.SetActive(false);

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
