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
        offset = new Vector3(4, 4, 0);
        transform.position = SnapOffset(PlayerScript.worldCursorPoint, offset, 8);
    }

    // Update is called once per frame
    void Update()
    {

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

    // Returns whether or not a given grid 
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

    //public static bool isTraversable(bool[,] grid, int rows, int cols, Tuple<int, int> start, Tuple<int, int> goal, Tuple<int, int> plus)
    //{

    //}
}