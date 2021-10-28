using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Wave
{
    public string waveName;
    public int numberOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves; // array of waves
    public Transform[] spawnPoints; // array of spawn points 

    public Wave currentWave; // current wave info 
    private int currentWaveNumber; // current wave number variable that is incremented when a wave is finished
    private bool canSpawn = true; 
    private bool baked; // variable to check if nav mesh has been baked before each wave starts
    private float nextSpawnTime, currentSpawnTime; // timer variables

    public NavMeshSurface2d Surface2D; // navmesh component variable

    // --- Start of Singleton Pattern --- \\
    public static WaveSpawner instance; // for singleton pattern
    private void Awake()
    {
        MakeSingleton();
    }
    private WaveSpawner MakeSingleton()
    {
        if (instance == null) // if the instance is equal to null (has not been run yet)
        {
            instance = this; // change the instance to this instance 
        }
        return instance;
    }
    // --- End of Singleton Pattern --- \\
    

    private void Start()
    {
        currentSpawnTime = 0.0f; // timer to track when the next enemy spawns
        nextSpawnTime = 3.0f; // countdown in seconds till next enemy spawn
        baked = false;
    }

    private void FixedUpdate()
    {
        currentWave = waves[currentWaveNumber]; // set the current wave
        // check gamestate
        if (GameManager.CheckState() == GameState.PLAY)
        {
            if (currentSpawnTime > 0)
            {
                currentSpawnTime -= Time.fixedDeltaTime; // decrement the timer
            }
            else
            {
                if(!baked)
                {
                    //Debug.Log(baked);
                    baked = true;
                    Surface2D.BuildNavMeshAsync();

                }
                SpawnWave();
                if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !canSpawn)
                {
                    if (1 < currentWaveNumber)
                    {
                        GameManager.ChangeState(GameState.WIN);
                    }
                    else
                    {
                        GameManager.ChangeState(GameState.SETUP); // change the gamestate to setup
                        currentWaveNumber++; // increment the current wave variable
                        canSpawn = true; // set canSpawn to true to wait for player to begin next wave
                        baked = false; // reset baked variable to false to re-bake after player hits play and before the first enemy spawns
                    }
                }
                currentSpawnTime += nextSpawnTime; // increment the time
            }
        }
    }

    void SpawnWave()
    {
        if(canSpawn) 
        {
            Instantiate(currentWave.typeOfEnemies[0], spawnPoints[0].position, Quaternion.identity);
            nextSpawnTime = 3.0f;
            currentWave.numberOfEnemies--;
            if(currentWave.numberOfEnemies == 0)
            {
                canSpawn = false;
            }
        }
    }
}
