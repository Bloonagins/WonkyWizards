using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using NUnit.Framework;

public class SummonBoundary
{
    private GameObject barrierPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/src/ethan/SummonPrefabs/Barrier.prefab");
    private Barrier barrier;

    [UnityTest]
    public IEnumerator SummonHealthBelow0()
    {
        if (!barrierPrefab) Debug.Log("barrierPrefab is null!");

        barrier = barrierPrefab.GetComponent<Barrier>();
        if (!barrier) Debug.Log("barrier is null!");

        barrier.takeDamage(barrier.getMaxHealth() + 100);
        Assert.AreEqual(0, barrier.getHealth());
        yield return null;
    }

    [UnityTest]
    public IEnumerator SummonHealthAboveMax()
    {
        if (!barrierPrefab) Debug.Log("barrierPrefab is null!");

        Barrier barrier = barrierPrefab.GetComponent<Barrier>();
        if (!barrier) Debug.Log("barrier is null!");

        barrier.takeHealing(barrier.getMaxHealth() + 100);
        Assert.AreEqual(barrier.getMaxHealth(), barrier.getHealth());
        yield return null;
    }
}
