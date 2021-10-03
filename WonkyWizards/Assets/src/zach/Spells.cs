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
    protected float speed; //travel speed
    protected int DAMAGE; //damage the spells do
    protected float RANGE; //how far it can travel
    protected float CHARGE_TIME; //pause before actual cast
    protected float COOL_DOWN; //cooldown until next cast
}

