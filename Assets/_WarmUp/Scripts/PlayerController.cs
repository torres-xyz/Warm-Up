using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public static event EventHandler EnemyTouched;
    public static event EventHandler<int> HealthChanged;
    public static event EventHandler<int> AttackDamageChanged;
    public static event EventHandler<int> MoneyChanged;

    [SerializeField] private float movementSpeed;
    [SerializeField] private int startingAttackDamage = 20;
    private NavMeshAgent navMeshAgent;

    private Camera mainCam;

    const string COIN_TAG = "Coin";
    const string CHEST_TAG = "Chest";
    const string ENEMY_TAG = "Enemy";
    const string CHALICE_TAG = "Chalice";
    const string TRAINING_STATUE_TAG = "TrainingStatue";

    private int maxHealth = 100;
    public int Health { get => health; private set => SetHealth(value); }
    public int AttackDamage { get => attackDamage; private set => SetAttackDamage(value); }
    public int Money { get => money; private set => SetMoney(value); }


    private int money;
    private int health;
    private int attackDamage;

    SphereCollider sphereCollider;

    void Start()
    {
        mainCam = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();
        sphereCollider = GetComponent<SphereCollider>();

        //Reset stats, so that the UI also gets updated
        Money = 0;
        Health = maxHealth;
        AttackDamage = startingAttackDamage;

        GameManager.TimeTicked += GameManager_OnTimeTicked;

        StoreItem.AttackDamageIncreasedByStoreItem += StoreItem_OnAttackDamageIncreasedByStoreItem;
    }



    private void GameManager_OnTimeTicked(object sender, EventArgs e)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + sphereCollider.center,
                                                        sphereCollider.radius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag(ENEMY_TAG))
            {
                Enemy touchedEnemy = hitCollider.gameObject.GetComponent<Enemy>();

                ReceiveDamage(touchedEnemy.AttackDamage);
                DealDamage(touchedEnemy, AttackDamage);
            }
            if (hitCollider.gameObject.CompareTag(CHEST_TAG))
            {
                OpenChest();
            }
            if (hitCollider.gameObject.CompareTag(CHALICE_TAG))
            {
                ReceiveHealing();
            }
            if (hitCollider.gameObject.CompareTag(TRAINING_STATUE_TAG))
            {
                if (Money >= GameManager.trainingCost)
                {
                    Money -= GameManager.trainingCost;
                    ReceiveTraining(); //Increase Attack Damage
                }
            }
        }
    }

    private void ReceiveTraining()
    {
        AttackDamage += GameManager.TrainingStatueReward;
    }

    private void ReceiveHealing()
    {
        Health = Math.Min(maxHealth, Health + GameManager.ChaliceReward);
    }

    private void OpenChest()
    {
        Money += GameManager.ChestReward;
    }

    private void DealDamage(Enemy touchedEnemy, int attackDamage)
    {
        touchedEnemy.TakeDamage(attackDamage);
    }

    private void ReceiveDamage(int attackDamage)
    {
        Health -= attackDamage;
        if (Health <= 0)
        {
            Health = 0;
            PlayerDied();
        }
    }

    private void PlayerDied()
    {
        Debug.Log($"Player Died");
    }

    public void SubtractMoney(int amount)
    {
        Money -= amount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetDestinationToMousePosition();
        }

        transform.LookAt(navMeshAgent.steeringTarget);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(COIN_TAG))
        {
            TouchedCoin(collider.gameObject);
        }
    }

    private void TouchedCoin(GameObject coin)
    {
        Money++;
        Destroy(coin); //Manage this better later
    }

    private void SetDestinationToMousePosition()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            navMeshAgent.SetDestination(hit.point);
        }
    }

    #region Setters
    private void SetHealth(int value)
    {
        health = value;
        HealthChanged?.Invoke(this, health);
    }
    private void SetAttackDamage(int value)
    {
        attackDamage = value;
        AttackDamageChanged?.Invoke(this, attackDamage);
    }
    private void SetMoney(int value)
    {
        money = value;
        MoneyChanged?.Invoke(this, money);
    }
    #endregion

    #region StoreItemListeners
    private void StoreItem_OnAttackDamageIncreasedByStoreItem(object sender, int e)
    {
        AttackDamage += e;
    }
    #endregion
}
