using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum StoreItemType
{
    IncreaseAttackDamage,
    IncreaseChaliceReward,
    IncreaseChestReward,
    IncreaseTrainingRewards
}

public class StoreItem : MonoBehaviour
{
    public static event EventHandler<int> AttackDamageIncreasedByStoreItem;
    public static event EventHandler<int> ChaliceRewardIncreasedByStoreItem;
    public static event EventHandler<int> ChestRewardIncreasedByStoreItem;
    public static event EventHandler<int> TrainingRewardIncreasedByStoreItem;

    public static event EventHandler StoreItemBoughtAndDestroyed;

    [SerializeField] private Sprite attackSprite;
    [SerializeField] private Color attackColor;
    [SerializeField] private Sprite healthSprite;
    [SerializeField] private Color healthColor;
    [SerializeField] private Sprite chestSprite;
    [SerializeField] private Color chestColor;
    [SerializeField] private Sprite trainingSprite;
    [SerializeField] private Color trainingColor;

    public int price;
    public int incrementAmount;
    public Image itemIconImg;
    public Image storeCardImg;
    public TMP_Text priceText;
    public TMP_Text itemDescriptionText;
    public Button button;

    StoreItemType itemType;

    private PlayerController player;

    public void SetupItem(int price, int incrementAmount, StoreItemType itemType)
    {
        this.price = price;
        this.incrementAmount = incrementAmount;
        this.itemType = itemType;

        player = FindObjectOfType<PlayerController>();

        SetSpriteAndColor(this.itemType);
        priceText.text = price.ToString();
        itemDescriptionText.text = $"+{incrementAmount}";

        SetButtonAction();
    }

    private void SetButtonAction()
    {
        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        //Check if player has money first
        if (player.Money < price) return;

        player.SubtractMoney(price);

        switch (itemType)
        {
            case StoreItemType.IncreaseAttackDamage:
                AttackDamageIncreasedByStoreItem?.Invoke(this, incrementAmount);
                break;
            case StoreItemType.IncreaseChaliceReward:
                ChaliceRewardIncreasedByStoreItem?.Invoke(this, incrementAmount);
                break;
            case StoreItemType.IncreaseChestReward:
                ChestRewardIncreasedByStoreItem?.Invoke(this, incrementAmount);
                break;
            case StoreItemType.IncreaseTrainingRewards:
                TrainingRewardIncreasedByStoreItem?.Invoke(this, incrementAmount);
                break;
        }


        StoreItemBoughtAndDestroyed?.Invoke(this, EventArgs.Empty);
        Destroy(this.gameObject);
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
            case StoreItemType.IncreaseChaliceReward:
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
