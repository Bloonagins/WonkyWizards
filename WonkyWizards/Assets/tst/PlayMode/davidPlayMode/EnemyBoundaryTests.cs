using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class EnemyHealthTest
{
    private GameObject enemyPrefab = AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/src/david/Prefabs/GoblinGiant.prefab"); 
    
    [UnityTest]
    public IEnumerator BelowMinHealth()
    {
        Enemy enemy = enemyPrefab.GetComponent<Enemy>();

        // Recieve more damage than available health
        enemy.RecieveDamage(enemy.GetMaxHealth() + 100);
        Assert.AreEqual(0, enemy.GetHealth());

        // Reset health to max
        enemy.AddHealth(enemy.GetMaxHealth());

        // Recieve exactly same damage as health
        enemy.RecieveDamage(enemy.GetMaxHealth());
        Assert.AreEqual(0, enemy.GetHealth());
        
        // Reset health to max
        enemy.AddHealth(enemy.GetMaxHealth());

        // Recieve maximum damage - 1
        enemy.RecieveDamage(enemy.GetMaxHealth()-1);
        Assert.IsTrue(enemy.GetHealth() > 0);

        // Reset health to max
        enemy.AddHealth(enemy.GetMaxHealth());

        // Recieve damage flat damage
        enemy.RecieveDamage(60);
        Assert.IsTrue(enemy.GetHealth() > 0 && enemy.GetHealth() < enemy.GetMaxHealth());

        yield return null; // skips frame
    }

    [UnityTest]
    public IEnumerator AboveMaxHealth()
    {
        Enemy enemy = enemyPrefab.GetComponent<Enemy>();

        // Add more health than maximum
        enemy.AddHealth(enemy.GetMaxHealth() + 100);
        Assert.AreEqual(enemy.GetMaxHealth(), enemy.GetHealth());

        // Reset health to 0
        enemy.RecieveDamage(enemy.GetMaxHealth());

        // Recieve exactly the same health as maximum health
        enemy.AddHealth(enemy.GetMaxHealth());
        Assert.AreEqual(enemy.GetMaxHealth(), enemy.GetHealth());

        // Reset health to 0
        enemy.RecieveDamage(enemy.GetMaxHealth());

        // Recieve maximum health - 1
        enemy.AddHealth(enemy.GetMaxHealth()-1);
        Assert.IsTrue(enemy.GetHealth() < enemy.GetMaxHealth());

        // Reset health to 0
        enemy.RecieveDamage(enemy.GetMaxHealth());

        // Recieve flat health
        enemy.AddHealth(60);
        Assert.IsTrue(enemy.GetHealth() > 0 && enemy.GetHealth() < enemy.GetMaxHealth());

        yield return null; // skips frame
    }
}

public class EnemyDamageTest
{
    private GameObject enemyPrefab = AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/src/david/Prefabs/GoblinGiant.prefab"); 
    
    [UnityTest]
    public IEnumerator BelowAboveDamage()
    {
        Enemy enemy = enemyPrefab.GetComponent<Enemy>();

        // Add damage above max
        enemy.ChangeDamage(enemy.GetMaxDamage() + 100);
        Assert.AreEqual(enemy.GetMaxDamage(), enemy.GetDamage());

        // Decrease damage below min
        enemy.ChangeDamage(-(enemy.GetMaxDamage()+100));
        Assert.AreEqual(enemy.GetMinDamage(), enemy.GetDamage());

        // Add exactly max damage
        enemy.ChangeDamage(enemy.GetMaxDamage()-enemy.GetDamage());
        Assert.AreEqual(enemy.GetMaxDamage(), enemy.GetDamage());
        
        // Decrease damage - 1 
        enemy.ChangeDamage(-1);
        Assert.IsTrue(enemy.GetDamage() < enemy.GetMaxDamage());

        // reset to min
        enemy.ChangeDamage(-enemy.GetMaxDamage());

        // Add 1 to damage
        enemy.ChangeDamage(1);
        Assert.IsTrue(enemy.GetDamage() > enemy.GetMinDamage());

        yield return null; // skips frame
    }

}