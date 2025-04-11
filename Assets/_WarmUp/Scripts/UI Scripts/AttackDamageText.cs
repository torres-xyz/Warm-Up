using TMPro;
using UnityEngine;

public class AttackDamageText : MonoBehaviour
{
    TMP_Text text;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        PlayerController.AttackDamageChanged += PlayerController_OnAttackDamageChanged;
    }

    private void PlayerController_OnAttackDamageChanged(object sender, int attackDamage)
    {
        text.text = attackDamage.ToString();
    }

    private void OnDestroy()
    {
        PlayerController.AttackDamageChanged -= PlayerController_OnAttackDamageChanged;

    }
}
