using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierScript : MonoBehaviour
{
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
    public Vector3 SnapOffset(Vector3 vec, Vector3 offset, float gridSize = 1.0f)
    {
        Vector3 snapped = vec + offset;
        snapped = new Vector3(
            Mathf.Round(snapped.x / gridSize) * gridSize,
            Mathf.Round(snapped.y / gridSize) * gridSize,
            Mathf.Round(snapped.z / gridSize) * gridSize);
        return snapped - offset;
    }
}