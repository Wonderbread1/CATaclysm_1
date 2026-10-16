
using UnityEngine;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    public float maxHealth = 6f;
    public float curr_Health;

    public List<Hearts> hearts;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        curr_Health = maxHealth;
        UpdateHeartsUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        curr_Health -= damage;
        curr_Health = Mathf.Clamp(curr_Health, 0, maxHealth);

        Debug.Log(gameObject.name + " took " + damage + " damage. Current health: " + curr_Health);

        UpdateHeartsUI();

        if (curr_Health <= 0)
        {
            Die();
        }
    }

    // ADDED: Heal health and update the hearts UI
    public void Heal(float amount)
    {
        curr_Health += amount;
        curr_Health = Mathf.Clamp(curr_Health, 0, maxHealth);

        Debug.Log(gameObject.name + " healed " + amount + " HP. Current health: " + curr_Health);

        UpdateHeartsUI();
    }

    void UpdateHeartsUI()
    {
        if (hearts == null || hearts.Count == 0) return;

        for (int i = 0; i < hearts.Count; i++)
        {
            float heart_Threshold = i * 2f;

            if (curr_Health >= heart_Threshold + 2f)
            {
                hearts[i].SetHeartState(2);
            }
            else if (curr_Health >= heart_Threshold + 1f)
            {
                hearts[i].SetHeartState(1);
            }
            else
            {
                hearts[i].SetHeartState(0);
            }
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " has died. ");
        Destroy(gameObject);
    }
}
