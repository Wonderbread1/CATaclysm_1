
using UnityEngine;

public class GolemKeyDrop : MonoBehaviour
{
    private Health golemHealth;
    private bool deathReported = false;

    private void Awake()
    {
        golemHealth = GetComponent<Health>();
    }

    private void OnDestroy()
    {
        if (deathReported)
            return;

        // Only count actual deaths, not scene unloading.
        if (golemHealth == null || golemHealth.curr_Health > 0f)
            return;

        deathReported = true;

        if (Level3KeyManager.Instance != null)
        {
            Level3KeyManager.Instance.GolemDefeated(
                transform.position
            );
        }
    }
}
