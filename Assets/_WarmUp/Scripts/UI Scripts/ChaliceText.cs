using TMPro;
using UnityEngine;

public class ChaliceText : MonoBehaviour
{
    TMP_Text text;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        GameManager.ChaliceRewardChanged += GameManager_OnChaliceRewardChanged;
    }

    private void GameManager_OnChaliceRewardChanged(object sender, int chaliceReward)
    {
        text.text = chaliceReward.ToString();
    }
    private void OnDestroy()
    {
        GameManager.ChaliceRewardChanged -= GameManager_OnChaliceRewardChanged;
    }
}
