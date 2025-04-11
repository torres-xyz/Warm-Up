using TMPro;
using UnityEngine;

public class AttackDamageText : MonoBehaviour
{
    TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        PlayerController.AttackDamageChanged += PlayerController_OnAttackDamageChanged;
    }

    private void PlayerController_OnAttackDamageChanged(object sender, int attackDamage)
    {
        text.text = attackDamage.ToString();
    }
}
