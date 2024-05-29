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
        GameObject[] generalEnemies = {enemyPrefabs[1], enemyPrefabs[3], enemyPrefabs[5], enemyPrefabs[4]};
        GameObject[] flockEnemies = {enemyPrefabs[7],enemyPrefabs[4],enemyPrefabs[2]};
        GameObject[] wallEnemies = {enemyPrefabs[0]};
        GameObject[] ringEnemies = {enemyPrefabs[2],enemyPrefabs[6]};
        StartCoroutine(SpawnEnemiesGeneral(generalEnemies, 16, 20f, 20f));
        StartCoroutine(SpawnEnemyFlocks(generalEnemies, 16, 20));
        StartCoroutine(SpawnEnemyWalls(wallEnemies, 12, 30f));
        StartCoroutine(SpawnEnemyRings(ringEnemies, 12, 25f));
        StartCoroutine(SpawnStage1Bosses());

    }
    private IEnumerator SpawnStage1Bosses(){
        yield return new WaitForSeconds(50f);
        SpawnBoss(bossPrefabs[0], false);
        yield return new WaitForSeconds(250f);
        SpawnBoss(bossPrefabs[0], true);
    }

    private void BuffBoss(GameObject boss){
        boss.GetComponent<EnemyAttack>().setDamage(100);
        boss.GetComponent<Boss>().SetShotInterval(3f);
        boss.GetComponent<Health>().SetIsFinalBoss(true);
        boss.GetComponent<Health>().SetMaxHealth(10000);
    }

    private IEnumerator SpawnEnemyRings(GameObject[] enemies, int numWaves, float cooldown){
        for (int i = 0; i < numWaves; i++){
            yield return new WaitForSeconds(cooldown);
            GameObject randomEnemyPrefab = enemies[Random.Range(0, enemies.Length)];
            SpawnRingOfEnemies(randomEnemyPrefab);
        }
    }
    private void SpawnBoss(GameObject boss, bool buffBoss){
        Vector2 spawnPosition = (Vector2)playerTransform.position + Random.insideUnitCircle * 10f;
            GameObject spawnedBoss = Instantiate(boss, spawnPosition, Quaternion.identity);
        if (buffBoss){     
            BuffBoss(spawnedBoss);
        }
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
            SpawnFormationOfEnemies("Formation" + Random.Range(1, 5), randomEnemyPrefab);
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

    private void SpawnRingOfEnemies(GameObject spawnEnemy)
    {
        float angleStep = 360f / numEnemiesInRing;

        for (int i = 0; i < numEnemiesInRing; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector2 spawnPosition = (Vector2)(playerTransform.position) + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * ringRadius;

            Instantiate(spawnEnemy, spawnPosition, Quaternion.identity);
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
