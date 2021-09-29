/**********************************************
| Spells V1.4.0                               |
| Author: Zach Heimbigner, T3                 |
| Description: This program manages the spells|              
| of the game                                 |
| Bugs:                                       |
**********************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    //spell vars
    public float speed; //travel speed
    public float[] DAMAGE= {0f, 0f}; //first index is DPS, second is duration
    public float RANGE; //how far it can travel
    public float CHARGE_TIME; //pause before actual cast
    public float COOL_DOWN; //cooldown until next cast

    //on hit behaviour
    public float[] on_hit(){
        return this.DAMAGE;
    }
}

