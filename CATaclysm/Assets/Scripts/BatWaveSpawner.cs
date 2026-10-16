
using UnityEngine;

public class BatWaveSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject batPrefab;
    public Transform[] spawnPoints;

    [Header("Wave Settings")]
    public int totalWaves = 4;
    public int batsPerWave = 3;
    public float timeBetweenWaves = 2f;

    [Header("Spawn Spacing")]
    public float minimumBatSpacing = 0.75f;

    [Header("Key Reward")]
    public GameObject keyPrefab;

    private GameObject[] activeBats;
    private int currentWave = 0;
    private bool waitingForNextWave = false;
    private bool finished = false;
    private float waveTimer = 0f;

    private Vector3 lastBatPosition;

    void Start()
    {
        SpawnWave();
    }

    void Update()
    {
        if (finished || activeBats == null)
            return;

        int livingBats = 0;

        foreach (GameObject bat in activeBats)
        {
            if (bat != null)
            {
                livingBats++;
                lastBatPosition = bat.transform.position;
            }
        }

        if (livingBats > 0)
            return;

        if (currentWave >= totalWaves)
        {
            finished = true;
            DropKey();
            return;
        }

        if (!waitingForNextWave)
        {
            waitingForNextWave = true;
            waveTimer = timeBetweenWaves;
        }

        waveTimer -= Time.deltaTime;

        if (waveTimer <= 0f)
        {
            waitingForNextWave = false;
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        if (batPrefab == null || spawnPoints == null ||
            spawnPoints.Length == 0 || batsPerWave <= 0 ||
            totalWaves <= 0)
        {
            Debug.LogWarning("Bat spawner setup is incomplete!");
            enabled = false;
            return;
        }

        currentWave++;
        activeBats = new GameObject[batsPerWave];

        Debug.Log("Starting bat wave " + currentWave);

        for (int i = 0; i < batsPerWave; i++)
        {
            int randomPoint = Random.Range(0, spawnPoints.Length);
            Transform spawn = spawnPoints[randomPoint];

            Vector3 spawnPosition = FindSpawnPosition(
                spawn.position,
                i
            );

            activeBats[i] = Instantiate(
                batPrefab,
                spawnPosition,
                spawn.rotation
            );
        }
    }

    Vector3 FindSpawnPosition(Vector3 center, int batIndex)
    {
        // First bat can spawn directly at its entrance.
        if (batIndex == 0)
            return center;

        // Try nearby positions until there's enough spacing.
        for (int attempt = 0; attempt < 30; attempt++)
        {
            Vector2 offset = Random.insideUnitCircle * 1.5f;

            Vector3 candidate = center + new Vector3(
                offset.x,
                offset.y,
                0f
            );

            bool tooClose = false;

            for (int j = 0; j < batIndex; j++)
            {
                if (activeBats[j] != null &&
                    Vector2.Distance(
                        candidate,
                        activeBats[j].transform.position
                    ) < minimumBatSpacing)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
                return candidate;
        }

        // Fallback: use an unused entrance if possible.
        foreach (Transform point in spawnPoints)
        {
            if (point == null)
                continue;

            bool tooClose = false;

            for (int j = 0; j < batIndex; j++)
            {
                if (activeBats[j] != null &&
                    Vector2.Distance(
                        point.position,
                        activeBats[j].transform.position
                    ) < minimumBatSpacing)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
                return point.position;
        }

        Debug.LogWarning("Could not find a fully separated bat spawn position.");
        return center;
    }

    void DropKey()
    {
        Debug.Log("Final bat defeated! Dropping Level 1 key.");

        if (keyPrefab != null)
        {
            Instantiate(
                keyPrefab,
                lastBatPosition,
                Quaternion.identity
            );
        }
    }
}
