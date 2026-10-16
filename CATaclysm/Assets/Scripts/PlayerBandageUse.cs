
using UnityEngine;

public class PlayerBandageUse : MonoBehaviour
{
    [Header("Bandage Settings")]
    public KeyCode useKey = KeyCode.G;
    public float healAmount = 2f;

    private PlayerItemInventory inventory;
    private Health playerHealth;

    private void Start()
    {
        inventory = GetComponent<PlayerItemInventory>();
        playerHealth = GetComponent<Health>();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(useKey))
            return;

        if (inventory == null || playerHealth == null)
            return;

        if (inventory.bandageCount <= 0)
            return;

        // Don't waste bandages at full health.
        if (playerHealth.curr_Health >= playerHealth.maxHealth)
            return;

        // Consume one bandage and restore health.
        if (inventory.UseBandage())
        {
            playerHealth.Heal(healAmount);

            Debug.Log("Bandage used! Health: " +
                playerHealth.curr_Health +
                " | Bandages remaining: " +
                inventory.bandageCount);
        }
    }
}
