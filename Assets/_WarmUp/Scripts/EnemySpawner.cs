using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static event EventHandler<int> SpawnFrequencyChanged;

    //TODO: Object Pooling
    //TODO: Randomize Enemy types or have them in a predictable list that we show the player.

    public static event EventHandler<Enemy> EnemySpawned;

    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private int amountOfSpawnsUntilFrequencyIncrease = 5;
    [SerializeField] private int initialSpawnFrequency = 8;
    [SerializeField] private Transform spawnLocation;
    private int ticksSinceLastSpawn;

    private int spawnsSinceLastFrequencyIncrease;

    public int SpawnFrequency { get => spawnFrequency; private set => SetSpawnFrequency(value); }
    private int spawnFrequency;

    void Start()
    {
        GameManager.TimeTicked += GameManager_OnTimeTicked;
        SpawnFrequency = initialSpawnFrequency;
    }

    private void GameManager_OnTimeTicked(object sender, System.EventArgs e)
    {
        ticksSinceLastSpawn++;
        if (ticksSinceLastSpawn >= SpawnFrequency)
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
        UpdateSpawnFrequency();
    }

    private void UpdateSpawnFrequency()
    {
        spawnsSinceLastFrequencyIncrease++;

        if (spawnsSinceLastFrequencyIncrease >= amountOfSpawnsUntilFrequencyIncrease)
        {
            spawnsSinceLastFrequencyIncrease = 0;
            SpawnFrequency--; //I know it's a bit counter-intuitive that the frequency value is going down here. I'll change it later
        }
    }

    private void SetSpawnFrequency(int value)
    {
        spawnFrequency = value;
        SpawnFrequencyChanged?.Invoke(this, value);
    }

}
