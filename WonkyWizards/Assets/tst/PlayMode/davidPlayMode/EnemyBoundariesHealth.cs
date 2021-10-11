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

        // Recieve exactly same damage as health
        goblinGrunt.AddHealth(goblinGrunt.GetMaxHealth());
        goblinGrunt.RecieveDamage(goblinGrunt.GetMaxHealth());
        Assert.AreEqual(0, goblinGrunt.GetHealth());
        
        // Recieve maximum damage - 1
        goblinGrunt.AddHealth(goblinGrunt.GetMaxHealth());
        goblinGrunt.RecieveDamage(goblinGrunt.GetMaxHealth()-1);
        Assert.IsTrue(goblinGrunt.GetHealth() > 0);

        // Recieve damage flat damage
        goblinGrunt.AddHealth(goblinGrunt.GetMaxHealth());
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

        // Recieve exactly the same health as maximum health
        goblinGrunt.RecieveDamage(goblinGrunt.GetMaxHealth());
        goblinGrunt.AddHealth(goblinGrunt.GetMaxHealth());
        Assert.AreEqual(goblinGrunt.GetMaxHealth(), goblinGrunt.GetHealth());

        // Recieve maximum health - 1
        goblinGrunt.RecieveDamage(goblinGrunt.GetMaxHealth());
        goblinGrunt.AddHealth(goblinGrunt.GetMaxHealth()-1);
        Assert.IsTrue(goblinGrunt.GetHealth() < goblinGrunt.GetMaxHealth());

        // Recieve flat health
        goblinGrunt.RecieveDamage(goblinGrunt.GetMaxHealth());
        goblinGrunt.AddHealth(60);
        Assert.IsTrue(goblinGrunt.GetHealth() > 0 && goblinGrunt.GetHealth() < goblinGrunt.GetMaxHealth());

        yield return null; // skips frame
    }
    //*/
}
