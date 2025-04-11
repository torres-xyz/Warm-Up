using UnityEngine;

public class EnemyHealthBarSpawner : MonoBehaviour
{
    [SerializeField] private EnemyFloatingHealthBar healthBarPrefab;
    [SerializeField] private GameObject healthBarsHolder;
    [SerializeField] private float healthBarHeight;

    // Start is called before the first frame update
    void Start()
    {
        EnemySpawner.EnemySpawned += EnemySpawner_OnEnemySpawned;
    }

    private void EnemySpawner_OnEnemySpawned(object sender, Enemy newEnemy)
    {
        EnemyFloatingHealthBar healthBar = Instantiate(healthBarPrefab);
        healthBar.transform.SetParent(healthBarsHolder.transform);
        healthBar.SetTarget(newEnemy, healthBarHeight);
    }

    private void OnDestroy()
    {
        EnemySpawner.EnemySpawned -= EnemySpawner_OnEnemySpawned;
    }
}
