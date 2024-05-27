using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs to choose from
    public GameObject[] bossPrefabs;
    public float minSpawnInterval = 10f; // Minimum time between spawns
    public float maxSpawnInterval = 15f; // Maximum time between spawns
    public float ringRadius = 5f; // Radius of the ring
    public int numEnemiesInRing = 8; // Number of enemies in the ring
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask WallLayer;

    private void Start()
    {
        Debug.Log("STARARRRTTAQTATATATT");
        playerTransform = GameObject.FindWithTag("Player").transform;
        SpawnStage1();
        // StartCoroutine(SpawnEnemiesRoutine());
        // StartCoroutine(FormationSpawnRoutine());
    }

    
    private void SpawnStage1()
    {
        GameObject[] generalEnemies = {enemyPrefabs[0], enemyPrefabs[1]};
        GameObject[] flockEnemies = {enemyPrefabs[2]};
        GameObject[] wallEnemies = {enemyPrefabs[3]};
        StartCoroutine(SpawnEnemiesGeneral(generalEnemies, 8, 20f, 20f));
        StartCoroutine(SpawnEnemyFlocks(generalEnemies, 4, 30f));
        StartCoroutine(SpawnEnemyWalls(wallEnemies, 4, 20f));
        StartCoroutine(SpawnStage1Bosses());

    }
    private IEnumerator SpawnStage1Bosses(){
        yield return new WaitForSeconds(20f);
        SpawnBoss(bossPrefabs[0]);
        yield return new WaitForSeconds(30f);
        SpawnBoss(bossPrefabs[0]);
    }

    private void SpawnBoss(GameObject boss){
        Vector2 spawnPosition = (Vector2)playerTransform.position + Random.insideUnitCircle * 10f;
        Instantiate(boss, spawnPosition, Quaternion.identity);
    }

    private IEnumerator SpawnEnemyFlocks(GameObject[] enemies,  int numWaves, float cooldown){
        for (int i = 0; i < numWaves; i++){
            yield return new WaitForSeconds(cooldown);
            transform.position = playerTransform.position;
            GameObject randomEnemyPrefab = enemies[Random.Range(0, enemies.Length)];
            SpawnFormationOfEnemies("FlockFormation" + Random.Range(1, 3), randomEnemyPrefab, 7f);
        }
    }

    private IEnumerator SpawnEnemyWalls(GameObject[] enemies,  int numWaves, float cooldown)
    {
        for (int i = 0; i < numWaves; i++){
            transform.position = playerTransform.position;
            GameObject randomEnemyPrefab = enemies[Random.Range(0, enemies.Length)];
            SpawnFormationOfEnemies("Formation" + Random.Range(1, 3), randomEnemyPrefab);
            yield return new WaitForSeconds(cooldown);
        }
    }
    private IEnumerator SpawnEnemiesGeneral(GameObject[] enemies, int numWaves, float cooldown, float range){
        for (int i = 0; i < numWaves; i++){
            int numSpawns = (int)(8*Mathf.Log(i + 1) + 15);
            int enemyIndex = Mathf.Min((int)(enemies.Length*i/(numWaves)), enemies.Length - 1);
            for (int j = 0; j < numSpawns; j++){
                Vector2 spawnPosition = (Vector2)playerTransform.position + Random.insideUnitCircle * range;
                Instantiate(enemies[enemyIndex], spawnPosition, Quaternion.identity);
            }
            yield return new WaitForSeconds(cooldown);
        }
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

    

    private void SpawnFormationOfEnemies(string formationName, GameObject spawnedEnemy)
    {
        Vector2[] spawnPoints = FindSpawnPoints(formationName);
        Vector2[] endPoints = FindEndPoints(formationName);
        GameObject[] enemies = new GameObject[spawnPoints.Length];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject enemy = Instantiate(spawnedEnemy, spawnPoints[i], Quaternion.identity);
            enemy.GetComponent<EnemyAI>().SetEndPoint(endPoints[i]);
            enemies[i] = enemy;
            //yield return new WaitForSeconds(0.1f); // Optional yield to control spawn rate
        }
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            enemies[i].GetComponent<EnemyAI>().SetEnableAim(true);
            enemies[i].GetComponent<EnemyAI>().SetEnableAim(true);
        }
    }

    private void SpawnFormationOfEnemies(string formationName, GameObject spawnedEnemy, float speed)
    {
        Vector2[] spawnPoints = FindSpawnPoints(formationName);
        Vector2[] endPoints = FindEndPoints(formationName);
        GameObject[] enemies = new GameObject[spawnPoints.Length];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject enemy = Instantiate(spawnedEnemy, spawnPoints[i], Quaternion.identity);
            enemy.GetComponent<EnemyAI>().SetEndPoint(endPoints[i]);
            enemies[i] = enemy;
            //yield return new WaitForSeconds(0.1f); // Optional yield to control spawn rate
        }
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            enemies[i].GetComponent<EnemyAI>().SetEnableAim(true);
            enemies[i].GetComponent<EnemyAI>().SetMoveSpeed(speed);
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

    private Vector2[] FindSpawnPoints(string formationName){
        Transform formation = transform.Find(formationName);
        Debug.Log(formationName + "     PSDIJFPOSJIFOPJSDIOFJSDOIPFJSIOFJIOSJFPOISDFPOJPFDS");
        return GetChildPositionsByPrefix(formation, "Spawn");
    }

    private Vector2[] FindEndPoints(string formationName){
        Transform formation = transform.Find(formationName);
        return GetChildPositionsByPrefix(formation, "End");
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
