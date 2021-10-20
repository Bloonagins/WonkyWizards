using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using NUnit.Framework;
using System;

public class GridTraversability
{
    private bool[,] grid =
    {
        {true, true, true},
        {false, true, false},
        {true, true, true}
    };

    private int rows = 3;
    private int cols = 3;

    private Tuple<int, int> start = new Tuple<int, int>(0, 0);
    private Tuple<int, int> goal = new Tuple<int, int>(2, 2);

    private Tuple<int, int> newSpot = new Tuple<int, int>(1, 1);



    [UnityTest]
    public IEnumerator GridNotTraversableTest()
    {
        // this grid aught to start traversable
        Assert.True(Summon.isTraversable(grid, rows, cols, start, goal) == true);
        
        // but when we add the new spot...
        grid[newSpot.Item1, newSpot.Item2] = false;

        // and see that this grid is not traversable anymore
        Assert.True(Summon.isTraversable(grid, rows, cols, start, goal) == false);

        yield return null;
    }
}
