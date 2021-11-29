using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    //Make the speed editable in inspector
    public float speed;

    //Component of the gameobject, saved here for easy access
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //At start, save a reference to the rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        //Set the velocity as (1,0,0) * the speed
        rb.velocity = Vector3.right * speed; // new Vector3(1f,0f,0f);
    }
    
    //On collision with any other object
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Kill")) { //If collides with something tagged "Kill", destroy self
            Destroy(gameObject);
        }
    }

    void FixedUpdate() {
        if (GameManager.instance.ended && rb != null) { //If game has ended, destroy rigidbody2D so physics stops being simulated
            Destroy(rb);
        }
    }
}
