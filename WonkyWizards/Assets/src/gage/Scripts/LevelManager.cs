/* Name: Gage Nardi
 * LevelManager.cs
 * Wonky Wizards -- Bloonagins
 * Description: 
 * The script first gets the current level, then loads the correct array size. 
 * The script also stores a Tuple of the enemy spawnpoint and a tuple for the goal tower coordinates.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private GameManager gm; // instance of the GameManager
    private static bool[,] level1Arr = new bool[12, 12]; // static array of level 1
    private static bool[,] level2Arr = new bool[12, 12]; // static array of level 2
    private static bool[,] level3Arr = new bool[12, 12]; // static array of level 3
    private static bool[,] currentLevelArr; // array variable to point to the current level
    
    private static Tuple<int, int> spawnPoint; // list of enemy spawn points (x, y)
    private static Tuple<int, int> goal;  // list of the 

    // int array to store the size of each level
    private static int rows;
    private static int cols;
    private int currentLevel;
  
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.getSingleton(); // get the current instance of the singleton from the GameManager script
        currentLevel = gm.getCurrentLevel(); // gets the current level from zach's GameManager script
        Initiate_Level(currentLevel); // Initialize the level array based on the current level

        gm.setLevelArray(currentLevelArr); // sets the array in the GameManager script to the current level array
    }

    void Initiate_Level(int currentLevel)
    {
        switch (currentLevel)
        {
            case 0: // first level
                rows = 12;
                cols = 12;
                Initialize_Level_1();
                currentLevelArr = level1Arr;
                spawnPoint = new Tuple<int, int>(0, 5);
                goal = new Tuple<int, int>(11, 5);

                break;
            case 1: // second level
                rows = 12;
                cols = 12;
                Initialize_Level_2();
                currentLevelArr = level2Arr;
                spawnPoint = new Tuple<int, int>(0, 5);
                goal = new Tuple<int, int>(11, 5);

                break;
            case 2: // second level
                rows = 12;
                cols = 12;
                Initialize_Level_3();
                currentLevelArr = level2Arr;
                spawnPoint = new Tuple<int, int>(0, 5);
                goal = new Tuple<int, int>(11, 5);

                break;
            default:
                Debug.Log("Current level invalide while initializing array [LevelManager.cs]");
                break;
        }
    }
    void Initialize_Level_1()
    {
        /// Initializes the level 1 bool array of traversable tiles.
        ///     - true  = is traversable
        ///     - false = not traversable
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                // set tile to traversable
                level1Arr[i, j] = true; 
    }

    void Initialize_Level_2()
    {
        /// Initializes the level 2 bool array of traversable tiles.
        ///     - true  = is traversable
        ///     - false = not traversable
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                // set tile to traversable
                level2Arr[i, j] = true; 
    }

    void Initialize_Level_3()
    {
        /// Initializes the level 3 bool array of traversable tiles.
        ///     - true  = is traversable
        ///     - false = not traversable
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                // set tile to traversable
                level3Arr[i, j] = true;
    }
    /// <returns> current static array for the level </returns>
    public static bool [,] getLevelArray()
    {
        return currentLevelArr;
    }
    /// <returns> get the current level's row size </returns>
    public static int getLevelRows()
    {
        return rows;
    }
    /// <returns> get the current level's column size </returns>
    public static int getLevelCols()
    {
        return cols;
    }
    /// <returns> gets the spawn point coordinates for the current level</returns>
    public static Tuple<int, int> getEnemySpawnPoint()
    {
        return spawnPoint;
    }
    /// <returns> gets the enemy's goal for the current level </returns>
    public static Tuple<int, int> getLevelGoal()
    {
        return goal;
    }
}
