using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;


// enemy punch's player out of the map
public class StressTest
{
    private GameObject Player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/src/gage/Player.prefab");
    private int count = 0;
    private float minFPS = 10f;

    [SetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("PlayableAreaStressTest");
        //wall = getComponent<Collider>();
    }
    
    [UnityTest]
    public IEnumerator PlayableAreaStress()
    {
        float FPS = 100f;
        while (FPS > minFPS)
        {
            GameObject.Instantiate(Player, new Vector3(0, 0, 0), Quaternion.identity);
            count += 1;
            yield return null;
            FPS = 1 / Time.unscaledDeltaTime;
        }
        Assert.IsTrue(true);
        Debug.Log("Final Player count: " + count);
    }
}
