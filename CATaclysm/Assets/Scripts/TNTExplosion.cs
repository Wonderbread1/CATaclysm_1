
using UnityEngine;
using System.Collections.Generic;

public class TNTExplosion : MonoBehaviour
{
    [Header("Explosion Settings")]
    public float explosionRadius = 2.5f;
    public float explosionDamage = 2f;

    public void Explode()
    {
        Health[] targets = FindObjectsByType<Health>(
            FindObjectsSortMode.None
        );

        HashSet<Health> damagedTargets = new HashSet<Health>();

        Vector2 explosionPosition = transform.position;

        foreach (Health health in targets)
        {
            if (health == null || !health.gameObject.activeInHierarchy)
                continue;

            // Use the target's actual world position.
            Vector2 targetPosition = health.transform.position;

            float distance = Vector2.Distance(
                explosionPosition,
                targetPosition
            );

            if (distance <= explosionRadius)
            {
                if (damagedTargets.Add(health))
                {
                    Debug.Log(
                        "TNT hit: " + health.gameObject.name +
                        " | Distance: " + distance
                    );

                    health.TakeDamage(explosionDamage);
                }
            }
        }

        Debug.Log("TNT explosion damage applied!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            transform.position,
            explosionRadius
        );
    }
}
