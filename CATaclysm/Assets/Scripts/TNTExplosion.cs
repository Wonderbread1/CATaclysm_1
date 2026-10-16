
using UnityEngine;
using System.Collections.Generic;

public class TNTExplosion : MonoBehaviour
{
    [Header("Explosion Settings")]
    public float explosionRadius = 2.5f;
    public float explosionDamage = 2f;

    public void Explode()
    {
        HashSet<Health> damagedTargets = new HashSet<Health>();

        // Find every Health component in the scene,
        // including Health on child objects like Barrel_Hitbox.
        Health[] targets = FindObjectsByType<Health>(
            FindObjectsSortMode.None
        );

        foreach (Health health in targets)
        {
            if (!health.gameObject.activeInHierarchy)
                continue;

            // Check every collider belonging to this Health object.
            Collider2D[] colliders =
                health.GetComponentsInChildren<Collider2D>();

            foreach (Collider2D col in colliders)
            {
                if (!col.enabled)
                    continue;

                Vector2 closestPoint = col.ClosestPoint(
                    transform.position
                );

                float distance = Vector2.Distance(
                    transform.position,
                    closestPoint
                );

                if (distance <= explosionRadius)
                {
                    if (damagedTargets.Add(health))
                    {
                        Debug.Log(
                            "TNT damaging: " + health.gameObject.name
                        );

                        health.TakeDamage(explosionDamage);
                    }

                    break;
                }
            }
        }

        Debug.Log("TNT exploded!");
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
