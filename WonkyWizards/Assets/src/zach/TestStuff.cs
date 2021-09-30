using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStuff : MonoBehaviour
{
    void Start ()
    {
        GameManager.instance.ChangeState(GameState.PLAY);
    }
}
