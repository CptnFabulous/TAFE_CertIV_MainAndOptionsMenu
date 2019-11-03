using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWave : MonoBehaviour
{
    [System.Serializable]
    public class EnemyWave
    {
        public GameObject enemy; // The enemy to spawn
        public int spawnAmount; // Amount of this enemy spawned per wave
        public int groupSize; // Enemies spawn in groups of this number
        public float spawnDelay; // Time between spawning this enemy and spawning a new enemy group
    }

    public List<EnemyWave> enemies; // Allows class to be viewed in the inspector

    public Transform[] spawnPoints; // Array of transforms to spawn enemies

    public float gracePeriod; // Delay after the start of the wave, before enemies spawn

    float spawnTime; // The time until spawning another enemy, changes based on variables such as spawnDelay and gracePeriod
    float spawnTimer; // Timer used to check against spawnTime

    [HideInInspector]
    public bool allEnemiesSpawned;

    // Use this for initialization
    void Start()
    {
        spawnTime = gracePeriod; // Initiates grace period
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime; // spawnTimer continually counts up

        if (spawnTimer >= spawnTime && allEnemiesSpawned == false) // Checks if spawnTimer has reached the designated spawn delay as determined by spawnTime
        {
            SelectEnemy(); // Select a random enemy to spawn
        }
    }

    void SelectEnemy()
    {
        int enemyType = Random.Range(0, enemies.Count); // Randomly select class index
        EnemyWave enemyToSpawn = enemies[enemyType]; // Use index to obtain appropriate enemy

        if (enemyToSpawn.spawnAmount > 0) // If there are any enemies of this type to spawn
        {
            SpawnEnemy(enemyToSpawn); // Spawn appropriate enemies
        }
        else
        {
            SelectEnemy(); // Check for another enemy to spawn
        }
    }

    void SpawnEnemy(EnemyWave e)
    {
        int sq; // Temporary variable for the amount of enemies to spawn
        if (e.spawnAmount >= e.groupSize) // If there are enough enemies for a group
        {
            sq = e.groupSize; // Spawn an amount of enemies based on groupSize
        }
        else
        {
            sq = e.spawnAmount; // Spawn all remaining enemies
        }

        for(int i = 0; i < sq; i++) // Spawn an amount of enemies equal to sq
        {
            Instantiate(e.enemy, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity); // Instantiate an enemy at a randomly selected spawn point
        }
        e.spawnAmount -= sq; // Subtract an appropriate amount of enemies from spawnAmount

        spawnTime = e.spawnDelay; // Changes spawnTime to the enemy's spawn delay
        spawnTimer = 0; // Resets spawn timer

        // It is apparently better to use a coroutine for this kind of thing

        CheckToSpawn();
    }

    
    void CheckToSpawn()
    {
        int sr = 0; // Temporary variable
        foreach (EnemyWave e in enemies) // Checks all enemies
        {
            if (e.spawnAmount > 0) // If there are still enemies to spawn
            {
                sr += e.spawnAmount; // Adds amount to sr
            }
        }

        if (sr <= 0) // If every enemy only added zero, i.e. there are no more enemies to spawn
        {
            allEnemiesSpawned = true;
        }

        print("Enemies left to spawn = " + sr + ", bool allEnemiesSpawned = " + allEnemiesSpawned);
    }
    
}
