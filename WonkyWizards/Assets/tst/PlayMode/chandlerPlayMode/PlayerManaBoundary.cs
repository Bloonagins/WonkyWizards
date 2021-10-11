using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerManaBoundary : MonoBehaviour
{
    [UnityTest]
    public IEnumerator manaBoundaryTest()
    {
        PlayerScript.setMana(100);

        // makes sure mana can't go below 0
        PlayerScript.spendMana(101);
        Assert.AreEqual(100, PlayerScript.getMana());

        PlayerScript.setMana(100);

        // makes sure mana equals 0 when player spends their current mana value
        PlayerScript.spendMana(100);
        Assert.AreEqual(0, PlayerScript.getMana());

        PlayerScript.setMana(100);

        // makes sure mana equals 1 when spending 1 less than current mana
        PlayerScript.spendMana(99);
        Assert.AreEqual(1, PlayerScript.getMana());

        yield return null;
    }
}
