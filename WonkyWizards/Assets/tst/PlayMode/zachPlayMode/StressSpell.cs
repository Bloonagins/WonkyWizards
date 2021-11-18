using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;

public class NewTestScript
{

    private GameObject Spell = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/src/zach/Fireball/FireBall.prefab");
    private int count = 0;
    private float minFPS = 10f;
    
    [SetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("FirstLevel");
    }
    [UnityTest]
    public IEnumerator SpellStress()
    {
        float FPS = 100f;
        while (FPS > minFPS)
        {
            GameObject.Instantiate(Spell, new Vector3(Random.Range(1, 100), Random.Range(0, -100)), Quaternion.identity);
            GameObject.Instantiate(Spell, new Vector3(Random.Range(1, 100), Random.Range(0, -100)), Quaternion.identity);
            GameObject.Instantiate(Spell, new Vector3(Random.Range(1, 100), Random.Range(0, -100)), Quaternion.identity);
            GameObject.Instantiate(Spell, new Vector3(Random.Range(1, 100), Random.Range(0, -100)), Quaternion.identity);
            count += 4;
            yield return null;
            FPS = 1 / Time.unscaledDeltaTime;
        }
        Assert.IsTrue(true);
        Debug.Log("Final Spell count: " + count);
    }
}
