using UnityEngine;
using UnityEngine.UI;

public class EnemyFloatingHealthBar : MonoBehaviour
{
    private float height;
    private Slider slider;
    private RectTransform rectTransform;
    private Enemy enemyToFollow;


    public void SetTarget(Enemy enemy, float height)
    {
        slider = GetComponent<Slider>();
        rectTransform = GetComponent<RectTransform>();

        this.height = height;
        enemyToFollow = enemy;
        enemyToFollow.HealthChanged += Enemy_OnHealthChanged;
        enemyToFollow.PositionChanged += Enemy_OnPositionChanged;
        enemyToFollow.EnemyKilled += Enemy_OnEnemyKilled;

        slider.enabled = true;
    }

    private void Enemy_OnEnemyKilled(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }

    private void Enemy_OnPositionChanged(object sender, Vector3 e)
    {
        rectTransform.position = enemyToFollow.transform.position + Vector3.up * height;
    }

    private void Enemy_OnHealthChanged(object sender, int e)
    {
        slider.value = e / 100f; //TODO: Get the current max health.
    }

    private void OnDestroy()
    {
        enemyToFollow.HealthChanged -= Enemy_OnHealthChanged;
        enemyToFollow.PositionChanged -= Enemy_OnPositionChanged;
        enemyToFollow.EnemyKilled -= Enemy_OnEnemyKilled;
    }
}
