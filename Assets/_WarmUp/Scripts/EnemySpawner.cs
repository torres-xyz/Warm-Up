using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //TODO: Object Pooling
    //TODO: Randomize Enemy types or have them in a predictable list that we show the player.

    public static event EventHandler<Enemy> EnemySpawned;

    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private int ticksBetweenSpawns;
    [SerializeField] private Transform spawnLocation;
    private int ticksSinceLastSpawn;

    void Start()
    {
        GameManager.TimeTicked += GameManager_OnTimeTicked;
    }

    private void GameManager_OnTimeTicked(object sender, System.EventArgs e)
    {
        ticksSinceLastSpawn++;
        if (ticksSinceLastSpawn >= ticksBetweenSpawns)
        {
            SpawnEnemy();
            ticksSinceLastSpawn = 0;
        }
    }

    private void SpawnEnemy()
    {
        Enemy newEnemy = Instantiate(enemyPrefab);
        newEnemy.transform.position = spawnLocation.position;
        EnemySpawned?.Invoke(this, newEnemy);
    }
}
