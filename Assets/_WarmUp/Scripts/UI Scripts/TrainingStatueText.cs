using TMPro;
using UnityEngine;

public class TrainingStatueText : MonoBehaviour
{
    TMP_Text text;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        GameManager.TrainingRewardChanged += GameManager_OnTrainingRewardChanged;
    }

    private void GameManager_OnTrainingRewardChanged(object sender, int trainingReward)
    {
        text.text = trainingReward.ToString();
    }

    private void OnDestroy()
    {
        GameManager.TrainingRewardChanged -= GameManager_OnTrainingRewardChanged;
    }
}
