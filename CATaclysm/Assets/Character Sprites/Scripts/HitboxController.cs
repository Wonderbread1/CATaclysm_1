using UnityEngine;

public class HitboxController : MonoBehaviour
{

    public float damage_Amount = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health targetHealth = other.GetComponent<Health>();

        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage_Amount);
        }

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit detected!");
        }
    }
}
