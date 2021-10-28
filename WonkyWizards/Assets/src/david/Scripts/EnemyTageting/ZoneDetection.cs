using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDetection : MonoBehaviour
{
    // The parent of the Zone
    public GameObject goblinParent;
    // The goblin warrior the zone is attached too
    private GoblinWarrior goblinWarrior;

    // Start is called before the first frame update
    void Start()
    {
        // Get GoblinWarrior component
        goblinWarrior = goblinParent.GetComponent<GoblinWarrior>(); 
        
    }
    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Collision Entered");
        GameObject other = collision.gameObject;
        if (other.tag == "Enemy") {
            goblinWarrior.ChangeDamage(goblinWarrior.GetDamageBoost()); // Add damage
            //Debug.Log("Added damage");
        }
        else {
            //Physics.IgnoreCollision(other.GetComponent<Collider>(), GetComponent<Collider>());
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerExit");
        GameObject other = collision.gameObject;
        if (other.tag == "Enemy") {
            goblinWarrior.ChangeDamage(-goblinWarrior.GetDamageBoost()); // Remove damage
            //Debug.Log("Removed damage");
        }
    }

}
