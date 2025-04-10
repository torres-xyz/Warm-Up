using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event EventHandler<int> CoinAmountChanged;

    private int coins;

    // Start is called before the first frame update
    void Start()
    {
        coins = 0;
        CoinAmountChanged?.Invoke(this, coins);


        PlayerController.CoinCaught += PlayerController_OnCoinCaught;
    }

    private void PlayerController_OnCoinCaught(object sender, EventArgs e)
    {
        coins++;
        CoinAmountChanged?.Invoke(this, coins);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
