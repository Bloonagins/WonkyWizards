using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class StressTest
{ /*
    [SetUp]
    public void loadScene()
    {
        SceneManager.LoadScene("StressTest");
        //NUNit.framework.setup
    }

    [UnityTest]
    public IEnumerator VelocityStressTestFunc()
    {

        //Get the rigidbody component of the Ball game object
        Rigidbody2D body = GameObject.Find("Ball").GetComponent<Rigidbody2D>();

        //Track positions of each paddle in Scene
        Vector3 leftPlayerPos = GameObject.Find("Player Paddle").GetComponent<Transform>().position;
        Vector3 rightPlayerPos = GameObject.Find("Computer Paddle").GetComponent<Transform>().position;
        Debug.Log(body.velocity.x);

        //loop until ball moves beyond either paddle (should, in theory loop forever)
        while (body.transform.position.x > leftPlayerPos.x && body.transform.position.x < rightPlayerPos.x)
        {

            Vector2 currentVelocity = body.velocity;
            currentVelocity *= 1.001f;
            body.velocity = currentVelocity;

            yield return null;
            //yield return new WaitForSeconds(1f);

        }

        Debug.Log("No Collision at velocity " + body.velocity.x);
        Debug.Log(body.transform.position);
        body.velocity = Vector2.zero;
    }

    /*[TearDown]
    public void teardown()
    {
        SceneManager.UnloadSceneAsync("TestScene");
    } */
}
