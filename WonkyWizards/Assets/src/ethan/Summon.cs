using System;
using System.Collections;
using UnityEngine;

public class Summon : MonoBehaviour
{
    public static Vector3 gridCursorPoint;

    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual int getCost()
    {
        return 0;
    }

    // Snaps a Vector3 to a grid
    public static Vector3 SnapOffset(Vector3 vec, Vector3 offset, float gridSize = 1.0f)
    {
        Vector3 snapped = vec + offset;
        snapped = new Vector3(
            Mathf.Round(snapped.x / gridSize) * gridSize,
            Mathf.Round(snapped.y / gridSize) * gridSize,
            Mathf.Round(snapped.z / gridSize) * gridSize);
        return snapped - offset;
    }

    // Returns whether or not the current grid plus one more spot is be placeable
    public static bool attemptPlacement(GameObject summon, Vector3 worldPos, Tuple<int, int> pos)
    {
        if (pos == null)
        {
            Debug.Log("Invalid new position (null): " + pos);
            return false;
        }

        // test that the proposed new position is not already occupied
        if (GameManager.getPlacementGrid()[pos.Item1, pos.Item2])
        {
            Debug.Log("[Summon.cs] Space is not placeable");
            return false;
        }

        // now test array with new pos added:
        // copy array to test on
        bool[,] newArray = GameManager.getPlacementGrid().Clone() as bool[,];
        // add the proposed new position to the new array
        newArray[pos.Item1, pos.Item2] = false;
        // then test that the new grid is traversable
        if (isTraversable(newArray, LevelManager.getLevelRows(), LevelManager.getLevelCols(), LevelManager.getEnemySpawnPoint(), LevelManager.getLevelGoal()))
        {
            Instantiate(summon, worldPos, Quaternion.identity);
            return true;
        }
        else return false;
    }
    public static bool isTraversable(bool[,] grid, int rows, int cols, Tuple<int, int> start, Tuple<int, int> goal)
    {
        // List of possible directions
        int[,] dir = { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };

        // Queue of spots to check
        Queue q = new Queue();

        // Add the spawn point to the queue first
        q.Enqueue(start);

        // Operate on the queue until it's empty
        while (q.Count > 0)
        {
            // Pull position from next queued item
            Tuple<int, int> p = (Tuple<int, int>)(q.Peek());
            // Then removes that item
            q.Dequeue();

            // Mark the position "traversed" (not traversable)
            grid[p.Item1, p.Item2] = false;

            // Check if the destination has been reached
            if (p.Equals(goal))
                return true;

            // Add neighboring locations to the queue
            for (int i = 0; i < 4; i++)
            {
                // Get position to check
                int a = p.Item1 + dir[i, 0];
                int b = p.Item2 + dir[i, 1];

                // Check if we add or not
                if (a >= 0 && b >= 0 &&
                    a < rows && b < cols &&
                    grid[a, b] == true)
                {
                    q.Enqueue(new Tuple<int, int>(a, b));
                }
            }
        }

        // Goal position was never found
        return false;
    }

    private static void print2DArray (bool[,] array)
    {
        for (int i = 0; i < LevelManager.getLevelRows(); i++)
        {
            for (int j = 0; j < LevelManager.getLevelCols(); j++)
            {
                Debug.Log("array[" + i + ", " + j + "] = " + array[i, j]);
            }
        }
    }
}

