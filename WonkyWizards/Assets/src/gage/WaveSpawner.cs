using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] spawnPoints;
    GameObject currentPoint;
    int index;
    
    public Transform enemyPrefab; // assign enemy prefabs
    public GameObject[] enemies; // array of enemy types
    public float timeBetweenSpawns; // countdown of time it takes for a enemy to spawn
    public float timeBetweenWaves = 10f; // 

    public bool canSpawn;
    
    private float countdown = 2f;

    void FixedUpdate()
    {
        if(countdown <= 0f)
        {
            SpawnWave();
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }
    void SpawnWave()
    {
        Debug.Log("Wave incoming!");
    }
}

/// Use this to comment functions