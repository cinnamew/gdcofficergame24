using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs to choose from
    public float minSpawnInterval = 10f; // Minimum time between spawns
    public float maxSpawnInterval = 15f; // Maximum time between spawns
    public float ringRadius = 5f; // Radius of the ring
    public int numEnemiesInRing = 8; // Number of enemies in the ring
    private Vector2 playerPos;

    private void Start()
    {
        playerPos = (GameObject.Find("Player")).transform.position;
        StartCoroutine(SpawnEnemiesRoutine());
    }

    private IEnumerator SpawnEnemiesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
            SpawnRingOfEnemies();
        }
    }

    private void SpawnRingOfEnemies()
    {
        GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        float angleStep = 360f / numEnemiesInRing;

        for (int i = 0; i < numEnemiesInRing; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 spawnPosition = playerPos + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * ringRadius;

            Instantiate(randomEnemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
