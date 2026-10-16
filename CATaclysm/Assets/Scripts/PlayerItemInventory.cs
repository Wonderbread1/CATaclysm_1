
using UnityEngine;

public class PlayerItemInventory : MonoBehaviour
{
    [Header("Consumable Inventory")]
    public int tntCount = 0;
    public int bandageCount = 0;

    // ADDED: Maximum carrying capacity
    public int maxTNT = 2;
    public int maxBandages = 3;

    // CHANGED: Return whether collection succeeded
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

    // CHANGED: Return whether collection succeeded
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
            return false;

        bandageCount--;
        return true;
    }
}
