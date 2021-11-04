using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using NUnit.Framework;
using System;

public class GridTraversability
{
    private static bool[,] level1Arr; // static array of level 1
    private static int rows;
    private static int cols;

    private static Tuple<int, int> spawnPoint;
    private static Tuple<int, int> goal;

    private Tuple<int, int>[] newSpots =
        {
            new Tuple<int, int>(0, 4),
            new Tuple<int, int>(1, 5),
            new Tuple<int, int>(1, 6),
            new Tuple<int, int>(0, 7)
        };

    [UnityTest]
    public IEnumerator GridNotTraversableTest()
    {
        level1Arr = new bool[12, 12];
        rows = 12;
        cols = 12;
        spawnPoint = new Tuple<int, int>(0, 5);
        goal = new Tuple<int, int>(11, 5);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
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

        // this grid aught to start traversable
        Assert.True(Summon.isTraversable(level1Arr,  rows, cols, spawnPoint, goal) == true);

        // but when we add the new spot...
        bool[,] newGrid = level1Arr.Clone() as bool[,];

        for (int i = 0; i < newSpots.Length; i++)
        {
            newGrid[newSpots[i].Item1, newSpots[i].Item2] = false;
        }

        // and see that this grid is not traversable anymore
        Assert.True(Summon.isTraversable(newGrid, rows, cols, spawnPoint, goal) == false);

        yield return null;
    }
}

