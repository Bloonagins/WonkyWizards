using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;

// tests if projectiles detroy on hit (Cannot leave the map)
public class BoundaryTest1 : MonoBehaviour
{
    private GameObject PlayerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/src/gage/Player.prefab");

    private bool properCollision = false;

    [SetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("PlayableAreaStressTest");
    }

    [UnityTest]
    public IEnumerator CollisionWall()
    {
        //GameObject spawnPoint = GameObject.Find("EntranceSpawnPoint");
        GameObject Player = GameObject.Instantiate(PlayerPrefab, new Vector3(0,0,0), Quaternion.identity);
        yield return new WaitForSeconds(5f);
        properCollision = Player.GetComponent<testPlayer>().walls;
        Assert.AreEqual(true, properCollision);
        properCollision = false;
    }
    
    [UnityTest]
    public IEnumerator CollisionEntranceWall()
    {
        //GameObject spawnPoint = GameObject.Find("EntranceSpawnPoint");
        GameObject Player = GameObject.Instantiate(PlayerPrefab, new Vector3(48, -5, 0), Quaternion.identity);
        yield return new WaitForSeconds(5f);
        properCollision = Player.GetComponent<testPlayer>().opening;
        Assert.AreEqual(true, properCollision);
        properCollision = false;
    }
}
