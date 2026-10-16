
using UnityEngine;

public class Level3KeyManager : MonoBehaviour
{
    public static Level3KeyManager Instance { get; private set; }

    [Header("Level 3 Keys")]
    public GameObject level3KeyPrefab;
    public int totalGolems = 10;
    public int requiredKeys = 3;

    [Header("Drop Chance")]
    [Range(0f, 100f)]
    public float startingDropChance = 30f;

    private int golemsKilled = 0;
    private int keysDropped = 0;
    private int keysCollected = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void GolemDefeated(Vector3 deathPosition)
    {
        golemsKilled++;

        int remainingGolems = Mathf.Max(
            0, totalGolems - golemsKilled
        );

        int keysStillNeeded = Mathf.Max(
            0, requiredKeys - keysDropped
        );

        if (keysStillNeeded <= 0)
            return;

        float dropChance = startingDropChance;

        // Increase the chance as golems run out.
        if (remainingGolems > 0)
        {
            float neededRatio =
                (float)keysStillNeeded / (remainingGolems + 1);

            dropChance = Mathf.Max(
                startingDropChance,
                neededRatio * 100f
            );
        }

        // Guarantee a key when there aren't enough
        // remaining golems to risk missing another drop.
        if (remainingGolems < keysStillNeeded)
            dropChance = 100f;

        if (Random.Range(0f, 100f) < dropChance)
        {
            DropKey(deathPosition);
        }

        Debug.Log(
            "Level 3: " + remainingGolems +
            " golems remain. Keys dropped: " +
            keysDropped + "/" + requiredKeys
        );
    }

    private void DropKey(Vector3 position)
    {
        if (level3KeyPrefab == null)
        {
            Debug.LogError("Level 3 key prefab is missing!");
            return;
        }

        GameObject key = Instantiate(
            level3KeyPrefab,
            position,
            Quaternion.identity
        );

        ItemPopOut popOut = key.GetComponent<ItemPopOut>();

        if (popOut != null)
            popOut.PopOut();

        keysDropped++;

        Debug.Log("Level 3 key dropped!");
    }

    public void KeyCollected()
    {
        keysCollected = Mathf.Min(
            keysCollected + 1,
            requiredKeys
        );

        Debug.Log(
            "Level 3 keys collected: " +
            keysCollected + "/" + requiredKeys
        );
    }
}
