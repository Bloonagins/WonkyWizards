using System;
using System.Collections;
using UnityEngine;

public class Summon : MonoBehaviour
{
    public static Vector3 gridCursorPoint;
    private int health;

    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        health = this.getMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual int getCost()
    {
        return 0;
    }

    public virtual int getMaxHealth()
    {
        return 0;
    }

    public int getHealth ()
    {
        return this.health;
    }

    public void resetSummonHP ()
    {
        this.health = getMaxHealth();
    }

    public void takeDamage(int damage)
    {
        this.health -= damage;
        this.health = Math.Max(0, Math.Min(100, this.health));
    }

    public void takeHealing(int healing)
    {
        this.health += healing;
        this.health = Math.Max(0, Math.Min(100, this.health));
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
            //Debug.Log("Invalid new position (null): " + pos);
            return false;
        }

        // now test array with new pos added:
        // copy array to test on
        bool[,] newArray = GameManager.getPlacementGrid().Clone() as bool[,];

        // add the proposed new position to the new array
        newArray[pos.Item1, pos.Item2] = false;

        // then test that the new grid is traversable
        //print2DArray(newArray);
        if (isTraversable(  newArray,
                            LevelManager.getLevelRows(),
                            LevelManager.getLevelCols(),
                            LevelManager.getEnemySpawnPoint(),
                            LevelManager.getLevelGoal()
        ))
        {
            GameManager.occupySpace(pos);
            GameObject newSummon = Instantiate(summon, worldPos, Quaternion.identity);

            newSummon.transform.parent = GameObject.FindGameObjectsWithTag("NavMesh")[0].transform;
            return true;
        }
        else
        {
            //Debug.Log("[Summon.cs] not traversable");
            return false;
        }
    }
    public static bool isTraversable(bool[,] grid, int rows, int cols, Tuple<int, int> start, Tuple<int, int> goal)
    {
        //Debug.Log("rows: " + rows + ", cols: " + cols + ", start: " + start + ", goal: " + goal);

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
            //Debug.Log(p);
            if (p.Equals(goal))
                return true;

            // Add neighboring locations to the queue
            for (int i = 0; i < 4; i++)
            {
                // Get position to check
                int a = p.Item1 + dir[i, 0];
                int b = p.Item2 + dir[i, 1];

                // Check that a,b is in bounds
                if (a >= 0 && b >= 0 &&
                    a < rows && b < cols)
                {
                    // create tuple for the spot we're considering
                    Tuple<int, int> spot = new Tuple<int, int>(a, b);
                    // check if the spot is the goal
                    if (spot.Equals(goal)) return true;
                    else if (grid[a, b] == true) q.Enqueue(new Tuple<int, int>(a, b));
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

