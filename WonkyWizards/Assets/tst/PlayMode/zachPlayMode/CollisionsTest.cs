using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;

public class CollisionsTest
{
    private GameObject Projectile1 = AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/src/zach/Projectile.prefab"); 
    private GameObject Projectile2 = AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/src/zach/Projectile.prefab"); 

    private bool properCollision = false;

    [SetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("SpellTest");
    }

    [UnityTest]
    public IEnumerator CollisionWall()
    {
        GameObject TestPlayer1 = GameObject.Find("TestPlayer1");
        GameObject Wall = GameObject.Find("Wall");
        GameObject.Instantiate(Projectile1, TestPlayer1.transform.position, TestPlayer1.transform.rotation);
        yield return new WaitForSeconds(1f);
        properCollision = Wall.GetComponent<TestWall>().propercollision;
        Assert.AreEqual(true, properCollision);
        properCollision = false;       
    }

    [UnityTest]
    public IEnumerator CollisionEnemy()
    {
        GameObject TestPlayer = GameObject.Find("TestPlayer2");
        GameObject Wall = GameObject.Find("Enemy");
        GameObject.Instantiate(Projectile2, TestPlayer.transform.position, TestPlayer.transform.rotation);
        yield return new WaitForSeconds(1f);
        properCollision = Wall.GetComponent<TestEnemy>().propercollision;
        Assert.AreEqual(true, properCollision);
        properCollision = false;       
    }
}
