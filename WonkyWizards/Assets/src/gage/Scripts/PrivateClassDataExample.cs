/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Pattern used:
// - Private class data

public class LevelManager : MonoBehaviour
{
    private GameManager gm;

    private static bool[,] currentLevelArr;

    private int currentLevel;

    public LevelManager()
    {

    }

    //private LevelManagerData LevelSetup()
    void Start()
    {
        // Private Class Data Pattern
        private static LevelManager lmd;
        //LevelManagerData lmd = new LevelManagerData();
        gm = GameManager.getSingleton();
        currentLevel = gm.getCurrentLevel(); // gets the current level from zach's GameManager script

        currentLevelArr = lmd.Initiate_Level(currentLevel);

        gm.setLevelArray(currentLevelArr);
        //gm.setLevelArray(currentLevelArr);

        return lmd;
    }

    /// <returns> current static array for the level </returns>
    public static bool[,] getLevelArray()
    {
        return currentLevelArr;
    }

    /// <returns> get the current level's row size </returns>
    public static int getLevelRows()
    {
        return getLevelRowsData();
    }
    /// <returns> get the current level's column size </returns>
    public static int getLevelCols()
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
*/