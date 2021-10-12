using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using NUnit.Framework;
using UnityEngine.SceneManagement;

public class SummonStress
{
    private GameObject barrierPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/src/ethan/Barrier.prefab");
    private Barrier barrier;
    private int summonCount = 0;

    [SetUp]
    public void SummonStressSetUp()
    {
        SceneManager.LoadScene("SummonTestScene");
    }

    [UnityTest]
    public IEnumerator SummonStressTest()
    {
        float fps = 100.0f;
        float min_fps = 10.0f;
        barrier = barrierPrefab.GetComponent<Barrier>();
        while (fps > min_fps)
        {
            GameObject.Instantiate(barrier, new Vector3(0, 0, 0), Quaternion.identity);
            summonCount++;
            yield return null;
            fps = 1 / Time.unscaledDeltaTime;
        }

        Assert.IsTrue(summonCount > 0);
        Debug.Log("Number of Summons: " + summonCount);
    }
}
