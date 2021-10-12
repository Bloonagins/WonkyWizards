using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using NUnit.Framework;

public class SummonBoundary
{
    private GameObject barrierPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/src/ethan/Barrier.prefab");
    private Barrier barrier;

    [UnityTest]
    public IEnumerator SummonHealthBelow0()
    {
        barrier = barrierPrefab.GetComponent<Barrier>();

        barrier.takeDamage(barrier.getMaxHealth() + 100);
        Assert.AreEqual(0, barrier.getHealth());
        yield return null;
    }

    [UnityTest]
    public IEnumerator SummonHealthAboveMax()
    {
        Barrier barrier = barrierPrefab.GetComponent<Barrier>();

        barrier.takeHealing(barrier.getMaxHealth() + 100);
        Assert.AreEqual(barrier.getMaxHealth(), barrier.getHealth());
        yield return null;
    }
}
