
using UnityEngine;

public class MoleAmbushSpawner : MonoBehaviour
{
    [Header("Mole Setup")]
    public GameObject molePrefab;
    public Transform[] spawnPoints;

    [Header("Spawn Settings")]
    public float spawnDelay = 0.5f;
    public bool triggerOnce = true;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (triggerOnce && triggered)
            return;

        triggered = true;
        StartCoroutine(SpawnMoles());
    }

    private System.Collections.IEnumerator SpawnMoles()
    {
        foreach (Transform point in spawnPoints)
        {
            if (point != null && molePrefab != null)
            {
                Instantiate(
                    molePrefab,
                    point.position,
                    point.rotation
                );
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
