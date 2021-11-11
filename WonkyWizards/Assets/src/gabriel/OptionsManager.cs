using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class OptionsManager : MonoBehaviour
{
    private static readonly object obj = new object();


    private double dEffectsVolume, dMusicVolume = 0.5;


    public double GetEffectsVolume()
    {
        return dEffectsVolume;
    }
    public double GetMusicVolume()
    {
        return dMusicVolume;
    }
    public void SetEffectsVolume(double i)
    {
        dEffectsVolume = i;
    }
    public void SetMusicVolume(double i)
    {
        dMusicVolume = i;
    }
    private OptionsManager()
    {
    }

    private static OptionsManager instance = null;

    public static OptionsManager GetInstance
    {
        get
        {
            if (instance == null)
            {
                lock (obj)
                {
                    if (instance == null)
                        instance = new OptionsManager();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        //Debug.Log("hi!");
    }
}
