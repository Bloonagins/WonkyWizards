using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PlayerSpeedStress
{
    private GameObject playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/tst/PlayMode/chandlerPlayMode/TestPlayer.prefab");
    private GameObject player;

    [SetUp]
    public void TestSetup()
    {
        SceneManager.LoadScene("PlayerTestScene");
    }

    [UnityTest]
    public IEnumerator SuperSpeedTest()
    {
        player = GameObject.Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
        TestPlayer testPlayer = player.GetComponent<TestPlayer>();
        do
        {
            yield return null;
        } while (player.transform.position.y < 4.0f && player.transform.position.y > -4.0f);

        Debug.Log("Final MovementSpeed: " + testPlayer.speed);

        yield return null;
    }
}