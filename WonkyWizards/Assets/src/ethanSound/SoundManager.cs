using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    private static readonly object obj = new object();

    private static AudioClip click1, click2, enemy_death1, enemy_death2, enemy_death3, enemy_death4, fire_spell;
    private static AudioSource audioSrc;

    private static int enemyDeath;

    public static SoundManager Instance
    {
        get
        {
            // make it thread-safe
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

    public static SoundManager getInstance()
    {
        return instance;
    }

    public SoundManager()
    {
        // make it thread-safe
        lock (obj)
        {
            // attempt generation
            getInstance();

            // if there's a duplicate, we need to remove it.
            // if there is an instance, and it's not this...
            if (instance && instance != this)
            {
                // remove the duplicate instance
                Destroy(this.gameObject);
                return;
            }
        }
    }

    void Start()
    {
        click1 = Resources.Load<AudioClip>("click1");
        click2 = Resources.Load<AudioClip>("click2");
        enemy_death1 = Resources.Load<AudioClip>("enemy_death1");
        enemy_death2 = Resources.Load<AudioClip>("enemy_death2");
        enemy_death3 = Resources.Load<AudioClip>("enemy_death3");
        enemy_death4 = Resources.Load<AudioClip>("enemy_death4");
        fire_spell = Resources.Load<AudioClip>("fire_spell");

        audioSrc = GetComponent<AudioSource>();
        if (!audioSrc) Debug.Log("audioSrc is null!");

        enemyDeath = 0;
    }

    void Update()
    {
        
    }

    public void playSound(string clip)
    {
        switch (clip)
        {
            case "click1":
                audioSrc.PlayOneShot(click1);
                break;
            case "click2":
                audioSrc.PlayOneShot(click2);
                break;

            case "enemy_death":
                playSound("enemy_death" + (enemyDeath++ % 4) + 1);
                break;
            case "enemy_death1":
                audioSrc.PlayOneShot(enemy_death1);
                break;
            case "enemy_death2":
                audioSrc.PlayOneShot(enemy_death2);
                break;
            case "enemy_death3":
                audioSrc.PlayOneShot(enemy_death3);
                break;
            case "enemy_death4":
                audioSrc.PlayOneShot(enemy_death4);
                break;

            case "fire_spell":
                audioSrc.PlayOneShot(fire_spell);
                break;
            default:
                Debug.Log("[SoundManager] sound \"" + clip + "\" could not be found.");
                break;
        }
    }
}
