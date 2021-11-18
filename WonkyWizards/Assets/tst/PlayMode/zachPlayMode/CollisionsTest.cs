using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;

public class CollisionsTest
{
    private GameObject Projectile1 = AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/src/zach/Fireball/FireBall.prefab"); 
    private GameObject Projectile2 = AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/src/zach/MagicMissle/MagicMissle.prefab"); 
    private GameObject Projectile3 = AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/src/zach/Ice Beam/IceBeam.prefab"); 
    private GameObject Projectile4 = AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/src/zach/Acid Spray/AcidSpray.prefab"); 

    private bool properCollision = false;

    [SetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("SpellTest");
    }
    //FB
    [UnityTest]
    public IEnumerator FBCollisionWall()
    {
        GameObject Wall = GameObject.Find("Wall");
        GameObject.Instantiate(Projectile1, new Vector3(-6,-3.37f,0), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        properCollision = Wall.GetComponent<TestWall>().propercollision;
        Assert.AreEqual(true, properCollision);
        properCollision = false;       
    }

    [UnityTest]
    public IEnumerator FBCollisionEnemy()
    {
        GameObject Enemy = GameObject.Find("Enemy");
        GameObject.Instantiate(Projectile1, new Vector3(-6,3.37f,0), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        properCollision = Enemy.GetComponent<TestEnemy>().propercollision;
        Assert.AreEqual(true, properCollision);
        properCollision = false;       
    }

    //MM
    [UnityTest]
    public IEnumerator MMCollisionWall()
    {
        GameObject Wall = GameObject.Find("Wall");
        GameObject.Instantiate(Projectile2, new Vector3(-6,-3.37f,0), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        properCollision = Wall.GetComponent<TestWall>().propercollision;
        Assert.AreEqual(true, properCollision);
        properCollision = false;       
    }

    [UnityTest]
    public IEnumerator MMCollisionEnemy()
    {
        GameObject Enemy = GameObject.Find("Enemy");
        GameObject.Instantiate(Projectile2, new Vector3(-6,3.37f,0), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        properCollision = Enemy.GetComponent<TestEnemy>().propercollision;
        Assert.AreEqual(true, properCollision);
        properCollision = false;       
    }

    //IB
    [UnityTest]
    public IEnumerator IBCollisionWall()
    {
        GameObject Wall = GameObject.Find("Wall");
        GameObject.Instantiate(Projectile3, new Vector3(-6,-3.37f,0), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        properCollision = Wall.GetComponent<TestWall>().propercollision;
        Assert.AreEqual(true, properCollision);
        properCollision = false;       
    }

    [UnityTest]
    public IEnumerator IBCollisionEnemy()
    {
        GameObject Enemy = GameObject.Find("Enemy");
        GameObject.Instantiate(Projectile3, new Vector3(-6,3.37f,0), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        properCollision = Enemy.GetComponent<TestEnemy>().propercollision;
        Assert.AreEqual(true, properCollision);
        properCollision = false;       
    }

    //AS
    [UnityTest]
    public IEnumerator ASCollisionWall()
    {
        GameObject Wall = GameObject.Find("Wall");
        GameObject.Instantiate(Projectile4, new Vector3(-6,-3.37f,0), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        properCollision = Wall.GetComponent<TestWall>().propercollision;
        Assert.AreEqual(true, properCollision);
        properCollision = false;       
    }

    [UnityTest]
    public IEnumerator ASCollisionEnemy()
    {
        GameObject Enemy = GameObject.Find("Enemy");
        GameObject.Instantiate(Projectile4, new Vector3(-6,3.37f,0), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        properCollision = Enemy.GetComponent<TestEnemy>().propercollision;
        Assert.AreEqual(true, properCollision);
        properCollision = false;       
    }
}
