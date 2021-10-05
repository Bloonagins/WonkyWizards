using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI PlayerMode;
    public TextMeshProUGUI PlayerHealthPercentTXT;

    public Image PlayerHealthCircleIMG;
    public GameObject PlayerRef;

    public bool bCastMode;
    public bool bBuildMode;

    public float fLerpSpeed;

    private void Awake()
    {
        bCastMode = false;
        bBuildMode = true;
        fLerpSpeed = 3f * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        UpdatePlayerHeathUI();
        UpdatePlayerModeUI();
    }

    ///returns decimal of players' current hp / their max hp
    private float CalcHealthDecimal()
    {   
        return ( (float) PlayerScript.getHP() / (float) PlayerScript.getMAXHP());
    }

    ///Calling this function will update all player health UI elements - should be called whenever the player hp value is anticipated to changed
    ///ie Healing, Enemy Interaction...etc</summary>
    ///These types of functions that update strings are computationally expensive and should not be placed in an update function
    
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

    private void HealthColorSetter()
    {
        //Debug.Log("setting color");

        Color healthColor = Color.Lerp(Color.red, Color.green, CalcHealthDecimal());
        PlayerHealthCircleIMG.color = healthColor;
        PlayerHealthPercentTXT.color = healthColor;
    }
    ///Calling this function will set cast/build bools and set UI text to correspond to those values
    public void UpdatePlayerModeUI()
    {    

        if(PlayerScript.inBuildMode)
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
