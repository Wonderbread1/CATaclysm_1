
using UnityEngine;

public class PickupPromptDetector : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionRange = 0.5f;

    [Header("E Key Prompt")]
    public GameObject interactionPrompt;

    private PlayerKeyInventory keyInventory;

    private DoorInteraction nearbyDoor;
    private ThreeKeyDoor nearbyThreeKeyDoor;
    private ReturnDoor nearbyReturnDoor;

    private GameObject nearbyKey;
    private ObjectiveKeyPickup nearbyObjectiveKey;

    private void Start()
    {
        keyInventory = GetComponent<PlayerKeyInventory>();
    }

    private void Update()
    {
        bool itemNearby = false;

        nearbyDoor = null;
        nearbyThreeKeyDoor = null;
        nearbyReturnDoor = null;
        nearbyKey = null;
        nearbyObjectiveKey = null;

        float closestKeyDistance = interactionRange;

        // Detect Level 1 keys, Level 3 keys, and other items.
        ItemPopOut[] items = FindObjectsByType<ItemPopOut>(
            FindObjectsSortMode.None
        );

        foreach (ItemPopOut item in items)
        {
            float distance = Vector2.Distance(
                transform.position,
                item.transform.position
            );

            if (distance > interactionRange)
                continue;

            bool isLevel1Key = item.CompareTag("Level1Key");
            bool isLevel3Key = item.CompareTag("Level3Key");

            if (isLevel1Key || isLevel3Key)
            {
                if (isLevel3Key &&
                    keyInventory != null &&
                    keyInventory.hasLevel3Key)
                {
                    continue;
                }

                if (distance <= closestKeyDistance)
                {
                    closestKeyDistance = distance;
                    nearbyKey = item.gameObject;
                }
            }
            else
            {
                // Preserve Bandage/TNT proximity prompts.
                itemNearby = true;
            }
        }

        // Detect Level 1 and Level 2 doors.
        DoorInteraction[] doors =
            FindObjectsByType<DoorInteraction>(
                FindObjectsSortMode.None
            );

        float closestDoorDistance = interactionRange;

        foreach (DoorInteraction door in doors)
        {
            float distance = Vector2.Distance(
                transform.position,
                door.transform.position
            );

            if (distance <= closestDoorDistance)
            {
                closestDoorDistance = distance;
                nearbyDoor = door;
            }
        }

        // Detect Level 3's three-key door.
        ThreeKeyDoor[] threeKeyDoors =
            FindObjectsByType<ThreeKeyDoor>(
                FindObjectsSortMode.None
            );

        float closestThreeKeyDoorDistance = interactionRange;

        foreach (ThreeKeyDoor door in threeKeyDoors)
        {
            float distance = Vector2.Distance(
                transform.position,
                door.transform.position
            );

            if (distance <= closestThreeKeyDoorDistance)
            {
                closestThreeKeyDoorDistance = distance;
                nearbyThreeKeyDoor = door;
            }
        }

        // Detect return doors using their own interaction range.
        ReturnDoor[] returnDoors =
            FindObjectsByType<ReturnDoor>(
                FindObjectsSortMode.None
            );

        foreach (ReturnDoor door in returnDoors)
        {
            if (door.IsPlayerInRange(transform) &&
                door.CanInteract(keyInventory))
            {
                nearbyReturnDoor = door;
                break;
            }
        }

        // Detect the main objective key on its podium.
        ObjectiveKeyPickup[] objectiveKeys =
            FindObjectsByType<ObjectiveKeyPickup>(
                FindObjectsSortMode.None
            );

        float closestObjectiveDistance = interactionRange;

        foreach (ObjectiveKeyPickup key in objectiveKeys)
        {
            float distance = Vector2.Distance(
                transform.position,
                key.transform.position
            );

            if (distance <= closestObjectiveDistance &&
                keyInventory != null &&
                !keyInventory.hasObjectiveKey)
            {
                closestObjectiveDistance = distance;
                nearbyObjectiveKey = key;
            }
        }

        // Check Level 1 key inventory.
        bool hasLevel1Key =
            keyInventory != null &&
            keyInventory.hasLevel1Key;

        // Check Level 1 and Level 2 door interactions.
        bool canUseRegularDoor = false;

        if (nearbyDoor != null)
        {
            LeverDoorMarker leverMarker =
                nearbyDoor.GetComponent<LeverDoorMarker>();

            if (leverMarker != null)
            {
                canUseRegularDoor = leverMarker.IsUnlocked;
            }
            else
            {
                canUseRegularDoor = hasLevel1Key;
            }
        }

        // Check Level 3 door interactions.
        bool canUseThreeKeyDoor =
            nearbyThreeKeyDoor != null &&
            keyInventory != null &&
            nearbyThreeKeyDoor.CanInteract(
                keyInventory.hasLevel3Key ? 1 : 0
            );

        // Check return doorway interactions.
        bool canUseReturnDoor =
            nearbyReturnDoor != null &&
            nearbyReturnDoor.CanInteract(keyInventory);

        // Display E when an interaction is available.
        bool showPrompt =
            itemNearby ||
            nearbyKey != null ||
            nearbyObjectiveKey != null ||
            canUseRegularDoor ||
            canUseThreeKeyDoor ||
            canUseReturnDoor;

        if (interactionPrompt != null &&
            interactionPrompt.activeSelf != showPrompt)
        {
            interactionPrompt.SetActive(showPrompt);
        }

        // Wait for E to be pressed.
        if (!Input.GetKeyDown(KeyCode.E))
            return;

        // Collect Level 1 or Level 3 keys.
        if (nearbyKey != null && keyInventory != null)
        {
            if (nearbyKey.CompareTag("Level1Key"))
            {
                keyInventory.CollectLevel1Key();
                Destroy(nearbyKey);
            }
            else if (nearbyKey.CompareTag("Level3Key"))
            {
                if (keyInventory.CollectLevel3Key())
                {
                    Destroy(nearbyKey);
                }
            }
        }

        // Collect the main objective key.
        else if (nearbyObjectiveKey != null &&
                 keyInventory != null)
        {
            keyInventory.CollectObjectiveKey();

            Debug.Log("Main objective key picked up!");

            Destroy(nearbyObjectiveKey.gameObject);
        }

        // Insert a key or open the Level 3 door.
        else if (canUseThreeKeyDoor)
        {
            bool consumedKey = nearbyThreeKeyDoor.Interact(
                keyInventory.hasLevel3Key ? 1 : 0
            );

            if (consumedKey)
            {
                keyInventory.UseLevel3Key();
            }
        }

        // Level 1 and Level 2 door interactions.
        else if (canUseRegularDoor)
        {
            nearbyDoor.Interact(true);
        }

        // Return to the previous level.
        else if (canUseReturnDoor)
        {
            nearbyReturnDoor.Interact(keyInventory);
        }
    }
}
