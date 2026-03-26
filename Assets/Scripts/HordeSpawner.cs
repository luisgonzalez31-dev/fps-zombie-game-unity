using UnityEngine;

public class HordeSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject normalZombie;
    public GameObject hipertrofiaZombie;
    public GameObject llamadorZombie;
    public GameObject mutadorZombie;
    public GameObject nieblaZombie;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    [Header("Spawn Settings")]
    public int totalZombies = 120;
    public int maxAlive = 20;
    public float spawnDelay = 1.5f;

    private int zombiesSpawned = 0;
    private int zombiesAlive = 0;

    [Header("Player")]
    public Transform player;
    public Transform playerCamera;
    public float minSpawnDistance = 15f;

    private float timer;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnZombie();
            timer = spawnDelay;
        }
    }

    void SpawnZombie()
    {
        if (zombiesSpawned >= totalZombies) return;
        if (zombiesAlive >= maxAlive) return;

        // intentamos varias veces encontrar un buen punto
        for (int i = 0; i < 10; i++)
        {
            Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];

            float distance = Vector3.Distance(player.position, point.position);

            if (distance < minSpawnDistance)
                continue;

            //evitar spawn frente al jugador
            if (IsInPlayerView(point.position))
                continue;

            GameObject prefab = GetRandomZombie();

            Instantiate(prefab, point.position, Quaternion.identity);

            zombiesSpawned++;
            zombiesAlive++;

            return;
        }
    }

    //DETECCIÓN DE CAMPO DE VISIÓN
    bool IsInPlayerView(Vector3 spawnPosition)
    {
        if (playerCamera == null) return false;

        Vector3 dirToSpawn = (spawnPosition - playerCamera.position).normalized;

        float dot = Vector3.Dot(playerCamera.forward, dirToSpawn);

        // 0.5 = visión moderada
        return dot > 0.5f;
    }

    // TIPOS DE ZOMBIES
    GameObject GetRandomZombie()
    {
        float roll = Random.value;

        if (roll < 0.70f) return normalZombie;
        if (roll < 0.82f) return mutadorZombie;
        if (roll < 0.90f) return nieblaZombie;
        if (roll < 0.97f) return hipertrofiaZombie;

        return llamadorZombie;
    }

    public void ZombieDied()
    {
        zombiesAlive--;
    }
}
