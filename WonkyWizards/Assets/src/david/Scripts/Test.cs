using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Enemy enemy1 = new Enemy(); // static
        Debug.Log("Enemy1: ");
        enemy1.Message();
        Enemy enemy2 = new GoblinBerserker(); // dynamic
        Debug.Log("Enemy2: ");
        enemy2.Message();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
