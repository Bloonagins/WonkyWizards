using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTest : MonoBehaviour
{
    // Variable for the location of where the enemies will spawn from
    public Transform spawnPoint;
    // Variable for the Game Object of the enemy prefab that will be spawned
    public GameObject enemyPrefab;
    private float timer;
    public float spawnDelay;

    void Start() 
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Delays spawn time
        timer += Time.deltaTime;

        // Check if toggle active
        if(PlayerScript.allowSpawn && timer >= spawnDelay)
        {
            // Spawns the enemies at the give point
            Instantiate(enemyPrefab, spawnPoint);
            timer = 0.0f;
        }
    }
}
