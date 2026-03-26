using UnityEngine;

public class ZombieDirector : MonoBehaviour
{
    public HordeSpawner spawner;
    public Transform player;

    public float calmDuration = 20f;
    public float buildupDuration = 15f;
    public float hordeDuration = 25f;
    public float cooldownDuration = 15f;

    private enum State { Calm, BuildUp, Horde, Cooldown }
    private State currentState;

    private float timer;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        SetState(State.Calm);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        // Si el jugador está muy herido calma 
        if (playerHealth != null && playerHealth.health < 30f && currentState != State.Calm)
        {
            SetState(State.Calm);
            return;
        }

        if (timer <= 0f)
        {
            switch (currentState)
            {
                case State.Calm:
                    SetState(State.BuildUp);
                    break;

                case State.BuildUp:
                    SetState(State.Horde);
                    break;

                case State.Horde:
                    SetState(State.Cooldown);
                    break;

                case State.Cooldown:
                    SetState(State.Calm);
                    break;
            }
        }
    }

    void SetState(State newState)
    {
        currentState = newState;

        switch (newState)
        {
            case State.Calm:
                timer = Random.Range(calmDuration - 5f, calmDuration + 5f);
                spawner.spawnDelay = 4f;
                spawner.maxAlive = 5;
                break;

            case State.BuildUp:
                timer = Random.Range(buildupDuration - 3f, buildupDuration + 3f);
                spawner.spawnDelay = 2f;
                spawner.maxAlive = 10;
                break;

            case State.Horde:
                timer = Random.Range(hordeDuration - 5f, hordeDuration + 5f);
                spawner.spawnDelay = 0.6f;
                spawner.maxAlive = 25;

                Debug.Log("HORDE INCOMING");
                break;

            case State.Cooldown:
                timer = Random.Range(cooldownDuration - 3f, cooldownDuration + 3f);
                spawner.spawnDelay = 3f;
                spawner.maxAlive = 8;
                break;
        }
    }

    // llamado por el Zombie Llamador
    public void TriggerMiniHorde(Vector3 position)
    {
        Debug.Log("MINI HORDE TRIGGERED");

        // Guardar valores actuales
        float originalDelay = spawner.spawnDelay;
        int originalMax = spawner.maxAlive;

        // Aumentar presión temporal
        spawner.spawnDelay = 0.3f;
        spawner.maxAlive += 10;

        // Restaurar después de unos segundos
        StartCoroutine(ResetMiniHorde(originalDelay, originalMax, 8f));
    }

    System.Collections.IEnumerator ResetMiniHorde(float delay, int max, float time)
    {
        yield return new WaitForSeconds(time);

        spawner.spawnDelay = delay;
        spawner.maxAlive = max;
    }
}
