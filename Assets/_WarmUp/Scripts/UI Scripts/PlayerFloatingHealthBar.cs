using UnityEngine;
using UnityEngine.UI;

public class PlayerFloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float height;

    private Slider slider;
    private RectTransform rectTransform;

    void Start()
    {
        slider = GetComponent<Slider>();
        rectTransform = GetComponent<RectTransform>();
        PlayerController.HealthChanged += PlayerController_OnLifeChanged;
    }

    private void PlayerController_OnLifeChanged(object sender, int e)
    {
        slider.value = e / 100f; //TODO: Get the current max health.
    }

    private void Update()
    {
        rectTransform.position = target.position + Vector3.up * height;
    }
}
