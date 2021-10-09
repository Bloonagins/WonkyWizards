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
    private Transform goal; 
    private int enemy_count = 0;

    [SetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("FirstLevel");
        //NUNit.framework.setup
    }

    [UnityTest]
    public IEnumerator SpawnMaxEnemies()
    {
        GoblinGrunt goblinGrunt = GoblinGruntPrefab.GetComponent<GoblinGrunt>();
        goal = GameObject.FindGameObjectWithTag("Goal").GetComponent<Transform>();
        while(!goblinGrunt.IsOnGoal(goal.position)) // check if enemies are inside goal dimensions
        {
            GameObject.Instantiate(GoblinGruntPrefab, new Vector3 (30, -90, 0), Quaternion.identity);
            //GameObject.Instantiate(GoblinGruntPrefab, new Vector3 (40, -80, 0), Quaternion.identity);
            //GameObject.Instantiate(GoblinGruntPrefab, new Vector3 (60, -80, 0), Quaternion.identity);
            //GameObject.Instantiate(GoblinGruntPrefab, new Vector3 (70, -90, 0), Quaternion.identity);
            enemy_count += 1;
            yield return new WaitForSeconds(1f);
        }
        
        Debug.Log("Number of Enemies: "+enemy_count);


    }
    //*/
}
