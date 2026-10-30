
using UnityEngine;

public class PlayerItemInventory : MonoBehaviour
{
    [Header("Consumable Inventory")]
    public int tntCount = 0;
    public int bandageCount = 0;

    [Header("Maximum Carrying Capacity")]
    public int maxTNT = 2;
    public int maxBandages = 3;

    [Header("Bandage Healing")]
    public float bandageHealAmount = 2f;

    private Health playerHealth;

    public InventoryManager inventoryManager;

    void Awake()
    {
        playerHealth = GetComponent<Health>();

        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            UseBandage();
        }
    }

    public bool CollectTNT()
    {
        if (inventoryManager != null && !inventoryManager.HasAvailableSlot())
        {
            return false;
        }

        if (tntCount >= maxTNT)
        {
            Debug.Log("TNT inventory full!");
            return false;
        }

        tntCount++;
        Debug.Log("TNT collected! Total: " + tntCount);
        inventoryManager.PickUpItem("Dynamite");
        return true;
    }

    public bool CollectBandage()
    {
        if (inventoryManager != null && !inventoryManager.HasAvailableSlot())
        {
            return false;
        }

        if (bandageCount >= maxBandages)
        {
            Debug.Log("Bandage inventory full!");
            return false;
        }

        bandageCount++;
        Debug.Log("Bandage collected! Total: " + bandageCount);
        inventoryManager.PickUpItem("Bandage");
        return true;
    }

    public bool UseTNT()
    {
        if (tntCount <= 0)
            return false;

        tntCount--;

        if (inventoryManager != null)
        {
            inventoryManager.UseTNT();
        }

        return true;
    }

    public bool UseBandage()
    {
        if (bandageCount <= 0)
        {
            Debug.Log("No bandages available!");
            return false;
        }

        if (playerHealth == null)
        {
            Debug.LogWarning("Player Health component not found!");
            return false;
        }

        if (playerHealth.curr_Health <= 0 ||
            playerHealth.curr_Health >= playerHealth.maxHealth)
        {
            Debug.Log("Cannot use bandage at full health or after death!");
            return false;
        }

        playerHealth.Heal(bandageHealAmount);
        bandageCount--;

        if (bandageCount <= 0)
        {
            inventoryManager.RemoveItem("Bandage");
        }

        Debug.Log("Bandage used! Remaining: " + bandageCount);

        return true;
    }
}
