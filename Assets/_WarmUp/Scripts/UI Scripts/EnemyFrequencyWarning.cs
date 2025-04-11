using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFrequencyWarning : MonoBehaviour
{
    const string WARNING_MESSAGE = "More enemies incoming!";
    [SerializeField] private float messageSpeed;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image panelImage;

    private float outOfScreenThreshold;


    void Awake()
    {
        EnemySpawner.SpawnFrequencyChanged += EnemySpawner_OnSpawnFrequencyChanged;

        outOfScreenThreshold = -text.rectTransform.rect.width;
        panelImage.enabled = false;
    }

    private void EnemySpawner_OnSpawnFrequencyChanged(object sender, int frequency)
    {
        panelImage.enabled = true;
        text.text = WARNING_MESSAGE + $" (TBE = {frequency})"; //For debug purposes
        text.rectTransform.localPosition = new Vector3(
               text.rectTransform.rect.width, //places it just outside the right side of the screen
               text.rectTransform.localPosition.y,
               text.rectTransform.localPosition.z);
    }

    void Update()
    {
        if (text.rectTransform.localPosition.x > outOfScreenThreshold)
        {
            text.rectTransform.localPosition = new Vector3(
                text.rectTransform.localPosition.x - Time.deltaTime * messageSpeed,
                text.rectTransform.localPosition.y,
                text.rectTransform.localPosition.z);
        }
        else
        {
            panelImage.enabled = false;
        }
    }

    private void OnDestroy()
    {
        EnemySpawner.SpawnFrequencyChanged -= EnemySpawner_OnSpawnFrequencyChanged;
    }
}
