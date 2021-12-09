/* Name: Gage Nardi
 * WaveSpawner.cs
 * Wonky Wizards -- Bloonagins
 * Description: 
 * The script first waits for the gamestate to change from setup phase to play phase. 
 * The spawner then spawns a number of enemies with a timer until the last enemy is killed.
 * Then the phase is changed to setup or Win/Lose depending on if the last wave was completed and if you won or lost
 */

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
    public bool bossWave;
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves; // array of waves
    public Wave currentWave; // current wave info 
    public Transform[] spawnPoints; // array of spawn points 
    public GameObject typeOfBoss; // game object of boss 

    private int currentWaveNumber; // current wave number variable that is incremented when a wave is finished
    
    private float nextSpawnTime, currentSpawnTime; // timer variables
    
    private bool canSpawn = true;
    private bool baked; // variable to check if nav mesh has been baked before each wave starts
    private bool givenMana;
    private bool spawnedBoss = false;
   // private int currentLevel;
    //private GameManager gm; // instance of the GameManager
    public NavMeshSurface2d Surface2D; // navmesh component variable

    // --- Start of Singleton Pattern --- \\
    private static readonly object obj = new object();
    private WaveSpawner() { /* Do nothing */ } // WaveSpawner Constructor
    private static WaveSpawner instance = null; // set the instance variable to null
    public static WaveSpawner GetInstance
    {
        get
        {
            if (instance == null)
            {
                lock (obj)
                {
                    if (instance == null)
                        instance = new WaveSpawner();
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
    // --- End of Singleton Pattern --- \\


    private void Start()
    {
        //gm = GameManager.getSingleton(); // get the current instance of the singleton from the GameManager script
        //currentLevel = gm.getCurrentLevel(); // gets the current level from zach's GameManager script
        currentSpawnTime = 0.0f; // timer to track when the next enemy spawns
        //mainTimer = 0.0f;
        nextSpawnTime = 3.0f; // countdown in seconds till next enemy spawn
        baked = false;
    }

    private void FixedUpdate()
    {
        //mainTimer += Time.fixedDeltaTime;
        currentWave = waves[currentWaveNumber]; // set the current wave
        
        // check gamestate if its in play mode
        if (GameManager.CheckState() == GameState.PLAY)
        {
            if (!spawnedBoss & currentWave.bossWave) // no passing for some reason
            {
                // spawn the boss
                Instantiate(typeOfBoss, spawnPoints[0].position, Quaternion.identity);
                spawnedBoss = true; // set spawned boss to true
            }

            if (currentSpawnTime > 0)
            {
                currentSpawnTime -= Time.fixedDeltaTime; // decrement the timer
            }
            else
            {
                // NavMesh bake component
                if (!baked)
                {
                    Surface2D.BuildNavMeshAsync();
                    baked = true;
                }
                // spawn the next enemy
                SpawnWave();

                // check if the last enemy of the wave has been killed
                if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !canSpawn)
                {
                    if (1 < currentWaveNumber) // If completed the last wave
                    {
                        //if (currentLevel == 2)
                        //{
                            //GameManager.ChangeState(GameState.WIN_SWITCH);
                        //}
                        //else
                        //{
                          GameManager.ChangeState(GameState.WIN); // set gamestate to win!
                        //}
                    }
                    else
                    {
                        GameManager.ChangeState(GameState.SETUP); // change the gamestate to setup
                        givenMana = false; // set given mana to false so Wonk can earn mana after the wave
                        currentWaveNumber++; // increment the current wave variable
                        canSpawn = true; // set canSpawn to true to wait for player to begin next wave
                        baked = false; // reset baked variable to false to re-bake after player hits play and before the first enemy spawns
                        if (currentWave.bossWave)
                        {
                            spawnedBoss = false;
                        }
                    }
                }
                currentSpawnTime += nextSpawnTime; // increment the time
            }
        }
    }

    /// <summary>
    /// Check if mana needs to be added to Wonk (If the wave has ended)
    /// </summary>
    /// <returns>Bool value</returns>
    public bool checkMana()
    {
        return givenMana;
    }

    /// <summary>
    /// Sets the value to true when chandler adds mana to Wonk at the end of the wave
    /// </summary>
    public void addedMana(bool val)
    {
        givenMana = val;
    }

    /// <summary>
    /// Spawns one enemy of the current wave if canSpawn is true (When in play mode)
    /// </summary>
    void SpawnWave()
    {
        GameObject randomEnemy;
        Transform randomPoint;
        if (canSpawn)
        {
            // check if theres more than one enemy type or more than one spawn point
            if (currentWave.typeOfEnemies.Length > 1 || spawnPoints.Length > 1)
            {
                if (currentWave.typeOfEnemies.Length > 1) // if there is more than one enemy type
                {
                    // choose one of the enemies at random
                    randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
                }
                else
                {
                    // set to the first/only enemy
                    randomEnemy = currentWave.typeOfEnemies[0];
                }

                if (spawnPoints.Length > 1) // if there is more than one spawnpoint 
                {
                    // choose one of the spawn points at random
                    randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; // choose a random spawnpoint
                }
                else
                {
                    // set to the only spawn point 
                    randomPoint = spawnPoints[0];
                }
                // spawn the random enemy and/or spawn at random point
                Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            }
            else
            {
                // spawn the only enemy type at the first/only spawn point
                Instantiate(currentWave.typeOfEnemies[0], spawnPoints[0].position, Quaternion.identity);
            }
            nextSpawnTime = 3.0f;
            currentWave.numberOfEnemies--;
            if (currentWave.numberOfEnemies == 0)
            {
                canSpawn = false;
            }
        }
    }
}
