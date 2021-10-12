using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class EnemyHealthTest
{
    private GameObject GoblinGruntPrefab = AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/src/david/Prefabs/GoblinGrunt.prefab"); 
    
    [UnityTest]
    public IEnumerator BelowMinHealth()
    {
        GoblinGrunt goblinGrunt = GoblinGruntPrefab.GetComponent<GoblinGrunt>();

        // Recieve more damage than available health
        goblinGrunt.RecieveDamage(goblinGrunt.GetMaxHealth() + 100);
        Assert.AreEqual(0, goblinGrunt.GetHealth());

        // Reset health to max
        goblinGrunt.AddHealth(goblinGrunt.GetMaxHealth());

        // Recieve exactly same damage as health
        goblinGrunt.RecieveDamage(goblinGrunt.GetMaxHealth());
        Assert.AreEqual(0, goblinGrunt.GetHealth());
        
        // Reset health to max
        goblinGrunt.AddHealth(goblinGrunt.GetMaxHealth());

        // Recieve maximum damage - 1
        goblinGrunt.RecieveDamage(goblinGrunt.GetMaxHealth()-1);
        Assert.IsTrue(goblinGrunt.GetHealth() > 0);

        // Reset health to max
        goblinGrunt.AddHealth(goblinGrunt.GetMaxHealth());

        // Recieve damage flat damage
        goblinGrunt.RecieveDamage(60);
        Assert.IsTrue(goblinGrunt.GetHealth() > 0 && goblinGrunt.GetHealth() < goblinGrunt.GetMaxHealth());

        yield return null; // skips frame
    }

    [UnityTest]
    public IEnumerator AboveMaxHealth()
    {
        GoblinGrunt goblinGrunt = GoblinGruntPrefab.GetComponent<GoblinGrunt>();

        // Add more health than maximum
        goblinGrunt.AddHealth(goblinGrunt.GetMaxHealth() + 100);
        Assert.AreEqual(goblinGrunt.GetMaxHealth(), goblinGrunt.GetHealth());

        // Reset health to 0
        goblinGrunt.RecieveDamage(goblinGrunt.GetMaxHealth());

        // Recieve exactly the same health as maximum health
        goblinGrunt.AddHealth(goblinGrunt.GetMaxHealth());
        Assert.AreEqual(goblinGrunt.GetMaxHealth(), goblinGrunt.GetHealth());

        // Reset health to 0
        goblinGrunt.RecieveDamage(goblinGrunt.GetMaxHealth());

        // Recieve maximum health - 1
        goblinGrunt.AddHealth(goblinGrunt.GetMaxHealth()-1);
        Assert.IsTrue(goblinGrunt.GetHealth() < goblinGrunt.GetMaxHealth());

        // Reset health to 0
        goblinGrunt.RecieveDamage(goblinGrunt.GetMaxHealth());

        // Recieve flat health
        goblinGrunt.AddHealth(60);
        Assert.IsTrue(goblinGrunt.GetHealth() > 0 && goblinGrunt.GetHealth() < goblinGrunt.GetMaxHealth());

        yield return null; // skips frame
    }
    //*/
}
