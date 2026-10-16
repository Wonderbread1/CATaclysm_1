
using UnityEngine;

public class LeverDoorMarker : MonoBehaviour
{
    public LeverPuzzleManager puzzleManager;

    public bool IsUnlocked
    {
        get
        {
            if (puzzleManager == null)
                return false;

            foreach (LeverSwitch lever in puzzleManager.levers)
            {
                if (lever == null || !lever.isOn)
                    return false;
            }

            return puzzleManager.levers.Length > 0;
        }
    }
}
