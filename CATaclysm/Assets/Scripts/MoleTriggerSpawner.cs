
using UnityEngine;

public class MoleTriggerSpawner : MonoBehaviour
{
    [Header("Mole Setup")]
    public GameObject molePrefab;
    public Transform[] spawnPoints;

    [Header("Puzzle Setup")]
    public LeverSwitch lever;
    public float respawnDelay = 3f;

    private GameObject currentMole;
    private bool triggered = false;
    private float respawnTimer = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered || !other.CompareTag("Player"))
            return;

        triggered = true;
        SpawnMole();
    }

    private void Update()
    {
        if (!triggered || lever == null || lever.isOn)
            return;

        if (currentMole != null)
        {
            respawnTimer = respawnDelay;
            return;
        }

        respawnTimer -= Time.deltaTime;

        if (respawnTimer <= 0f)
            SpawnMole();
    }

    private void SpawnMole()
    {
        if (molePrefab == null || spawnPoints == null ||
            spawnPoints.Length == 0)
        {
            Debug.LogWarning("Mole spawner setup is incomplete!");
            return;
        }

        if (lever == null || lever.isOn || currentMole != null)
            return;

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform selectedPoint = spawnPoints[randomIndex];

        if (selectedPoint == null)
        {
            Debug.LogWarning("A mole spawn point is missing!");
            return;
        }

        currentMole = Instantiate(
            molePrefab,
            selectedPoint.position,
            selectedPoint.rotation
        );

        respawnTimer = respawnDelay;

        Debug.Log("Mole spawned at point " + (randomIndex + 1));
    }
}
