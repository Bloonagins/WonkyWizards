using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static bool[,] level1Arr = new bool[11, 11]; // static array of level 1
    int row, col;

    // Start is called before the first frame update
    void Start()
    {
        //
        for(row = 0; row < 12; row++)
        {
            for (col = 0; col < 12; col++)
            {
                // check positions if they are on nontraversable tiles
                if ((row == 1 && col == 2) || (row == 1 && col == 9) || (row == 4 && col == 2) || (row == 4 && col == 9) || (row == 7 && col == 2) || (row == 7 && col == 9) || (row == 11 && col == 5))
                {
                    level1Arr[row, col] = false; // set tile to not traversable
                }
                else
                {
                    level1Arr[row, col] = true; // set tile to traversable
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
