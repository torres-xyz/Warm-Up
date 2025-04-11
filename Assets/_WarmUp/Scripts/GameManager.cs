using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//TODO: Make this a singleton, and persistent through scenes
public class GameManager : MonoBehaviour
{
    public static event EventHandler<int> TrainingRewardChanged;
    public static event EventHandler<int> ChestRewardChanged;
    public static event EventHandler<int> ChaliceRewardChanged;
    public static event EventHandler<int> EnemiesLeftChanged;

    public static event EventHandler TimeTicked;

    public const float TICK_RATE = 0.5f; //in seconds
    [SerializeField] private int totalKillsObjective = 60; //how many enemies defeated needed to win
    [SerializeField] private int initialTrainingStatueReward = 1;
    [SerializeField] private int initialChestReward = 2;
    [SerializeField] private int initialChaliceReward = 5;
    [SerializeField] private GameObject victoryUiPanel;
    [SerializeField] private GameObject defeatUiPanel;
    [SerializeField] private Button victoryRestartButton;
    [SerializeField] private Button defeatRestartButton;


    public static int trainingCost = 2; //TODO: Make this upgradable as well


    public static int TrainingStatueReward { get => trainingStatueReward; private set => SetTrainingReward(value); }
    private static int trainingStatueReward;
    public static int ChestReward { get => chestReward; private set => SetChestReward(value); }
    private static int chestReward;
    public static int ChaliceReward { get => chaliceReward; private set => SetChaliceReward(value); }
    private static int chaliceReward;


    private float timeOfLastTick;
    private int enemyKillCount;

    private void Start()
    {
        StoreItem.ChestRewardIncreasedByStoreItem += StoreItem_OnChestRewardIncreasedByStoreItem;
        StoreItem.TrainingRewardIncreasedByStoreItem += StoreItem_OnTrainingRewardIncreasedByStoreItem;
        StoreItem.ChaliceRewardIncreasedByStoreItem += StoreItem_OnChaliceRewardIncreasedByStoreItem;

        PlayerController.PlayerDied += PlayerController_OnPlayerDied;

        Enemy.AnEnemyWasKilled += Enemy_OnAnEnemyWasKilled;

        TrainingStatueReward = initialTrainingStatueReward;
        ChestReward = initialChestReward;
        ChaliceReward = initialChaliceReward;
        EnemiesLeftChanged?.Invoke(this, totalKillsObjective);

        victoryRestartButton.onClick.AddListener(RestartGame);
        defeatRestartButton.onClick.AddListener(RestartGame);
    }

    private void Enemy_OnAnEnemyWasKilled(object sender, EventArgs e)
    {
        enemyKillCount++;
        EnemiesLeftChanged?.Invoke(this, Math.Max(0, totalKillsObjective - enemyKillCount));
        if (enemyKillCount >= totalKillsObjective)
        {
            GameWon();
        }
    }

    void Update()
    {
        if (timeOfLastTick + TICK_RATE < Time.time)
        {
            TimeTicked?.Invoke(this, EventArgs.Empty);
            timeOfLastTick = Time.time;
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void PlayerController_OnPlayerDied(object sender, EventArgs e)
    {
        defeatUiPanel.SetActive(true);
    }

    private void GameWon()
    {
        victoryUiPanel.SetActive(true);
    }

    private void StoreItem_OnChestRewardIncreasedByStoreItem(object sender, int e)
    {
        ChestReward += e;
    }
    private void StoreItem_OnTrainingRewardIncreasedByStoreItem(object sender, int e)
    {
        TrainingStatueReward += e;
    }
    private void StoreItem_OnChaliceRewardIncreasedByStoreItem(object sender, int e)
    {
        ChaliceReward += e;
    }

    private static void SetTrainingReward(int value)
    {
        trainingStatueReward = value;
        TrainingRewardChanged?.Invoke(null, TrainingStatueReward);
    }
    private static void SetChestReward(int value)
    {
        chestReward = value;
        ChestRewardChanged?.Invoke(null, ChestReward);
    }
    private static void SetChaliceReward(int value)
    {
        chaliceReward = value;
        ChaliceRewardChanged?.Invoke(null, ChaliceReward);
    }

    private void OnDestroy()
    {
        StoreItem.ChestRewardIncreasedByStoreItem -= StoreItem_OnChestRewardIncreasedByStoreItem;
        StoreItem.TrainingRewardIncreasedByStoreItem -= StoreItem_OnTrainingRewardIncreasedByStoreItem;
        StoreItem.ChaliceRewardIncreasedByStoreItem -= StoreItem_OnChaliceRewardIncreasedByStoreItem;

        PlayerController.PlayerDied -= PlayerController_OnPlayerDied;

        Enemy.AnEnemyWasKilled -= Enemy_OnAnEnemyWasKilled;

        victoryRestartButton.onClick.RemoveListener(RestartGame);
        defeatRestartButton.onClick.RemoveListener(RestartGame);
    }
}
