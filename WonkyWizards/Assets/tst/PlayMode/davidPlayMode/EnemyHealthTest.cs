using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class EnemyHealthTest
{
    /* 
    private GameObject GoblinGruntPrefab = AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/src/david/Prefabs/GoblinGrunt.prefab"); 
    
    [UnityTest]
    public IEnumerator BelowMinHealth()
    {
        GoblinGrunt goblinGrunt = GoblinGruntPrefab.GetComponent<GoblinGrunt>();

        goblinGrunt.RecieveDamage(goblinGrunt.GetMaxHealth() + 100);
        Assert.AreEqual(0, goblinGrunt.GetHealth());

        goblinGrunt.AddHealth(goblinGrunt.GetMaxHealth());
        goblinGrunt.RecieveDamage(goblinGrunt.GetMaxHealth());
        Assert.AreEqual(0, goblinGrunt.GetHealth());

        goblinGrunt.AddHealth(goblinGrunt.GetMaxHealth());
        goblinGrunt.RecieveDamage(goblinGrunt.GetMaxHealth()-1);
        Assert.IsTrue(goblinGrunt.GetHealth() > 0);

        yield return null; // skips frame
    }

    [UnityTest]
    public IEnumerator AboveMaxHealth()
    {
        GoblinGrunt goblinGrunt = GoblinGruntPrefab.GetComponent<GoblinGrunt>();

        goblinGrunt.AddHealth(goblinGrunt.GetMaxHealth() + 100);
        Assert.AreEqual(goblinGrunt.GetMaxHealth(), goblinGrunt.GetHealth());

        goblinGrunt.RecieveDamage(goblinGrunt.GetMaxHealth());
        goblinGrunt.AddHealth(goblinGrunt.GetMaxHealth());
        Assert.AreEqual(goblinGrunt.GetMaxHealth(), goblinGrunt.GetHealth());

        goblinGrunt.RecieveDamage(goblinGrunt.GetMaxHealth());
        goblinGrunt.AddHealth(goblinGrunt.GetMaxHealth()-1);
        Assert.IsTrue(goblinGrunt.GetHealth() < goblinGrunt.GetMaxHealth());

        yield return null; // skips frame
    }
    */
}
