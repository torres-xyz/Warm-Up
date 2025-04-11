using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static EventHandler EnemyKilled;

    [SerializeField] private int attackDamage;
    [SerializeField] private int life;

    public int AttackDamage { get => attackDamage; private set => attackDamage = value; }
    public int Life { get => life; private set => life = value; }

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
