/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerData
{
    private static bool[,] level1Arr = new bool[12, 12];
    private static bool[,] level2Arr = new bool[12, 12];
    //private static bool[,] level3Arr = new bool[12, 12]; 
    //private static bool[,] level4Arr = new bool[12, 12]; 
    //private static bool[,] level5Arr = new bool[12, 12]; 
    //private static bool[,] level6Arr = new bool[12, 12];
    //private static bool[,] level7Arr = new bool[12, 12];
    //private static bool[,] level8Arr = level1Arr; // Final level is the same as level 1

    private static Tuple<int, int> spawnPoint; // tuple of the enemy spawn point coordinates (x, y)
    private static Tuple<int, int> goal;  // tuple of the goal tower coordinates (x, y)
    private static int rows; // number of rows for the current level 
    private static int cols; // number of columns for the current level

    // Constructor function
    public LevelManagerData()
    {

    }
    public bool[,] Initiate_Level(int currentLevel)
    {
        bool[,] temp;
        switch (currentLevel)
        {
            case 0:
                rows = 12;
                cols = 12;

                Initialize_Level_1();
                temp = level1Arr;

                spawnPoint = new Tuple<int, int>(0, 5);
                goal = new Tuple<int, int>(11, 5);

                break;
            case 1:
                rows = 12;
                cols = 12;

                Initialize_Level_2();
                temp = level2Arr;

                spawnPoint = new Tuple<int, int>(0, 5);
                goal = new Tuple<int, int>(11, 5);

                break;
            default:
                Debug.Log("Current level invalide while initializing array");
                temp = null;
                break;
        }
        return temp;
    }

    private void Initialize_Level_1()
    {
        /// Initializes the level 1 bool array of traversable tiles.
        ///     - true  = is traversable
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                // set tile to traversable
                level1Arr[i, j] = true;
    }
    private void Initialize_Level_2()
    {
        /// Initializes the level 2 bool array of traversable tiles.
        ///     - true  = is traversable
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                // set tile to traversable
                level2Arr[i, j] = true;
    }

    /// <returns> get the current level's row size </returns>
    public int getLevelRowsData()
    {
        return rows;
    }

    /// <returns> get the current level's column size </returns>
    public int getLevelColsData()
    {
        return cols;
    }

    /// <returns> gets the spawn point coordinates for the current level</returns>
    public Tuple<int, int> getEnemySpawnPointData()
    {
        return spawnPoint;
    }

    /// <returns> gets the enemy's goal for the current level </returns>
    public Tuple<int, int> getLevelGoalData()
    {
        return goal;
    }
}
*/