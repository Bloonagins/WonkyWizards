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
    private bool upSpeed;

    [SetUp]
    public void LoadTestScene()
    {
        SceneManager.LoadScene("PlayerTestScene");
        upSpeed = false;
    }

    [UnityTest]
    public IEnumerator SuperSpeedTest()
    {
        GameObject player = GameObject.Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
        Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
        float movementspeed = 30.0f;

        do
        {
            if (upSpeed)
            {
                upSpeed = false;
                movementspeed += 0.01f;
            }
            playerRB.AddForce(direction * movementspeed, ForceMode2D.Impulse);
        } while (player.transform.position.y < 4 && player.transform.position.y > -4);

        Debug.Log("Final MovementSpeed: " + movementspeed);

        yield return null;
    }

    private void OnCollisionEnter2D(Collider2D wall)
    {
        upSpeed = true;
        direction *= -1;
    }
}
