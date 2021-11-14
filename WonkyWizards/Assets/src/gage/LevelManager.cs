using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Pattern used:
// - Private class data

public class LevelManager : MonoBehaviour
{
    private GameManager gm;

    // Private Class Data Pattern
    private LevelManagerData lmd;

    private static bool[,] currentLevelArr;
    
    private int currentLevel;
  
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.getSingleton();
        currentLevel = gm.getCurrentLevel(); // gets the current level from zach's GameManager script

        currentLevelArr = lmd.Initiate_Level(currentLevel);

        gm.setLevelArray(currentLevelArr);
        //gm.setLevelArray(currentLevelArr);
    }
        
    /// <returns> current static array for the level </returns>
    public static bool [,] getLevelArray()
    {
        return currentLevelArr;
    }
    
    /// <returns> get the current level's row size </returns>
    public int getLevelRows()
    {
        return this.lmd.getLevelRowsData();
    }
    /// <returns> get the current level's column size </returns>
    public int getLevelCols()
    {
        return this.lmd.getLevelColsData();
    }
    /// <returns> gets the spawn point coordinates for the current level</returns>
    public Tuple<int, int> getEnemySpawnPoint()
    {
        return this.lmd.getEnemySpawnPointData();
    }
    /// <returns> gets the enemy's goal for the current level </returns>
    public Tuple<int, int> getLevelGoal()
    {
        return this.lmd.getLevelGoalData();
    }
}
