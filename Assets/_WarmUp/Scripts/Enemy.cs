using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public static EventHandler EnemyKilled;
    public int AttackDamage { get => attackDamage; private set => attackDamage = value; }
    public int Life { get => life; private set => life = value; }

    [SerializeField] private int attackDamage;
    [SerializeField] private int life;

    private NavMeshAgent navMeshAgent;
    private PlayerController player;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>(); //TODO: Error handling on this.

    }

    private void Update()
    {
        navMeshAgent.SetDestination(player.transform.position);
        transform.LookAt(navMeshAgent.steeringTarget);
    }

    public void TakeDamage(int damageReceived)
    {
        Life -= damageReceived;
        if (Life <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        EnemyKilled?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}
