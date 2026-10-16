
using UnityEngine;

public class BarrelDestruction : MonoBehaviour
{
    public GameObject barrelRoot;

    private void OnDestroy()
    {
        // Don't trigger barrel breaking when exiting Play Mode.
        if (!Application.isPlaying) return;

        if (barrelRoot == null) return;

        // Don't try to start animations on inactive barrels.
        if (!barrelRoot.activeInHierarchy) return;

        BarrelBreakHandler handler =
            barrelRoot.GetComponent<BarrelBreakHandler>();

        if (handler != null && handler.isActiveAndEnabled)
        {
            handler.BreakBarrel();
        }
    }
}
