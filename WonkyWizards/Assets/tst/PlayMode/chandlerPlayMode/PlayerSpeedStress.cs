using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PlayerSpeedStress
{
    private GameObject playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/src/chandler/player.prefab");
    private Vector2 direction = Vector2.up;
    private GameObject player;
    private Rigidbody2D playerRB;
    private float movementspeed;

    [SetUp]
    public void TestSetup()
    {
        SceneManager.LoadScene("PlayerTestScene");
        player = GameObject.Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
        playerRB = player.GetComponent<Rigidbody2D>();
        movementspeed = 30.0f;
    }

    [UnityTest]
    public IEnumerator SuperSpeedTest()
    {
        do
        {
            playerRB.AddForce(direction * movementspeed, ForceMode2D.Impulse);
            yield return null;
        } while (player.transform.position.y < 4 && player.transform.position.y > -4);

        Debug.Log("Final MovementSpeed: " + movementspeed);

        yield return null;
    }

    private void OnCollisionEnter2D(Collider2D wall)
    {
        movementspeed += 0.01f;
        direction *= -1;
    }
}
