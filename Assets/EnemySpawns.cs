using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector3 spawnPoint;
    private float lastSpawnTime;

    private void Update()
    {
        // Check if it's time to spawn a new enemy
        if (Time.time - lastSpawnTime > Random.Range(1f, 5f))
        {
            SpawnEnemy();
            lastSpawnTime = Time.time;
        }
    }

    private void SpawnEnemy()
    {
        // Instantiate enemy at the spawn point
        GameObject newEnemy = new GameObject("Enemy");
        newEnemy.transform.position = spawnPoint;

        // Attach the EnemyController script to the spawned enemy
        newEnemy.AddComponent<EnemyController>();
    }
}

public class EnemyController : MonoBehaviour
{
    private static readonly float removalXPosition = -10f;
    private float speed = 2f; // Speed of enemy movement to the left

    private void Update()
    {
        // Move the enemy to the left
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Check if the enemy is past the left border, remove it if true
        if (transform.position.x < removalXPosition)
        {
            Destroy(gameObject);
        }
    }
}