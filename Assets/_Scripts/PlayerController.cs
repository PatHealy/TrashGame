using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Called "PlayerController" because it takes in input (i.e. "Controller") and manipulates the object called "Player"
public class PlayerController : MonoBehaviour
{
    // Speed, changeable from the inspector
    public float speed;

    // A timer indicating the remaining number of seconds the player should be invulnerable
    float deathTimer = 0f;

    // Components of this object, saved here for easy access
    Rigidbody2D rb;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        //Set our saved components
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // FixedUpdate is called 30 times per second
    void FixedUpdate()
    {
        // Start with the vector (0,0,0)
        Vector3 vel = Vector3.zero; 

        if (Input.GetKey(KeyCode.D)) { //If the D key is currently being held down
            //COMMENTED OUT ALTERNATIVE WAYS OF MOVING
            //transform.Translate(new Vector3(0.1f, 0f, 0f)); //TRANSFORM MOVEMENT
            //rb.AddForce(new Vector3(0.1f, 0f, 0f)); //MOVEMENT WITH FORCES
            vel += new Vector3(speed, 0f, 0f); // Add positive X velocity
        } 
        
        if (Input.GetKey(KeyCode.A)) {
            //transform.Translate(new Vector3(-0.1f, 0f, 0f));
            vel += new Vector3(-speed, 0f, 0f); // Add negative X velocity
        }

        if (Input.GetKey(KeyCode.W)) {
            //transform.Translate(new Vector3(0f, 0.1f, 0f));
            vel += new Vector3(0f, speed, 0f); // Add positive Y velocity
        }

        if (Input.GetKey(KeyCode.S)) {
            //transform.Translate(new Vector3(0f, -0.1f, 0f));
            vel += new Vector3(0f, -speed, 0f); // Add negative Y velocity
        }

        // Set velocity to our temporary variable
        rb.velocity = vel;

        //Tick down our deathTimer
        deathTimer -= 1f / 30f;

        //If deathTimer reaches 0, become vulnerable again
        if (deathTimer <= 0f) {
            deathTimer = 0f;
            gameObject.layer = 0; //layer 0 is the default layer, which can collide with stuff
            sr.color = Color.white; //Set color back to default
        }

        // If the game has ended, disable this object
        if (GameManager.instance.ended && rb != null) {
            Destroy(gameObject); //lowercase "gameObject" accesses the object this script is attached to
        }
    }

    //This method is called when this object collides with something
    //Requires we have a Rigidbody2D
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Enemy")) { //If collides with an object with the "enemy" tag
            transform.position = Vector3.zero; //move to the center
            deathTimer = 5f; //Become invulnerable for 5 seconds
            gameObject.layer = 8; //Move to layer 8, which doesn't collide with anything
            sr.color = Color.blue; //Set sprite to blue
            GameManager.instance.Die(); //Log player death with the game manager
        }
    }

}
