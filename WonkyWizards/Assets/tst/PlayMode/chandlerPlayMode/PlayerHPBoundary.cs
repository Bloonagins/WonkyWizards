using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

public class PlayerHPBoundary
{
    [UnityTest]
    public IEnumerator HPBoundaryTest()
    {
        PlayerScript.resetPlayerHP();

        // makes sure hp can't go below 0
        PlayerScript.damagePlayer(PlayerScript.getMAXHP() + 1);
        Assert.AreEqual(0, PlayerScript.getHP());

        PlayerScript.resetPlayerHP();

        // makes sure hp can't go above max hp
        PlayerScript.damagePlayer(-1);
        Assert.AreEqual(PlayerScript.getMAXHP(), PlayerScript.getHP());

        PlayerScript.resetPlayerHP();

        // makes sure hp equals 0 when player loses max hp
        PlayerScript.damagePlayer(PlayerScript.getMAXHP());
        Assert.AreEqual(0, PlayerScript.getHP());

        PlayerScript.resetPlayerHP();

        // makes sure hp equals 1 when player takes max hp - 1 damage
        PlayerScript.damagePlayer(PlayerScript.getMAXHP() - 1);
        Assert.AreEqual(1, PlayerScript.getHP());

        yield return null;
    }
}
