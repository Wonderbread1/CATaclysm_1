
using UnityEngine;

public class PlayerKeyInventory : MonoBehaviour
{
    [Header("Level 1 Key")]
    public bool hasLevel1Key = false;

    [Header("Level 3 Keys")]
    public bool hasLevel3Key = false;

    [Header("Main Objective Key")]
    public bool hasObjectiveKey = false;

    private PlayerCurseController curseController;

    private void Awake()
    {
        hasObjectiveKey = GameProgress.hasObjectiveKey;
        curseController = GetComponent<PlayerCurseController>();
    }

    // MAIN OBJECTIVE KEY
    public void CollectObjectiveKey()
    {
        if (hasObjectiveKey)
            return;

        hasObjectiveKey = true;
        GameProgress.hasObjectiveKey = true;

        Debug.Log("MAIN OBJECTIVE KEY COLLECTED!");

        if (curseController != null)
        {
            curseController.SetInvertedCurse();
            Debug.Log("CURSE ACTIVATED! Controls inverted!");
        }
        else
        {
            Debug.LogWarning("PlayerCurseController not found on player!");
        }
    }

    // LEVEL 1 KEY
    public void CollectLevel1Key()
    {
        if (hasLevel1Key)
            return;

        hasLevel1Key = true;
        Debug.Log("Level 1 key collected!");
    }

    // LEVEL 3 KEYS
    public bool CollectLevel3Key()
    {
        if (hasLevel3Key)
        {
            Debug.Log("Already carrying a Level 3 key!");
            return false;
        }

        hasLevel3Key = true;
        Debug.Log("Level 3 key collected!");
        return true;
    }

    public bool UseLevel3Key()
    {
        if (!hasLevel3Key)
            return false;

        hasLevel3Key = false;
        Debug.Log("Level 3 key inserted into door!");
        return true;
    }
}
