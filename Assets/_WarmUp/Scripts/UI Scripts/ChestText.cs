using TMPro;
using UnityEngine;

public class ChestText : MonoBehaviour
{
    TMP_Text text;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        GameManager.ChestRewardChanged += GameManager_OnChestRewardChanged;
    }

    private void GameManager_OnChestRewardChanged(object sender, int chestReward)
    {
        text.text = chestReward.ToString();
    }

    private void OnDestroy()
    {
        GameManager.ChestRewardChanged -= GameManager_OnChestRewardChanged;
    }
}
