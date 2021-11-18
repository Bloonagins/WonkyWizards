using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    // main SoundManager instance
    private static SoundManager instance;

    //locking object
    private static readonly object obj = new object();

    // sound mixer
    private static AudioMixer mixer;

    // volumes for sfx and music (from -80 to 0)
    private static float SFXVol;
    private static float musicVol;

    // declare audio clip objects
    private static AudioClip
        // wonk sounds
        wonk_hurt, wonk_death, wonk_potion,
        // notifications
        goal_hit, lost, wave_start, wave_complete,
        // spell sounds
        cast_spells, end_spells,
        // summon sounds
        place_summon, delete_summon,
        // enemy sounds
        enemy_grunt1,   enemy_grunt2,   enemy_grunt3,   // random grunts
        enemy_attack1,  enemy_attack2,  enemy_attack3,  // attacking sounds
        enemy_hurt1,    enemy_hurt2,    enemy_hurt3,    // hurt sounds
        enemy_death1,   enemy_death2,   enemy_death3,   // death sounds
        // hud sounds
        click1, click2, hotbar;

    // declare audio source
    [SerializeField]
    private static AudioSource audioSrc;

    // int used for cycleing enemyDeath sound
    private static int enemyGrunt, enemyAttack, enemyHurt, enemyDeath;

    // shortened getter. also is thread-safe
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


    // the SoundManager constructor
    private SoundManager()
    {
        // make it thread-safe
        lock (obj)
        {
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

    static void Start()
    {
        // mixer setup
        mixer = Resources.Load<AudioMixer>("ethanSound/main_mixer");
        if (!mixer) Debug.Log("mixer is null!");

        // wonk sounds
        wonk_hurt = Resources.Load<AudioClip>("wonk_hurt");
        wonk_death = Resources.Load<AudioClip>("wonk_death");
        wonk_potion = Resources.Load<AudioClip>("wonk_potion");

        // notifications
        goal_hit = Resources.Load<AudioClip>("goal_hit");
        lost = Resources.Load<AudioClip>("lost");
        wave_start = Resources.Load<AudioClip>("wave_start");
        wave_complete = Resources.Load<AudioClip>("wave_complete");

        // spell sounds
        // = Resources.Load<AudioClip>("");

        // summon sounds
        place_summon = Resources.Load<AudioClip>("place_summon");
        delete_summon = Resources.Load<AudioClip>("delete_summon");

        // enemy sounds 
        enemy_grunt1 = Resources.Load<AudioClip>("enemy_grunt1");
        enemy_grunt2 = Resources.Load<AudioClip>("enemy_grunt2");
        enemy_grunt3 = Resources.Load<AudioClip>("enemy_grunt3");

        enemy_attack1 = Resources.Load<AudioClip>("enemy_attack1");
        enemy_attack2 = Resources.Load<AudioClip>("enemy_attack2");
        enemy_attack3 = Resources.Load<AudioClip>("enemy_attack3");

        enemy_hurt1 = Resources.Load<AudioClip>("enemy_hurt1");
        enemy_hurt2 = Resources.Load<AudioClip>("enemy_hurt2");
        enemy_hurt3 = Resources.Load<AudioClip>("enemy_hurt3");

        enemy_death1 = Resources.Load<AudioClip>("enemy_death1");
        enemy_death2 = Resources.Load<AudioClip>("enemy_death2");
        enemy_death3 = Resources.Load<AudioClip>("enemy_death3");

        click1 = Resources.Load<AudioClip>("click1");
        click2 = Resources.Load<AudioClip>("click2");
        hotbar = Resources.Load<AudioClip>("hotbar");

        if (!audioSrc) Debug.Log("audioSrc is null!");

        SFXVol = 0f;
        musicVol = 0f;

        enemyDeath = 0;
    }

    static void FixedUpdate()
    {
        setSFXVol();
    }

    public static void playSound(string clip)
    {
        Debug.Log("Attempting to play sound: " + clip);
        switch (clip)
        {
            /*
            case "wonk_hurt": audioSrc.PlayOneShot(wonk_hurt); break;
            case "wonk_death": audioSrc.PlayOneShot(wonk_death); break;
            case "wonk_potion": audioSrc.PlayOneShot(wonk_potion); break;

            case "goal_hit": audioSrc.PlayOneShot(goal_hit); break;
            case "lost": audioSrc.PlayOneShot(lost); break;
            case "wave_start": audioSrc.PlayOneShot(wave_start); break;
            case "wave_complete": audioSrc.PlayOneShot(wave_complete); break;

            case "cast_spells": audioSrc.PlayOneShot(cast_spells); break;
            case "end_spells": audioSrc.PlayOneShot(end_spells); break;

            case "place_summon": audioSrc.PlayOneShot(place_summon); break;
            case "delete_summon": audioSrc.PlayOneShot(delete_summon); break;

            case "enemy_grunt":
                playSound("enemy_grunt" + (enemyGrunt++ % 4) + 1);
                break;
            case "enemy_grunt1": audioSrc.PlayOneShot(enemy_grunt1); break;
            case "enemy_grunt2": audioSrc.PlayOneShot(enemy_grunt2); break;
            case "enemy_grunt3": audioSrc.PlayOneShot(enemy_grunt3); break;

            case "enemy_attack":
                playSound("enemy_attack" + (enemyAttack++ % 4) + 1);
                break;
            case "enemy_attack1": audioSrc.PlayOneShot(enemy_attack1); break;
            case "enemy_attack2": audioSrc.PlayOneShot(enemy_attack2); break;
            case "enemy_attack3": audioSrc.PlayOneShot(enemy_attack3); break;

            case "enemy_hurt":
                playSound("enemy_hurt" + (enemyHurt++ % 4) + 1);
                break;
            case "enemy_hurt1": audioSrc.PlayOneShot(enemy_hurt1); break;
            case "enemy_hurt2": audioSrc.PlayOneShot(enemy_hurt2); break;
            case "enemy_hurt3": audioSrc.PlayOneShot(enemy_hurt3); break;

            case "enemy_death":
                playSound("enemy_death" + (enemyDeath++ % 4) + 1);
                break;
            case "enemy_death1": audioSrc.PlayOneShot(enemy_death1); break;
            case "enemy_death2": audioSrc.PlayOneShot(enemy_death2); break;
            case "enemy_death3": audioSrc.PlayOneShot(enemy_death3); break;

            case "click1": audioSrc.PlayOneShot(click1); break;
            case "click2": audioSrc.PlayOneShot(click2); break;
            case "hotbar": audioSrc.PlayOneShot(hotbar); break; */

            // if no sound matched the requested string, then print an error message.
            default:
                Debug.Log("[SoundManager] sound \"" + clip + "\" could not be found."); break;
        }
    }

    private static void setSFXVol()
    {
        mixer.SetFloat("volume", SFXVol);
    }

    private static void setMusicVol()
    {
        mixer.SetFloat("volume", musicVol);
    }

    public static void updateSFXVol(float v)
    {
        SFXVol = v;
        Debug.Log(SFXVol);
        setSFXVol();
    }

    public static void updateMusicVol(float v)
    {
        musicVol = v;
        Debug.Log(musicVol);
        setMusicVol();
    }
}
