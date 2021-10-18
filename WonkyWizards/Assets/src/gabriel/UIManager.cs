using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public GameObject PlayerRef;

    //HUD element object reference list follows:

    public TextMeshProUGUI PlayerMode;

    public TextMeshProUGUI PlayerHealthPercentTXT;
    public Image PlayerHealthCircleIMG;

    public TextMeshProUGUI PlayerManaTXT;
    public Image PlayerManaCircleDepletedIMG;

    public Image DashAbilityIcon;
    public Image FireballAbilityIcon;

    

    /*
    public Image DashIcon;
    public Image DashIcon;
    public Image DashIcon;
    public Image DashIcon;
    */


    
    
    //remove these bools later
    public bool bCastMode;
    public bool bBuildMode;

    public float fLerpSpeed;

    private void Awake()
    {
        bCastMode = false;
        bBuildMode = true;

        //this value is not set in stone and can be modified for differing looks
        fLerpSpeed = 3f * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        UpdatePlayerHeathUI();
        UpdatePlayerManaUI();
        UpdatePlayerModeUI();
        UpdatePlayerCooldownsUI();
    }

    /// <summary>
    /// This function fills each cooldown based HUD element determined by their remaining cooldown time
    /// </summary>
    private void UpdatePlayerCooldownsUI()
    {
        DashAbilityIcon.fillAmount = ((float)PlayerTimer.getDashTimer() / (float)PlayerTimer.getDashCooldown());
        FireballAbilityIcon.fillAmount = ((float)PlayerTimer.getFireballTimer() / (float)PlayerTimer.getFireballCooldown());

    }
    
    /// <summary>
    /// Returns decimal of players' current hp / their max hp
    /// </summary>    
    private float CalcHealthDecimal()
    {   
        return ( (float) PlayerScript.getHP() / (float) PlayerScript.getMAXHP());
    }

    /// <summary>
    /// Calling this function will update all player health UI elements.
    /// These types of functions that update strings are computationally expensive and should not be placed in a regular update function
    /// </summary>
    public void UpdatePlayerHeathUI()
    {
        //float created to avoid extra computation by calling CalcHealthDecimal multiple times in the same function
        //This also avoids potential albiet unlikely discrepancies in the values displayed in fill and text

        float PlayerCurrHPDecimal = CalcHealthDecimal();

        //Debug.Log("pulling player hp" + PlayerCurrHPDecimal);

        PlayerHealthPercentTXT.text = PlayerCurrHPDecimal * 100 + "%";
        PlayerHealthCircleIMG.fillAmount = PlayerCurrHPDecimal;
        HealthColorSetter();
    }

    /// <summary>
    /// Calling this function will update all player mana UI elements.
    /// These types of functions that update strings are computationally expensive and should not be placed in a regular update function
    /// </summary>
    private void UpdatePlayerManaUI()
    {
        int PlayerCurrManaInt = PlayerScript.getMana();
        PlayerManaTXT.text = PlayerCurrManaInt.ToString();
        if (PlayerCurrManaInt == 0)
        {
            PlayerManaCircleDepletedIMG.enabled = true;
        }
        else
        {
            PlayerManaCircleDepletedIMG.enabled = false;
        }
    }

    /// <summary>
    /// This function grabs the correct color gradient value between green and red based on current player hp
    /// </summary>
    private void HealthColorSetter()
    {
        //Debug.Log("setting color");

        Color healthColor = Color.Lerp(Color.red, Color.green, CalcHealthDecimal());
        PlayerHealthCircleIMG.color = healthColor;
        PlayerHealthPercentTXT.color = healthColor;
    }

    /// <summary>
    /// Calling this function will set cast/build bools and set UI text to correspond to those values
    /// </summary>
    public void UpdatePlayerModeUI()
    {   
        if(PlayerScript.isInBuildMode())
        {
            bCastMode = false;
            bBuildMode = true;
            PlayerMode.text = "Build Mode";
            PlayerMode.color = new Color32(52, 209, 30, 255);
            //Debug.Log("changed to build mode");
            //34D11E
        }
        else {
            bCastMode = true;
            bBuildMode = false;
            PlayerMode.text = "Cast Mode";
            PlayerMode.color = new Color32(209, 73, 30, 255);
            //Debug.Log("changed to cast mode");
            //D1491E
        }
    }
    public void ChangeCastMode()
    {
        bCastMode = !bCastMode;
    }
    public void ChangeBuildMode()
    {
        bBuildMode = !bBuildMode;
    }





}
