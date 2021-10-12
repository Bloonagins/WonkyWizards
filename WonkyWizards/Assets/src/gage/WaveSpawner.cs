using UnityEngine;
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

    private Wave currentWave;
    private int currentWaveNumber;
    private bool canSpawn = true;
    private float nextSpawnTime;

    private void FixedUpdate()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
    }

    void SpawnWave()
    {
        if(canSpawn && nextSpawnTime < Time.time) 
        {
            //GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            //Transform randomPoint = spawnPoints[randomEnemy.Range(0, spawnPoints.Length)];
            Instantiate(currentWave.typeOfEnemies[0], spawnPoints[0].position, Quaternion.identity);
            currentWave.numberOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if(currentWave.numberOfEnemies == 0)
            {
                canSpawn = false;
            }
        }
    }
}

/// Use this to comment functions