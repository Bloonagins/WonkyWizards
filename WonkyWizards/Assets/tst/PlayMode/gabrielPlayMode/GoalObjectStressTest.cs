using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GoalObjectStressTest
{
    [SetUp]

    public void SceneLoaderSetUp()
    {
        SceneManager.LoadScene("FirstLevel");
    }
    private GameObject TestGoalPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/src/gabriel/GoalDefault.prefab");
    private float fpsCounter = 100.0f;
    private int goalCounter = 0;



    [UnityTest]
    public IEnumerator GoalStressTestOne()
    {

        // Use the Assert class to test conditions


        do {
            fpsCounter = 100;
            GameObject.Instantiate(TestGoalPrefab, new Vector3(Random.Range(1, 100), Random.Range(0, -100)), Quaternion.identity);
            
            goalCounter += 1;

            yield return new WaitForSeconds(0.25f);
            Debug.Log("Current # of Goals: " + goalCounter);
            fpsCounter = (float)(1.0f / Time.unscaledDeltaTime);
            Debug.Log("Current FPS: " + fpsCounter);
        }
        while (fpsCounter > 15.0f);

        Assert.IsTrue(goalCounter > 0);
        Debug.Log("\nGoals needed to break Unity: " + goalCounter);
        Debug.Log("\nFinal Framerate: " + fpsCounter);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    
}
