using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        ZombieLlamador llamador = GetComponent<ZombieLlamador>();

        if (llamador != null)
        {
            llamador.OnDamaged();
        }

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        // Dar dinero al jugador
        if (GameManager.instance != null)
        {
            GameManager.instance.AddMoney(10);
        }

        // Contar zombie eliminado para la misión
        if (MissionManager.instance != null)
        {
            MissionManager.instance.ZombieKilled();
        }

        // Avisar al spawner que murió un zombie
        HordeSpawner spawner = FindFirstObjectByType<HordeSpawner>();

        if (spawner != null)
        {
            spawner.ZombieDied();
        }

        Destroy(gameObject);
    }
}
