using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //The prefab of the enemy, to be spawned
    public GameObject enemyPrefab;

    void Start() {
        //This makes the "SpawnEnemy" method repeat every 0.1 seconds, starting immediately
        InvokeRepeating("SpawnEnemy", 0f, 0.1f);
    }

    void SpawnEnemy() {
        if (GameManager.instance.ended) { //If the game has ended, destroy this component
            Destroy(this);
        }

        //Randomly select spawn location, at the same location as this object + some random x offset
        Vector3 spawnLoc = new Vector3(Random.Range(-5f, 5f), 0f, 0f) + transform.position;

        //Spawn the object at that location, as a child of this
        GameObject ob = Instantiate(enemyPrefab, spawnLoc, transform.rotation, transform);

        //Randomize the new enemy's X speed
        ob.GetComponent<EnemyBehavior>().speed = Random.Range(-10f, 10f);
    }
}
