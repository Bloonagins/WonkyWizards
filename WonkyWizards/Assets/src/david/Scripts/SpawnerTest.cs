using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTest : MonoBehaviour
{
    // Variable for the location of where the enemies will spawn from
    public Transform spawnPoint;
    // Variable for the Game Object of the enemy prefab that will be spawned
    public GameObject enemyPrefab;
    public bool toggleKey;
    private float timer;
    public float spawnDelay;

    void Start() 
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if key is pressed, toggle true and false
        //if(Input.GetKeyDown(KeyCode.Q)) 
        if(PlayerScript.allowSpawn)
        {
            toggleKey = !toggleKey;
        }

        // Delays spawn time
        timer += Time.deltaTime;

        // Check if toggle active
        if(toggleKey && timer >= spawnDelay)
        {
            // Spawns the enemies at the give point
            Instantiate(enemyPrefab, spawnPoint);
            timer = 0.0f;
        }
    }
}
