using System;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    //Spawn in random items. If they're not good the player
    //can let them go. Otherwise it creates a sense of 
    //urgency to get the item.


    public const int MAX_STORE_ITEMS = 9;
    [SerializeField] private StoreItem storeItemPrefab;
    [SerializeField] private int ticksBetweenNewItems;
    [SerializeField] private GameObject itemHolder;

    private int itemsInStoreCount;
    private int ticksSinceLastItem;


    void Start()
    {
        itemsInStoreCount = 0;
        GameManager.TimeTicked += GameManager_OnTimeTicked;
    }

    private void GameManager_OnTimeTicked(object sender, System.EventArgs e)
    {
        ticksSinceLastItem++;
        if (ticksSinceLastItem >= ticksBetweenNewItems &&
            itemsInStoreCount < MAX_STORE_ITEMS)
        {
            SpawnItem();
            ticksSinceLastItem = 0;
        }
    }

    private void SpawnItem()
    {
        StoreItem newStoreItem = Instantiate(storeItemPrefab, itemHolder.transform);
        itemsInStoreCount++;

        //TODO - Replace magic numbers
        int price = UnityEngine.Random.Range(5, 21); //5 - 20
        int increment = UnityEngine.Random.Range(1, 5); //1 - 4
        int itemType = UnityEngine.Random.Range(0, Enum.GetNames(typeof(StoreItemType)).Length);

        newStoreItem.SetupItem(price, increment, (StoreItemType)itemType);
    }
}
