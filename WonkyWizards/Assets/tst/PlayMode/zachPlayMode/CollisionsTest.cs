using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;

public class CollisionsTest
{
    private GameObject Projectile = AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/src/zach/Projectile.prefab"); 

    private bool properCollision = false;

    [SetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("SpellTest");
    }

    [UnityTest]
    public IEnumerator CollisionWall()
    {
        GameObject Wall = GameObject.Find("Wall");
        GameObject.Instantiate(Projectile, new Vector3(-6,-3.37f,0), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        properCollision = Wall.GetComponent<TestWall>().propercollision;
        Assert.AreEqual(true, properCollision);
        properCollision = false;       
    }

    [UnityTest]
    public IEnumerator CollisionEnemy()
    {
        GameObject Enemy = GameObject.Find("Enemy");
        GameObject.Instantiate(Projectile, new Vector3(-6,3.37f,0), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        properCollision = Enemy.GetComponent<TestEnemy>().propercollision;
        Assert.AreEqual(true, properCollision);
        properCollision = false;       
    }
}
