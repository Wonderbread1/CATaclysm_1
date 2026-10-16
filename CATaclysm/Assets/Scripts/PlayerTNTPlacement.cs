
using UnityEngine;

public class PlayerTNTPlacement : MonoBehaviour
{
    [Header("TNT Settings")]
    public GameObject tntPrefab;
    public KeyCode placementKey = KeyCode.F;

    [Header("Placement Position")]
    public Vector2 feetOffset = Vector2.zero;

    private PlayerItemInventory inventory;

    private void Start()
    {
        inventory = GetComponent<PlayerItemInventory>();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(placementKey))
            return;

        if (inventory == null || inventory.tntCount <= 0)
            return;

        if (tntPrefab == null)
        {
            Debug.LogWarning("TNT prefab is not assigned!");
            return;
        }

        Vector3 dropPosition = transform.position +
            new Vector3(feetOffset.x, feetOffset.y, 0f);

        if (inventory.UseTNT())
        {
            Instantiate(tntPrefab, dropPosition, Quaternion.identity);

            Debug.Log("TNT placed! Remaining: " + inventory.tntCount);
        }
    }
}
