using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public event EventHandler<int> HealthChanged;
    public event EventHandler<Vector3> PositionChanged;
    public event EventHandler WasKilled;

    public static event EventHandler AnEnemyWasKilled;


    public int AttackDamage { get => attackDamage; private set => attackDamage = value; }
    public int Health { get => health; private set => ChangeHealth(value); }
    private int health;

    [SerializeField] private int attackDamage;
    [SerializeField] private int speed;

    private NavMeshAgent navMeshAgent;
    private PlayerController player;
    private int maxHealth = 100;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>(); //TODO: Error handling on this.
        navMeshAgent.speed = speed;

        Health = maxHealth;
    }

    private void Update()
    {
        navMeshAgent.SetDestination(player.transform.position);
        transform.LookAt(navMeshAgent.steeringTarget);
        PositionChanged?.Invoke(this, transform.position);
    }

    public void TakeDamage(int damageReceived)
    {
        Health -= damageReceived;
        if (Health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        WasKilled?.Invoke(this, EventArgs.Empty);
        AnEnemyWasKilled?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }

    private void ChangeHealth(int value)
    {
        health = value;
        HealthChanged?.Invoke(this, health);
    }
}
