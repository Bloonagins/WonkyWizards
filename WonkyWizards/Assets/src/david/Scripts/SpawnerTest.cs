using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTest : MonoBehaviour
{
    // Variable for the location of where the enemies will spawn from
    public Transform spawnPoint;
    // Variable for the Game Object of the enemy prefab that will be spawned
    public GameObject enemyPrefab;

    // Update is called once per frame
    void Update()
    {
        // Check if Input is active
        // if (PlayerScript.allowSpawn)
        if (Input.GetMouseButtonDown(0)) 
        {
            // Spawns the enemies at the give point
            Instantiate(enemyPrefab, spawnPoint);
        }
    }
}
