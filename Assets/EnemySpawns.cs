using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    private float lastSpawnTime;

    private void Update()
    {
        // Check if it's time to spawn a new enemy
        if (Time.time - lastSpawnTime > UnityEngine.Random.Range(1f, 5f))
        {
            SpawnEnemy();
            lastSpawnTime = Time.time;
        }
    }

    private void SpawnEnemy()
    {
        // Spawn enemy at a random position along the top border
        Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-8.9f, 8.9f), 10f, 0f);
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Attach the Enemy component to the spawned enemy
        newEnemy.AddComponent<Enemy>();
    }
}

public class Enemy : MonoBehaviour
{
    private static readonly float removalXPosition = -10f;

    private void Update()
    {
        // Move the enemy downwards
        transform.Translate(Vector3.down * Time.deltaTime);

        // Check if the enemy is past the left border, remove it if true
        if (transform.position.x < removalXPosition)
        {
            Destroy(gameObject);
        }
    }
}