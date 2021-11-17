using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class OptionsManager : MonoBehaviour
{
    private static readonly object obj = new object();


    private static float fEffectsVolume, fMusicVolume = 0.5f;


    public static float GetEffectsVolume()
    {
        return fEffectsVolume;
    }
    public static float GetMusicVolume()
    {
        return fMusicVolume;
    }
    public static void SetEffectsVolume(float i)
    {
        fEffectsVolume = i;
        //Debug.Log(fEffectsVolume);
    }
    public static void SetMusicVolume(float i)
    {
        fMusicVolume = i;
        //Debug.Log(fMusicVolume);
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
