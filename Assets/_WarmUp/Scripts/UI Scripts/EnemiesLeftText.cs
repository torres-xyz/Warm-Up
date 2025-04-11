using TMPro;
using UnityEngine;

public class EnemiesLeftText : MonoBehaviour
{
    TMP_Text text;
    [SerializeField] private string message;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        GameManager.EnemiesLeftChanged += GameManager_OnEnemiesLeftChanged;
    }

    private void GameManager_OnEnemiesLeftChanged(object sender, int enemiesLeft)
    {
        text.text = message + enemiesLeft;
    }

    private void OnDestroy()
    {
        GameManager.EnemiesLeftChanged -= GameManager_OnEnemiesLeftChanged;
    }
}
