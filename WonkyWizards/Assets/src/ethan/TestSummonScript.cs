/*
 *  The purpose of this script is to demonstrate how the placement
 *  of a summon will be verified before being allowed to be placed.
 */


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSummonScript : MonoBehaviour
{
    // Size of grid (retrieve from Gage's static level info for current level)
    int rows, cols;

    // Position of spawn point and goal tower (retrieve from Gage's static level info for current level)
    Tuple<int, int> start, goal;

    
    void Start()
    {
        // Let the size of our example grid be 5 x 5
        rows = cols = 5;

        // And let the spawn point be in the top left, and the goal in the bottom right.
        start = new Tuple<int, int>(0, 0);
        goal = new Tuple<int, int>(4, 4);

        // Given a grid that is valid, such as...
        bool[,] grid1 = {
            {true, true, true, true, true},
            {false, true, true, true, true}, // one non traversable spot
            {true, true, true, true, true},
            {true, true, true, true, true},
            {true, true, true, true, true}
        };  // This "grid1" would need to be a reference to the current level's grid (in Gage's(?) code)

        // We should get "Traversable."
        Debug.Log("Grid in question:" + grid1);
        if (Summon.isTraversable(grid1, rows, cols, start, goal))
            Debug.Log("Traversable.");
        else Debug.Log("Not traversable.");

        // Given a grid that is NOT valid, such as...
        bool[,] grid2 = {
            {true, false, true, true, true}, // added a new non-trav. spot
            {false, true, true, true, true}, // same old non-trav. spot
            {true, true, true, true, true},
            {true, true, true, true, true},
            {true, true, true, true, true}
        };  // Again, the grid is from the current level's updated grid

        // We should get "Not traversable."
        Debug.Log("Grid in question:" + grid1);
        if (Summon.isTraversable(grid1, rows, cols, start, goal))
            Debug.Log("Traversable.");
        else Debug.Log("Not traversable.");

        // The above is how we are able to check if a position 
    }
}
