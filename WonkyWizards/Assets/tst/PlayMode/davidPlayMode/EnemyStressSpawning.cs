using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;

public class EnemyStressTest
{
    ///*
    private GameObject GoblinGruntPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/src/david/Prefabs/GoblinGrunt.prefab");
    private int enemy_count = 0;

    [SetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("FirstLevel");
    }

    [UnityTest]
    public IEnumerator SpawnMaxEnemies()
    {
        float fps = 100f;
        float minimum_fps = 10f;
        while(fps > minimum_fps)
        {
            GameObject.Instantiate(GoblinGruntPrefab, new Vector3 (30, -90, 0), Quaternion.identity);
            GameObject.Instantiate(GoblinGruntPrefab, new Vector3 (40, -80, 0), Quaternion.identity);
            GameObject.Instantiate(GoblinGruntPrefab, new Vector3 (60, -80, 0), Quaternion.identity);
            GameObject.Instantiate(GoblinGruntPrefab, new Vector3 (70, -90, 0), Quaternion.identity);
            enemy_count += 4;
            yield return new WaitForSeconds(1f);
            fps = 1/Time.unscaledDeltaTime;
            //Debug.Log("Fps: "+fps);
        }
        Debug.Log("Number of Enemies: "+enemy_count);
    }

    //*/
}
