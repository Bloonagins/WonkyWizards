using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static bool[,] level1Arr = new bool[11, 11]; // static array of level 1
    private static List<bool[,]> levelMasterArray = new List<bool[,]>();
    
    private static List<Tuple<int, int>> enemySpawnPoints = new List<Tuple<int,int>>();
    private static List<Tuple<int, int>> goals = new List<Tuple<int,int>>();

    // int array to store the size of each level
    private static int[] row = {12};
    private static int[] col = {12};

  
    // Start is called before the first frame update
    void Start()
    {
        levelMasterArray.Add(level1Arr);
        enemySpawnPoints.Add(new Tuple<int, int>(0, 5));
        goals.Add(new Tuple<int, int>(11, 5));
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void Initialize_Level_1()
    {
        /// Initializes the level 1 bool array of traversable tiles.
        ///     - true  = is traversable
        ///     - false = not traversable
        for (int i = 0; i < row[0]; i++)
        {
            for (int j = 0; j < col[0]; j++)
            {
                // set positions certain to non-traversable tiles
                if ((i == 1 && j == 2) || (i == 1 && j == 9) || 
                    (i == 4 && j == 2) || (i == 4 && j == 9) || 
                    (i == 7 && j == 2) || (i == 7 && j == 9) || 
                    (i == 11 && j == 5))
                {
                    level1Arr[i, j] = false; // set tile to not traversable
                }
                else
                {
                    level1Arr[i, j] = true; // set tile to traversable
                }
            }
        }
    }

    /// <returns> current static array for the level </returns>
    public static bool [,] getLevelArray()
    {
        return levelMasterArray.ToArray()[GameManager.getCurrentLevel()];
    }
    /// <returns> get the current level's row size </returns>
    public static int getLevelRows()
    {
        return row[GameManager.getCurrentLevel()];
    }
    public static int getLevelCols()
    {
        return col[GameManager.getCurrentLevel()];
    }
    public static Tuple<int, int> getEnemySpawnPoint()
    {
        return enemySpawnPoints[GameManager.getCurrentLevel()];
    }
    public static Tuple<int, int> getLevelGoal()
    {
        return goals[GameManager.getCurrentLevel()];
    }
}
