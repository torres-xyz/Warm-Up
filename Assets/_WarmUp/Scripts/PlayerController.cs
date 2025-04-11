using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public static event EventHandler CoinCaught;
    public static event EventHandler ChestOpened;
    public static event EventHandler EnemyTouched;
    public static event EventHandler<int> HealthChanged;

    [SerializeField] private float movementSpeed;
    private NavMeshAgent navMeshAgent;

    private Camera mainCam;

    const string COIN_TAG = "Coin";
    const string CHEST_TAG = "Chest";
    const string ENEMY_TAG = "Enemy";
    const string CHALICE_TAG = "Chalice";

    private int MaxPlayerHealth = 100;
    private int playerHealth;

    int attackDamage;

    float timeSinceLastTick;
    bool justTicked;
    SphereCollider sphereCollider;

    void Start()
    {
        mainCam = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();
        sphereCollider = GetComponent<SphereCollider>();

        playerHealth = 100;

        GameManager.TimeTicked += GameManager_OnTimeTicked;
    }

    private void GameManager_OnTimeTicked(object sender, EventArgs e)
    {
        justTicked = true;
        Debug.Log($"Time Ticked");

        Collider[] hitColliders = Physics.OverlapSphere(transform.position + sphereCollider.center,
                                                        sphereCollider.radius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag(ENEMY_TAG))
            {
                Enemy touchedEnemy = hitCollider.gameObject.GetComponent<Enemy>();

                ReceiveDamage(touchedEnemy.AttackDamage);
                DealDamage(touchedEnemy, attackDamage);
            }
            if (hitCollider.gameObject.CompareTag(CHEST_TAG))
            {
                OpenChest();
            }
            if (hitCollider.gameObject.CompareTag(CHALICE_TAG))
            {
                ReceiveHealing();
            }



            Debug.Log($"Collider hit = {hitCollider.gameObject.name}");
        }
    }

    private void ReceiveHealing()
    {
        playerHealth = Math.Min(MaxPlayerHealth, playerHealth + GameManager.chaliceHealingAmount);
        HealthChanged?.Invoke(this, playerHealth);
    }

    private void OpenChest()
    {
        ChestOpened?.Invoke(this, EventArgs.Empty);
    }

    private void DealDamage(Enemy touchedEnemy, int attackDamage)
    {
        touchedEnemy.TakeDamage(attackDamage);
    }

    private void ReceiveDamage(int attackDamage)
    {
        playerHealth -= attackDamage;
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            PlayerDied();
        }
        HealthChanged?.Invoke(this, playerHealth);
    }

    private void PlayerDied()
    {
        Debug.Log($"Player Died");
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
        CoinCaught?.Invoke(this, EventArgs.Empty);
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
}
