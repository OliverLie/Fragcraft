using UnityEngine;
using UnityEngine.Pool;

public class EventManager : MonoBehaviour
{
    [SerializeField] private Transform[] SpawnPoints;
    [SerializeField] private float timeBetweenSpawns = 5f;
    [SerializeField] private PotentialAIScript enemyPrefab;

    private float timeSinceLastSpawn;
    private IObjectPool<PotentialAIScript> enemyPool;

    // Cache references
    private Transform player;
    private ExperienceManager expManager;

    void Start()
    {
        // Setup object pool
        enemyPool = new ObjectPool<PotentialAIScript>(CreateEnemy);

        // Cache Player + ExperienceManager
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("⚠ No GameObject with tag 'Player' found in scene!");
        }

        expManager = FindFirstObjectByType<ExperienceManager>();
        if (expManager == null)
        {
            Debug.LogError("⚠ No ExperienceManager found in scene!");
        }
    }

private PotentialAIScript CreateEnemy()
{
    PotentialAIScript enemy = Instantiate(enemyPrefab); // ingen spawnPoint her
    enemy.SetPool(enemyPool);
    enemy.player = player;

    Health health = enemy.GetComponent<Health>();
    if (health != null)
    {
        enemy.EnemyAI = health;
        if (expManager != null)
            health.experience = expManager;
    }
    else
    {
        Debug.LogError("⚠ Enemy prefab mangler Health component!");
    }

    enemy.gameObject.SetActive(false); // start som inaktiv
    return enemy;
}


    void Update()
    {
    if (Time.time > timeSinceLastSpawn)
    {
        PotentialAIScript enemy = enemyPool.Get(); // hent fra pool
        int index = Random.Range(0, SpawnPoints.Length);
        Transform spawnPoint = SpawnPoints[index];
        enemy.transform.position = spawnPoint.position;
        enemy.transform.rotation = spawnPoint.rotation;
        enemy.gameObject.SetActive(true); // sørg for den bliver aktiv

        timeSinceLastSpawn = Time.time + timeBetweenSpawns;
    }

    }
}
