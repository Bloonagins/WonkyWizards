using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI PlayerMode;
    public bool CastMode;
    public bool BuildMode;

    public TextMeshProUGUI PlayerHealthPercentTXT;
    public Image PlayerHealthCircleIMG;

    public GameObject PlayerRef;


    private void Awake()
    {
        CastMode = false;
        BuildMode = true;
    }

    private void FixedUpdate()
    {
        
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

            PlayerHealthPercentTXT.text = (int)PlayerCurrHPDecimal + "%";
            PlayerHealthCircleIMG.fillAmount = PlayerCurrHPDecimal;
        }
    ///Calling this funtion will set cast/build bools and set UI text to correspond to those values
    public void UpdatePlayerModeUI()
    {

    

        if(BuildMode)
        {
            CastMode = true;
            BuildMode = false;
            PlayerMode.text = "Cast Mode";
            PlayerMode.color = new Color32(209, 73, 30, 255);
            //Debug.Log("changed to cast mode");
            //D1491E

        }
        else {
            CastMode = false;
            BuildMode = true;
            PlayerMode.text = "Build Mode";
            PlayerMode.color = new Color32(52, 209, 30, 255);
            //Debug.Log("changed to build mode");
            //34D11E
        }

    }
    public void ChangeCastMode()
    {
        CastMode = !CastMode;
    }
    public void ChangeBuildMode()
    {
        BuildMode = !BuildMode;
    }





}
