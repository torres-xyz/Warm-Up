using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum StoreItemType
{
    IncreaseAttackDamage,
    IncreaseHealth,
    IncreaseChestReward,
    IncreaseTrainingRewards
}

public class StoreItem : MonoBehaviour
{
    [SerializeField] private Sprite attackSprite;
    [SerializeField] private Color attackColor;
    [SerializeField] private Sprite healthSprite;
    [SerializeField] private Color healthColor;
    [SerializeField] private Sprite chestSprite;
    [SerializeField] private Color chestColor;
    [SerializeField] private Sprite trainingSprite;
    [SerializeField] private Color trainingColor;

    public int price;
    public Image itemIconImg;
    public Image storeCardImg;
    public TMP_Text priceText;
    public TMP_Text itemDescriptionText;
    public Button button;

    StoreItemType itemType;

    public void SetupItem(int price, int incrementAmount, StoreItemType itemType)
    {
        this.itemType = itemType;
        SetSpriteAndColor(this.itemType);
        this.price = price;
        priceText.text = price.ToString();

        itemDescriptionText.text = $"+{incrementAmount}";
    }

    private void SetSpriteAndColor(StoreItemType itemType)
    {
        //I could also use a dictionary here
        switch (itemType)
        {
            case StoreItemType.IncreaseAttackDamage:
                itemIconImg.sprite = attackSprite;
                storeCardImg.color = attackColor;
                break;
            case StoreItemType.IncreaseHealth:
                itemIconImg.sprite = healthSprite;
                storeCardImg.color = healthColor;
                break;
            case StoreItemType.IncreaseChestReward:
                itemIconImg.sprite = chestSprite;
                storeCardImg.color = chestColor;
                break;
            case StoreItemType.IncreaseTrainingRewards:
                itemIconImg.sprite = trainingSprite;
                storeCardImg.color = trainingColor;
                break;
        }
    }
}
