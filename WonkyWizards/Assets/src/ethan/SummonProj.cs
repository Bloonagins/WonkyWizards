using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonProj : MonoBehaviour
{
    protected float speed;
    protected int damage;
    protected float kockback;

    protected GameObject prefab;
    protected GameObject effectPrefab;

    protected Vector3 velocity;

    public int getProjDamage()
    {
        return damage;
    }

    public float getProjKnockback()
    {
        return kockback;
    }


}
