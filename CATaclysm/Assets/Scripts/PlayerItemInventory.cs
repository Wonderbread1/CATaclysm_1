
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
        if (tntCount >= maxTNT)
        {
            Debug.Log("TNT inventory full!");
            return false;
        }

        tntCount++;
        Debug.Log("TNT collected! Total: " + tntCount);
        return true;
    }

    public bool CollectBandage()
    {
        if (bandageCount >= maxBandages)
        {
            Debug.Log("Bandage inventory full!");
            return false;
        }

        bandageCount++;
        Debug.Log("Bandage collected! Total: " + bandageCount);
        return true;
    }

    public bool UseTNT()
    {
        if (tntCount <= 0)
            return false;

        tntCount--;

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

        Debug.Log("Bandage used! Remaining: " + bandageCount);

        return true;
    }
}
