using UnityEngine;

public class LeverPuzzleManager : MonoBehaviour
{
    public LeverSwitch[] levers;
    public DoorInteraction exitDoor;

    private bool puzzleCompleted = false;

    public void CheckLevers()
    {
        if (puzzleCompleted)
            return;

        foreach (LeverSwitch lever in levers)
        {
            if (!lever.isOn)
                return;
        }

        puzzleCompleted = true;

        // Automatically insert the door's key
        exitDoor.Interact(true);

        Debug.Log("All three levers ON! Exit door unlocked!");
    }
}