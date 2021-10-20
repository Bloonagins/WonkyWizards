using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssassinHealthBarManager : MonoBehaviour
{

    public GameObject HealthBarUI;
    private GoblinAssassin SelfPrefab;
    public Slider HPslider;


    // Start is called before the first frame update
    void Awake()
    {
        SelfPrefab = GetComponent<GoblinAssassin>();
    }
    private void Start()
    {
        HPslider.value = CalcHealthDecimal();
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        //add pause checker to avoid unnecessary computation while paused

        HPslider.value = CalcHealthDecimal();
        if (SelfPrefab.GetHealth() < SelfPrefab.GetMaxHealth() && SelfPrefab.GetHealth() > 0)
        {
            HealthBarUI.SetActive(true);
        }
        else
        {
            HealthBarUI.SetActive(false);
        }

    }

    /// <summary>
    /// Calling this function will return float of enemy remaining hp / total hp
    /// </summary>
    float CalcHealthDecimal()
    {
        //lost an hour of my time due to int truncation debugging AHHHHHHHHHHH why am I coding at 1am

        //Debug.Log("trying to calc enemy hp as ");
        //Debug.Log(SelfPrefab.GetHealth() / 200f);
        return ((float)SelfPrefab.GetHealth() / (float)SelfPrefab.GetMaxHealth());
    }
}
