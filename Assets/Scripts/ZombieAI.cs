using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform player;
    public float attackDistance = 2.2f;
    public float damage = 10f;
    public float attackCooldown = 1f;
    public bool canAttack = true;

    private NavMeshAgent agent;
    private float lastAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (canAttack && distance <= attackDistance)
        {
            agent.isStopped = true; // detenerse al atacar
            Attack();
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
    }

    void Attack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // el zombie hipertrofia - empuja
            ZombieHipertrofia tank = GetComponent<ZombieHipertrofia>();

            if (tank != null)
            {
                tank.ApplyPush();
            }

            lastAttackTime = Time.time;
        }
    }
}
