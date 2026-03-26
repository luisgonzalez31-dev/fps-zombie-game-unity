using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Max(health - damage, 0);

        Debug.Log("Player Health: " + health);

        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Died");
        Time.timeScale = 0f;
    }
}
