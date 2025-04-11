using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        PlayerController.MoneyChanged += PlayerController_OnMoneyChanged; ;
    }

    private void PlayerController_OnMoneyChanged(object sender, int coinAmount)
    {
        text.text = coinAmount.ToString();
    }
}
