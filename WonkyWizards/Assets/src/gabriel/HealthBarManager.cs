using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{

    public GameObject HealthBarUI;
    private GoblinGrunt SelfPrefab;
    public Slider HPslider;


    // Start is called before the first frame update
    void Awake()
    {
        SelfPrefab = GetComponent<GoblinGrunt>();

    }
    private void Start()
    {
        HPslider.value = CalcHealthDecimal();
    }


    // Update is called once per frame
    void Update()
    {
        
        if (SelfPrefab.GetHealth() < 200)
        {
            HealthBarUI.SetActive(true);
        }

    }

    //Calling this func will return float of remaining hp / total hp
    float CalcHealthDecimal()
    {
        return ( SelfPrefab.GetHealth() / 200 );
    }
}
