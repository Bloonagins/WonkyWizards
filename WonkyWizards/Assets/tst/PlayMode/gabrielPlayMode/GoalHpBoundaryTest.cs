using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;


public class GoalHpBoundaryTest
{

    //private GameObject TestGoalPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/src/gabriel/GoalDefault.prefab");
    //private GameObject TestGoalPrefab = GameObject.FindGameObjectWithTag("Goal");


    // A Test behaves as an ordinary method

    [SetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("FirstLevel");
        
    }
    //private GameObject TestGoalPrefab1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/src/gabriel/GoalDefault.prefab");
    //GameObject.Instantiate(TestGoalPrefab1, new Vector2(0,0), Quaternion.identity) ;


    [UnityTest]
    public IEnumerator GoalHpLowerBoundsTest()
    {
        GameObject TestGoalPrefab = GameObject.FindGameObjectWithTag("Goal");

        //GameObject GoalPrefabObject = TestGoalPrefab.GetComponent<GameObject>();

        //Goal taking more damage than hp - checking if goal corrects/defaults to 0

        TestGoalPrefab.GetComponent<GoalManager>().ResetGoalHpToMax();
        yield return null;

        //Debug.Log(TestGoalPrefab.GetComponent<GoalManager>().GetGoalHp() + "hp");

        //Assert.AreEqual(10000, TestGoalPrefab.GetComponent<GoalManager>().GetGoalHp());


        TestGoalPrefab.GetComponent<GoalManager>().GoalTakeDamage(100000);

        yield return new WaitForFixedUpdate();

        Debug.Log("Goal Hp Lower Bound Check\n");

        Assert.AreEqual(0, TestGoalPrefab.GetComponent<GoalManager>().GetGoalHp());

    }
    [UnityTest]
    public IEnumerator GoalHpUpperBoundsTest()
    {
        GameObject TestGoalPrefab = GameObject.FindGameObjectWithTag("Goal");
        //GameObject GoalPrefabObject = TestGoalPrefab;

        //Goal receiving more Hp than MaxHp - checking if goal corrects/defaults to MaxHp
        TestGoalPrefab.GetComponent<GoalManager>().ResetGoalHpToMax();
        TestGoalPrefab.GetComponent<GoalManager>().GoalAddHp(5000);

        //Debug.Log(TestGoalPrefab.GetComponent<GoalManager>().GetGoalHp() + "hp");

        yield return new WaitForFixedUpdate();

        //Debug.Log(TestGoalPrefab.GetComponent<GoalManager>().GetGoalHp() + "hp");

        Debug.Log("Goal Hp Upper Bound Check\n");

        Assert.AreEqual(TestGoalPrefab.GetComponent<GoalManager>().GetGoalMaxHp(), TestGoalPrefab.GetComponent<GoalManager>().GetGoalHp());
        

    }
    [UnityTest]
    public IEnumerator GoalHpMiddleBoundsTest()
    {
        GameObject TestGoalPrefab = GameObject.FindGameObjectWithTag("Goal");
        //GameObject GoalPrefabObject = TestGoalPrefab;

        //Goal receiving damage in regular bounds - checking if goal functions properly
        TestGoalPrefab.GetComponent<GoalManager>().ResetGoalHpToMax();

        TestGoalPrefab.GetComponent<GoalManager>().GoalTakeDamage(2000);

        yield return new WaitForFixedUpdate();

        Debug.Log("Goal Hp Inner Bound Check\n");

        Assert.Greater(TestGoalPrefab.GetComponent<GoalManager>().GetGoalMaxHp(), TestGoalPrefab.GetComponent<GoalManager>().GetGoalHp());
        Assert.Less(0, TestGoalPrefab.GetComponent<GoalManager>().GetGoalHp());

    }


    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
   
}
