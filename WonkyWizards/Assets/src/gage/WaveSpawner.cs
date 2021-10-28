using UnityEngine;
using UnityEditor;
//using UnityEditor.AI;
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
    public Wave[] waves;
    public Transform[] spawnPoints;

    public Wave currentWave;
    private int currentWaveNumber = 1;
    private bool canSpawn = true;
    private float nextSpawnTime, currentSpawnTime;
    private bool baked;

    public NavMeshSurface2d Surface2D;

    //private float timer;
    //private GameObject GoblinGrunt = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/src/david/Prefab/GoblinGrunt.prefab");

    private void Start()
    {
        currentSpawnTime = 0.0f; // timer to track when the next enemy spawns
        nextSpawnTime = 3.0f; // countdown in seconds till next enemy spawn
        baked = false;
    }

    private void FixedUpdate()
    {

        currentWave = waves[currentWaveNumber];
        // check gamestate
        if (GameManager.CheckState() == GameState.PLAY)
        {
            //Debug.Log("check");
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
            //spawnedEnemies++;
            nextSpawnTime = 3.0f;
            currentWave.numberOfEnemies--;
            if(currentWave.numberOfEnemies == 0)
            {
                canSpawn = false;
            }
        }
    }
}

/*
public class WaveSpawner : MonoBehaviour
{
    private int currentLevel;
    private WaveSpawnerData waveData;
    private WaveSpawnerData wave1;
    private WaveSpawnerData wave2;
    private WaveSpawnerData wave3;
    private WaveSpawnerData wave4;
    private WaveSpawnerData wave5;
    private WaveSpawnerData wave6;
    private WaveSpawnerData wave7;
    private WaveSpawnerData wave8;
    private WaveSpawnerData wave9;
    private WaveSpawnerData wave10;


    void Start()
    {
        currentLevel = GameManager.getCurrentLevel();
        LoadLevelWaves(currentLevel);
    }

    void LoadLevelWaves(int currentLevel)
    {
        switch (currentLevel)
        {
            case 0:
                LoadLevel1();
                break;
            case 1:
                LoadLevel2();
                break;
            default:
                Debug.Log("Current level invalid while loading current level enemy waves");
                break;
        }
    }

    void LoadLevel1()
    {
        this.wave1 = new WaveSpawnerData("Wave1", 3.0f, 3, 0, 0, 0, 0);
        this.wave2 = new WaveSpawnerData("Wave2", 3.0f, 5, 0, 0, 0, 0);
        this.wave3 = new WaveSpawnerData("Wave3", 3.0f, 10, 0, 0, 0, 0);
        this.wave4 = new WaveSpawnerData("Wave4", 3.0f, 20, 0, 0, 0, 0);
        this.wave5 = new WaveSpawnerData("Wave5", 3.0f, 20, 0, 0, 0, 0);        
    }

    void LoadLevel2()
    {
        
    }
}
*/