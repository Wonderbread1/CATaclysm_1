
using UnityEngine;

public class ObjectiveKeyPickup : MonoBehaviour
{
    [Header("Ending")]
    public TBCEnding ending;

    private bool endingTriggered = false;

    public void TriggerEnding()
    {
        if (endingTriggered)
            return;

        endingTriggered = true;

        if (ending == null)
            ending = FindFirstObjectByType<TBCEnding>();

        if (ending != null)
        {
            ending.StartEnding();
        }
        else
        {
            Debug.LogWarning("TBC Ending not found!");
        }
    }
}
