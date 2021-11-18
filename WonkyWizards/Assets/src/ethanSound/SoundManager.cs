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
    private AudioMixer mixer;

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
    private static AudioSource SFXAudioSrc;
    [SerializeField]
    private static AudioSource musicAudioSrc;

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

        if (!SFXAudioSrc) Debug.Log("SFXAudioSrc is null!");
        if (!musicAudioSrc) Debug.Log("musicAudioSrc is null!");

        enemyDeath = 0;
    }

    static void FixedUpdate()
    {
        
    }

    public static void playSound(string clip)
    {
        Debug.Log("Attempting to play sound: " + clip);
        switch (clip)
        {
            /*
            case "wonk_hurt": SFXAudioSrc.PlayOneShot(wonk_hurt); break;
            case "wonk_death": SFXAudioSrc.PlayOneShot(wonk_death); break;
            case "wonk_potion": SFXAudioSrc.PlayOneShot(wonk_potion); break;

            case "goal_hit": SFXAudioSrc.PlayOneShot(goal_hit); break;
            case "lost": SFXAudioSrc.PlayOneShot(lost); break;
            case "wave_start": SFXAudioSrc.PlayOneShot(wave_start); break;
            case "wave_complete": SFXAudioSrc.PlayOneShot(wave_complete); break;

            case "cast_spells": SFXAudioSrc.PlayOneShot(cast_spells); break;
            case "end_spells": SFXAudioSrc.PlayOneShot(end_spells); break;

            case "place_summon": SFXAudioSrc.PlayOneShot(place_summon); break;
            case "delete_summon": SFXAudioSrc.PlayOneShot(delete_summon); break;

            case "enemy_grunt":
                playSound("enemy_grunt" + (enemyGrunt++ % 4) + 1);
                break;
            case "enemy_grunt1": SFXAudioSrc.PlayOneShot(enemy_grunt1); break;
            case "enemy_grunt2": SFXAudioSrc.PlayOneShot(enemy_grunt2); break;
            case "enemy_grunt3": SFXAudioSrc.PlayOneShot(enemy_grunt3); break;

            case "enemy_attack":
                playSound("enemy_attack" + (enemyAttack++ % 4) + 1);
                break;
            case "enemy_attack1": SFXAudioSrc.PlayOneShot(enemy_attack1); break;
            case "enemy_attack2": SFXAudioSrc.PlayOneShot(enemy_attack2); break;
            case "enemy_attack3": SFXAudioSrc.PlayOneShot(enemy_attack3); break;

            case "enemy_hurt":
                playSound("enemy_hurt" + (enemyHurt++ % 4) + 1);
                break;
            case "enemy_hurt1": SFXAudioSrc.PlayOneShot(enemy_hurt1); break;
            case "enemy_hurt2": SFXAudioSrc.PlayOneShot(enemy_hurt2); break;
            case "enemy_hurt3": SFXAudioSrc.PlayOneShot(enemy_hurt3); break;

            case "enemy_death":
                playSound("enemy_death" + (enemyDeath++ % 4) + 1);
                break;
            case "enemy_death1": SFXAudioSrc.PlayOneShot(enemy_death1); break;
            case "enemy_death2": SFXAudioSrc.PlayOneShot(enemy_death2); break;
            case "enemy_death3": SFXAudioSrc.PlayOneShot(enemy_death3); break;

            case "click1": SFXAudioSrc.PlayOneShot(click1); break;
            case "click2": SFXAudioSrc.PlayOneShot(click2); break;
            case "hotbar": SFXAudioSrc.PlayOneShot(hotbar); break; */

            // if no sound matched the requested string, then print an error message.
            default:
                Debug.Log("[SoundManager] sound \"" + clip + "\" could not be found."); break;
        }
    }

    public static void setSFXVol()
    {

    }

    private static void setMusicVol()
    {

    }
}
