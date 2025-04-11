using System;
using UnityEngine;


//TODO: Make this a singleton, and persistent through scenes
public class GameManager : MonoBehaviour
{
    public static event EventHandler<int> TrainingRewardChanged;
    public static event EventHandler<int> ChestRewardChanged;
    public static event EventHandler<int> ChaliceRewardChanged;

    public static event EventHandler TimeTicked;

    public const float TICK_RATE = 0.5f; //in seconds
    [SerializeField] private int initialTrainingStatueReward = 1;
    [SerializeField] private int initialChestReward = 2;
    [SerializeField] private int initialChaliceReward = 5;

    public static int trainingCost = 2;


    public static int TrainingStatueReward { get => trainingStatueReward; private set => SetTrainingReward(value); }
    private static int trainingStatueReward;
    public static int ChestReward { get => chestReward; private set => SetChestReward(value); }
    private static int chestReward;
    public static int ChaliceReward { get => chaliceReward; private set => SetChaliceReward(value); }
    private static int chaliceReward;


    private float timeOfLastTick;

    private void Start()
    {
        StoreItem.ChestRewardIncreasedByStoreItem += StoreItem_OnChestRewardIncreasedByStoreItem;
        StoreItem.TrainingRewardIncreasedByStoreItem += StoreItem_OnTrainingRewardIncreasedByStoreItem;
        StoreItem.ChaliceRewardIncreasedByStoreItem += StoreItem_OnChaliceRewardIncreasedByStoreItem;

        TrainingStatueReward = initialTrainingStatueReward;
        ChestReward = initialChestReward;
        ChaliceReward = initialChaliceReward;
    }


    void Update()
    {
        if (timeOfLastTick + TICK_RATE < Time.time)
        {
            TimeTicked?.Invoke(this, EventArgs.Empty);
            timeOfLastTick = Time.time;
        }
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

}
