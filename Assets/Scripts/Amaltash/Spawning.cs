using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public Transform[] spawnPoints; // Array of spawn points
    public float spawnInterval = 5f; // Time interval between spawns

    private float timer; // Timer to track spawn intervals

    void Start()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab not assigned.");
        }

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned.");
        }

        timer = spawnInterval; // Initialize the timer with the spawn interval
    }

    void Update()
    {
        timer -= Time.deltaTime; // Decrease the timer by the time passed since last frame

        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = spawnInterval; // Reset the timer
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab != null && spawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length); // Select a random spawn point
            Transform spawnPoint = spawnPoints[randomIndex]; // Get the spawn point transform

            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation); // Spawn the enemy at the selected spawn point
        }
    }
}
