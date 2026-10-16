
using UnityEngine;

public class HitboxController : MonoBehaviour
{
    public float damage_Amount = 1f;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health targetHealth = other.GetComponent<Health>();

        if (targetHealth != null)
        {
            // Golems cannot be damaged by sword attacks.
            if (targetHealth.GetComponentInParent<GolemTNTOnly>() != null)
            {
                return;
            }

            targetHealth.TakeDamage(damage_Amount);
        }

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit detected!");
        }
    }
}
