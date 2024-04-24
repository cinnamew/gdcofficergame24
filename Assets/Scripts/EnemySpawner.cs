using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs to choose from
    public float minSpawnInterval = 10f; // Minimum time between spawns
    public float maxSpawnInterval = 15f; // Maximum time between spawns
    public float ringRadius = 5f; // Radius of the ring
    public int numEnemiesInRing = 8; // Number of enemies in the ring
    [SerializeField] private Transform playerTransform;
    private Vector2[] spawnPoints;
    private Vector2[] endPoints;

    private void Start()
    {
        LoadSpawnAndEndPoints("Formation1");
        StartCoroutine(SpawnEnemiesRoutine());
        StartCoroutine(FormationSpawnRoutine());
    }

    private IEnumerator SpawnEnemiesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
            transform.position = playerTransform.position;
            SpawnRingOfEnemies();
        }
    }

    private IEnumerator FormationSpawnRoutine()
    {
        while (true)
        {
            transform.position = playerTransform.position;
            SpawnFormationOfEnemies();
            yield return new WaitForSeconds(Random.Range(minSpawnInterval*3, maxSpawnInterval*3));
        }
        
    }

    private void SpawnFormationOfEnemies()
    {
        GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        GameObject[] enemies = new GameObject[spawnPoints.Length];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject enemy = Instantiate(randomEnemyPrefab, spawnPoints[i], Quaternion.identity);
            enemy.GetComponent<EnemyAI>().SetEndPoint(endPoints[i]);
            enemies[i] = enemy;
            //yield return new WaitForSeconds(0.1f); // Optional yield to control spawn rate
        }
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            enemies[i].GetComponent<EnemyAI>().SetEnableAim(true);
        }
    }

    private void SpawnRingOfEnemies()
    {
        GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        float angleStep = 360f / numEnemiesInRing;

        for (int i = 0; i < numEnemiesInRing; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector2 spawnPosition = (Vector2)(transform.position) + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * ringRadius;

            Instantiate(randomEnemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
    private void LoadSpawnAndEndPoints(string formationName)
    {
        
        Transform formation = transform.Find(formationName);
        if (formation != null)
        {
            spawnPoints = GetChildPositionsByPrefix(formation, "Spawn");
            endPoints = GetChildPositionsByPrefix(formation, "End");
        }
        else
        {
            Debug.LogError(formationName + " object not found!");
        }
    }

    private Vector2[] GetChildPositionsByPrefix(Transform parent, string prefix)
    {
        Vector2[] positions = new Vector2[parent.childCount];
        int count = 0;
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.name.StartsWith(prefix))
            {
                positions[count] = child.position;
                count++;
            }
        }
        System.Array.Resize(ref positions, count);

        return positions;
    }
}
