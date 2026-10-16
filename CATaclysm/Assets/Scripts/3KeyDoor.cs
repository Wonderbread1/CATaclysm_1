
using UnityEngine;

public class ThreeKeyDoor : MonoBehaviour
{
    [Header("Door References")]
    public Animator doorAnimator;
    public LevelTransition levelTransition;

    [Header("Door Progress")]
    [Range(0, 3)]
    public int insertedKeys = 0;

    private bool doorOpened = false;

    public bool CanInteract(int playerKeys)
    {
        if (doorOpened)
            return false;

        if (insertedKeys >= 3)
            return true;

        return playerKeys > 0;
    }

    public bool Interact(int playerKeys)
    {
        if (!CanInteract(playerKeys))
            return false;

        if (doorAnimator == null)
        {
            Debug.LogWarning("Three-key door Animator is missing!");
            return false;
        }

        if (insertedKeys < 3)
        {
            insertedKeys++;

            doorAnimator.SetTrigger("InsertKey" + insertedKeys);

            Debug.Log("Key inserted: " + insertedKeys + "/3");

            return true;
        }

        doorOpened = true;
        doorAnimator.SetTrigger("Open");

        if (levelTransition != null)
            levelTransition.BeginTransition();

        return false;
    }
}
