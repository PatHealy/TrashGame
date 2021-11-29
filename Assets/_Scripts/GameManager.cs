using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; //Need this import to use textmeshpros

public class GameManager : MonoBehaviour
{
    //This public static instance is accessible from anywhere! 
    //It being static means there is exactly one of these!
    public static GameManager instance;

    //Stats on the player, basically defining the score
    private int lives = 5;
    private float startTime;
    private float time = 0f;

    // References to our two UI elements
    public TextMeshProUGUI livesCounter;
    public TextMeshProUGUI timeCounter;

    //Public field -- indicates if the game is over
    public bool ended = false;

    //On Awake this runs (before start, when the game launches)
    void Awake() {
        instance = this; //Set the public static instance to THIS object
        startTime = Time.time; //Save the current time, so we can calculate the time since start later
    }

    // Call this function when the player has died
    public void Die() {
        lives -= 1; //decrease the lives
        if (lives <= 0) {//If out of lives
            lives = 0; //keep lives from going negative
            ended = true; //Set ended to true, signals to other scripts that the game is over
        }
        livesCounter.text = "Lives: " + lives; // Update the text of lives in the UI
    }

    void FixedUpdate() {
        if (!ended) { //If the game has not ended
            time = Time.time - startTime; //calculate time since the start of the game
            timeCounter.text = "Time: " + time; //Put the proper time in the UI
        }
    }

    // NOTE: ONLY EVER CALL "GetKeyDown" in an update function! If you call it in FixedUpdate, you could miss the input (try it yourself to see what I mean)
    void Update() {
        if (ended && Input.GetKeyDown(KeyCode.R)) { //If the game is over, let the player restart the scene by pressing R
            SceneManager.LoadScene("SampleScene");
        }

        if (Input.GetKeyDown(KeyCode.Escape)) { //If the player presses Escape, close the game
            Application.Quit();
        }
    }
}
