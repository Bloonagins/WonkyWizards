using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    private static readonly object obj = new object();

    public static SoundManager Instance
    {
        get
        {
            lock (obj)
            {
                if (instance == null)
                {
                    instance = new SoundManager();
                }
                return instance;
            }
        }
    }

    public void Awake()
    {
        // if there is an instance, and it's not this...
        if (instance && instance != this)
        {
            // remove the duplicate instance
            Destroy(this.gameObject);
            return;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
