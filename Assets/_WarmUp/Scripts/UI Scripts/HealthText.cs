using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        PlayerController.HealthChanged += PlayerController_OnLifeChanged;
    }

    private void PlayerController_OnLifeChanged(object sender, int life)
    {
        text.text = $"{life}%";
    }

    private void GameManager_OnCoinAmountChanged(object sender, int coinAmount)
    {
        text.text = coinAmount.ToString();
    }
}
