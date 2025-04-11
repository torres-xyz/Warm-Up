using System;
using UnityEngine;


//TODO: Make this a singleton, and persistent through scenes
public class GameManager : MonoBehaviour
{
    public static event EventHandler<int> CoinAmountChanged;

    public static event EventHandler TimeTicked;

    public const float TICK_RATE = 0.5f; //in seconds
    public static int chestCoinGiveAmount = 2;
    public static int chaliceHealingAmount = 5;


    private float timeOfLastTick;

    private int coins;
    public int Coins { get => coins; private set => SetCoins(value); }

    private void SetCoins(int value)
    {
        coins = value;
        CoinAmountChanged?.Invoke(this, Coins);
    }

    // Start is called before the first frame update
    void Start()
    {
        Coins = 0;

        PlayerController.CoinCaught += PlayerController_OnCoinCaught;
        PlayerController.ChestOpened += PlayerController_OnChestOpened;
    }

    private void PlayerController_OnChestOpened(object sender, EventArgs e)
    {
        Coins += chestCoinGiveAmount;
    }

    private void PlayerController_OnCoinCaught(object sender, EventArgs e)
    {
        Coins++;
    }

    void Update()
    {
        if (timeOfLastTick + TICK_RATE < Time.time)
        {
            TimeTicked?.Invoke(this, EventArgs.Empty);
            timeOfLastTick = Time.time;
        }
    }
}
