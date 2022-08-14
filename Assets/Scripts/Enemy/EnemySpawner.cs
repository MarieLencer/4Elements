using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    private List<GameObject> activeEnemies = new List<GameObject>();

    private int amountEnemiesSpawn = 5;

    private int WaveCount = 0;

    private int maxWaves = 4;

    private Vector3[] spawnPositions = new Vector3[]
    {
        new Vector3(-7.5f, 0f, 15f),
        new Vector3(7.5f, 0f, 15f),
        new Vector3(-7.5f, 0f, 30f),
        new Vector3(7.5f, 0f, 30f)
    };
    
    private void Update()
    {
        if (activeEnemies.Count == 0 || activeEnemies == null)
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    IEnumerator SpawnEnemies()
    {
        if (WaveCount <= maxWaves)
        {
            SetUpNewWave();
            for (int i = 0; i < amountEnemiesSpawn; i++)
            {
                int spawnPositionIndex = i % spawnPositions.Length;
                Vector3 nextSpawnposition = spawnPositions[spawnPositionIndex];
                nextSpawnposition.z += UnityEngine.Random.Range(-5f, 5f);
                GameObject enemy = Instantiate(enemyPrefab, nextSpawnposition, Quaternion.identity, this.transform);
                enemy.GetComponent<EnemyLife>().Spawn();
                activeEnemies.Add(enemy);
                yield return new WaitForSeconds(1f);
            }
        }

        yield return null;
    }

    private void SetUpNewWave()
    {
        WaveCount++;
        amountEnemiesSpawn += WaveCount;
    }

    public void removeEnemy(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }
}
