using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class LeftPaddleBoundaryTest
{
    [UnityTest]
    public IEnumerator TestUpperPaddleBoundary()
    {
        SceneManager.LoadScene("Pong");
        yield return new WaitForSeconds(1.0f);

        GameObject paddle1 = GameObject.Find("Player Paddle");
        Vector3 newPosition = paddle1.transform.position;
        
        newPosition.y = 50f;
        paddle1.transform.position = newPosition;
        yield return null; // skips a frame
        Assert.AreEqual(4.2f, paddle1.transform.position.y);


        newPosition.y = 2.0f;
        paddle1.transform.position = newPosition;
        yield return null;
        Assert.IsTrue(2.0f == paddle1.transform.position.y);


        newPosition.y = 4.2f;
        paddle1.transform.position = newPosition;
        yield return null;
        Assert.AreEqual(4.2f, paddle1.transform.position.y);

        newPosition.y = 4.21f;
        paddle1.transform.position = newPosition;
        yield return null;
        Assert.AreEqual(4.2f, paddle1.transform.position.y);
    }

    [UnityTest]
    public IEnumerator TestLowerPaddleBoundary()
    {
        SceneManager.LoadScene("Pong");
        yield return new WaitForSeconds(1.0f);

        GameObject paddle1 = GameObject.Find("Player Paddle");
        Vector3 newPosition = paddle1.transform.position;
        
        newPosition.y = -50f;
        paddle1.transform.position = newPosition;
        yield return null; // skips a frame
        Assert.AreEqual(-4.2f, paddle1.transform.position.y);


        newPosition.y = -2.0f;
        paddle1.transform.position = newPosition;
        yield return null;
        Assert.IsTrue(-2.0f == paddle1.transform.position.y);


        newPosition.y = -4.2f;
        paddle1.transform.position = newPosition;
        yield return null;
        Assert.AreEqual(-4.2f, paddle1.transform.position.y);

        newPosition.y = -4.21f;
        paddle1.transform.position = newPosition;
        yield return null;
        Assert.AreEqual(-4.2f, paddle1.transform.position.y);
    }
}
