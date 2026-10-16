
using UnityEngine;

public class ReturnDoor : MonoBehaviour
{
    [Header("Return Door Settings")]
    public LevelTransition levelTransition;

    [Header("Interaction Settings")]
    public float interactionRange = 1.5f;

    [Header("Requirements")]
    public bool requiresObjectiveKey = true;

    public bool IsPlayerInRange(Transform player)
    {
        if (player == null)
            return false;

        float distance = Vector2.Distance(
            player.position,
            transform.position
        );

        return distance <= interactionRange;
    }

    public bool CanInteract(PlayerKeyInventory inventory)
    {
        if (inventory == null)
            return false;

        if (requiresObjectiveKey && !inventory.hasObjectiveKey)
            return false;

        return levelTransition != null;
    }

    public void Interact(PlayerKeyInventory inventory)
    {
        if (!CanInteract(inventory))
            return;

        levelTransition.BeginTransition();
    }
}
