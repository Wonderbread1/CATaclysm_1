
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
    public InventorySelector inventorySelector;

    //Drop Items
    public GameObject dynamitePrefab;
    public GameObject bandagePrefab;
    public Transform dropPoint;
    

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

        if (Input.GetKeyDown(KeyCode.R))
        {
            DropCurrentSelectedItem();
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
       if (inventoryManager != null)
        {
            inventoryManager.PickUpItem("Bandage");
            bandageCount++;
            Debug.Log("Bandage collected! Total: " + bandageCount);
            return true;
        }

        return false;
    }

    public bool UseTNT()
    {
        if (inventorySelector != null)
        {
            string currentItem = inventoryManager.GetSelectedItemType(inventorySelector.selectedIndex);
            if (currentItem != "Dynamite")
            {
                Debug.Log("Cannot use dynamite");
                return false;
            } 
        }

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
        if (inventorySelector != null)
        {
            string currentItem = inventoryManager.GetSelectedItemType(inventorySelector.selectedIndex);
            if (currentItem != "Bandage")
            {
                Debug.Log("Cannot use bandage");
                return false;
            }
        }

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

    void DropCurrentSelectedItem()
    {
        if (inventorySelector == null || inventoryManager == null) return;

        string selectedItem = inventoryManager.GetSelectedItemType(inventorySelector.selectedIndex);

        Vector3 drop_pos = transform.position + transform.forward * 1.5f;
        if (dropPoint != null)
        {
            drop_pos = dropPoint.position;
        }

        if (selectedItem == "Dynamite" && tntCount > 0)
        {
            tntCount--;
            if (dynamitePrefab != null)
            {
                GameObject droppedItem = Instantiate(dynamitePrefab, drop_pos, Quaternion.identity);

                ItemPopOut popOut = droppedItem.GetComponent<ItemPopOut>();
                if (popOut != null)
                {
                    popOut.enabled = false;
                }
            }

            if (tntCount <= 0)
            {
                inventoryManager.RemoveItem("Dynamite");
            }

            Debug.Log("Dropped dynamite");
        }
        else if (selectedItem == "Bandage" && bandageCount > 0)
        {
            bandageCount--;
            if (bandagePrefab != null)
            {
                GameObject droppedItem = Instantiate(bandagePrefab, drop_pos, Quaternion.identity);

                ItemPopOut popOut = droppedItem.GetComponent<ItemPopOut>();
                if (popOut != null)
                {
                    popOut.enabled = false;
                }
            }

            if(bandageCount <= 0)
            {
                inventoryManager.RemoveItem("Bandage");
            }

            Debug.Log("Dropped bandage");
        }
        else
        {
            Debug.Log("Nothing droppable");
        }
    }


}

