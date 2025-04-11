using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        GameManager.CoinAmountChanged += GameManager_OnCoinAmountChanged;
    }

    private void GameManager_OnCoinAmountChanged(object sender, int coinAmount)
    {
        text.text = coinAmount.ToString();
    }
}
