
using UnityEngine;

public class GolemActivation : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 3.5f;

    private Transform player;
    private GolemAnimations golemAI;
    private bool isAwake = false;
    private bool wasPlayerInside = false;

    public bool IsAwake => isAwake;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        golemAI = GetComponent<GolemAnimations>();

        if (golemAI != null)
        {
            golemAI.enabled = false;
        }
    }

    private void Update()
    {
        if (isAwake || player == null) return;

        bool playerInside =
            Vector2.Distance(transform.position, player.position)
            <= detectionRadius;

        // Only attempt activation when the player enters the circle.
        if (playerInside && !wasPlayerInside)
        {
            if (GolemManager.Instance != null &&
                GolemManager.Instance.TryActivateGolem())
            {
                isAwake = true;

                if (golemAI != null)
                {
                    golemAI.enabled = true;
                }
            }
        }

        wasPlayerInside = playerInside;
    }

    private void OnDestroy()
    {
        // Only free a slot if this golem was actually awake.
        if (isAwake && GolemManager.Instance != null)
        {
            GolemManager.Instance.GolemDied();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
