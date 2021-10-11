using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PlayerSpeedStress
{
    private GameObject playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/src/chandler/player.prefab");

    [SetUp]
    public void LoadTestScene()
    {
        SceneManager.LoadScene("PlayerTestScene");
    }

    [UnityTest]
    public IEnumerator SuperSpeedTest()
    {
        GameObject player = GameObject.Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
        Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
        PlayerControls playerController = player.GetComponent<PlayerControls>();
        playerController.movementspeed = 30.0f;
        float minFPS = 30.0f;

        do
        {
            playerRB.AddForce(Vector2.up * playerController.movementspeed, ForceMode2D.Impulse);
            playerController.movementspeed *= 2;
        } while (1 / Time.unscaledDeltaTime > minFPS);

        Debug.Log("Final MovementSpeed: " + playerController.movementspeed);

        yield return null;
    }
}
