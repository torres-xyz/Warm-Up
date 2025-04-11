using TMPro;
using UnityEngine;

public class EnemyFrequencyWarning : MonoBehaviour
{
    const string WARNING_MESSAGE = "More enemies incoming!";
    [SerializeField] private float messageSpeed;
    [SerializeField] private float initialPosition;
    [SerializeField] private float outOfScreenThreshold;

    TMP_Text text;

    void Start()
    {
        EnemySpawner.SpawnFrequencyChanged += EnemySpawner_OnSpawnFrequencyChanged;
        text = GetComponent<TMP_Text>();
    }

    private void EnemySpawner_OnSpawnFrequencyChanged(object sender, int frequency)
    {
        text.text = WARNING_MESSAGE + $" (Ticks between enemies = {frequency})";
        text.rectTransform.position = new Vector3(
               text.rectTransform.position.x + text.rectTransform.rect.width,
               text.rectTransform.position.y,
               text.rectTransform.position.z);
    }

    void Update()
    {
        //hardcoding some values here, but for better performance I'd perhaps use a coroutine

        //if (text.rectTransform.position.y < outOfScreenThreshold)
        //{
        //    text.rectTransform.position = new Vector3(
        //        text.rectTransform.position.x - Time.deltaTime * messageSpeed,
        //        text.rectTransform.position.y,
        //        text.rectTransform.position.z);
        //}
    }
}
