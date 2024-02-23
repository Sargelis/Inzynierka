using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave   //musi public
    {
        public string waveName;
        [HideInInspector] public int waveQuota;   //total enemies
        public float spawnInterval;
        [HideInInspector] public int spawnCount;  //number enemies spawned
        public List<EnemyGroup> enemyGroups;    //List of gropu enemis to spawn
    }
    [System.Serializable]
    public class EnemyGroup //musi public
    {
        public string enemyName;
        public int enemyCount;  //number of enemies to spawn
        [HideInInspector] public int spawnCount;  //enemies already spawned
        public GameObject enemyPrefab;
    }
    [System.Serializable]
    public class Bosses
    {
        public string bossName;
        public GameObject bossPrefab;
    }

    [SerializeField] List<Wave> waves;    //List of waves
    int currentWaveCount;
    Transform player;
    [Header("Spawner Attributes")]
    float spawnTimer; //when spawn next enemy
    [SerializeField] float waveInterval;  //interval between each wave
    float waveCooldown;
    [HideInInspector] public int enemiesAlive;
    [SerializeField] int maxEnemiesAllowed;   //max of enemies on map
    bool maxEnemiesReached = false;
    [Header("Bosses")]
    [SerializeField] List<Bosses> bosses;
    int i = 0;
    [SerializeField] float bossTimer;
    float bossCooldown;
    [Header("Spawn Positions")]
    public List<Transform> relativeSpawnPoints; //musi public

    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        CalculateWaveQuota();
        bossCooldown = bossTimer;
        waveCooldown = waveInterval;
    }
    void Update()
    {
        waveInterval -= Time.deltaTime;
        if (currentWaveCount < waves.Count)
        {
            if (waves[currentWaveCount].spawnCount >= waves[currentWaveCount].waveQuota || waveInterval <= 0)
            {
                NextWave();
                waveInterval = waveCooldown;
            }
        }

        bossTimer -= Time.deltaTime;
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= waves[currentWaveCount].spawnInterval)    //check when spawn next enemy
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }

        if (bossTimer <= 0 || waves[currentWaveCount].spawnCount >= waves[currentWaveCount].waveQuota)
        {
            SpawnBoss();
            bossTimer = bossCooldown;
            i++;
        }
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups) currentWaveQuota += enemyGroup.enemyCount;

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.LogWarning(currentWaveQuota);
    }
    void SpawnEnemies()
    {
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached) //check if min number of enemies have been spawned
        {
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups) //spawn each type of enemy until quota is filled
            {
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)   //check if min number of enemis of this type have been spawned
                {
                    if (enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }

                    GameObject mob = Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);
                    mob.transform.SetParent(this.transform);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }

        if (enemiesAlive < maxEnemiesAllowed) maxEnemiesReached = false;
    }
    public void NextWave()
    {
        if(currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }
    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }
    void SpawnBoss()
    {
        Instantiate(bosses[i].bossPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);
        enemiesAlive++;
    }
}