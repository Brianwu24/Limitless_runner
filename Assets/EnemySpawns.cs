using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Vector3 spawnPoint;
    private float lastSpawnTime;

    private List<GameObject> _enemies;

    private void Start()
    {
        _enemies = new List<GameObject>();
    }

    private void Update()
    {
        // Check if it's time to spawn a new enemy
        if (Time.time - lastSpawnTime > UnityEngine.Random.Range(2, 5.5f))
        {
            SpawnEnemy();
            lastSpawnTime = Time.time;
        }


        for (int i = _enemies.Count - 1; i >= 0; i--)
        {
            GameObject enemy = _enemies[i];
            if (enemy != null && enemy.transform.position.y <= -5.5)
            {
                _enemies.RemoveAt(i);
                Destroy(enemy);
            }
        }
        
    }

    private void SpawnEnemy()
    {
        // Spawn enemy at a random position along the top border
        Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(0, 8.9f), 5f, -0f);
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, this.transform);
        _enemies.Add(newEnemy);
    }
}


