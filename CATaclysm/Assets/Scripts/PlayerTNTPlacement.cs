
using UnityEngine;

public class PlayerTNTPlacement : MonoBehaviour
{
    [Header("TNT Settings")]
    public GameObject tntPrefab;
    public KeyCode placementKey = KeyCode.F;
    public float dropDistance = 0.6f;

    private PlayerItemInventory inventory;
    private Vector2 lastMoveDirection = Vector2.down;
    private Vector3 previousPosition;

    private void Start()
    {
        inventory = GetComponent<PlayerItemInventory>();
        previousPosition = transform.position;
    }

    private void Update()
    {
        // Determine which direction the player is moving.
        Vector2 movement = (Vector2)(transform.position - previousPosition);

        if (movement.sqrMagnitude > 0.000001f)
        {
            lastMoveDirection = movement.normalized;
        }

        previousPosition = transform.position;

        // Press F to place TNT.
        if (!Input.GetKeyDown(placementKey))
            return;

        if (inventory == null || inventory.tntCount <= 0)
            return;

        if (tntPrefab == null)
        {
            Debug.LogWarning("TNT prefab is not assigned!");
            return;
        }

        // Drop TNT behind the player.
        Vector3 dropPosition = transform.position -
            (Vector3)(lastMoveDirection * dropDistance);

        // Only consume TNT when placing it.
        if (inventory.UseTNT())
        {
            Instantiate(tntPrefab, dropPosition, Quaternion.identity);

            Debug.Log("TNT placed! Remaining: " + inventory.tntCount);
        }
    }
}
