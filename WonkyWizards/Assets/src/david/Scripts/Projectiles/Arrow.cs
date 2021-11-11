using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Transform player;
    private Transform goal;
    private Transform target;
    private float speed = 40.0f;

    private int damage = 40;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        goal = GameObject.FindGameObjectWithTag("Goal").GetComponent<Transform>();
        
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null) {
            Debug.Log("NUll");
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag !="Enemy" && collision.gameObject.tag != "Spell" && collision.gameObject.tag != "Zone" && collision.gameObject.tag != "SummonProjectile" && collision.gameObject.tag != "SummonNoPlace") 
        {
            Destroy(gameObject);
        }
    }
    public int GetDamage()
    {
        return damage;
    }
}
