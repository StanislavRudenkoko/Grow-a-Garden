using UnityEngine;
/// <summary>
/// Logic for when NextDayButton is clicked
/// Authors: Stanislav Rudenko, Tin Trinh
/// Date: Apr. 14, 2026
/// Source: 
/// </summary>
public class NextDayButton : MonoBehaviour
{
    [SerializeField]
    private StoreInventory storeInventory;
    private Player player;

    void Start()
    {
        storeInventory = ObjectGetter.GetStoreInventory();
        player = ObjectGetter.GetPlayer();
    }

    public void OnNextDay()
    {
        PlantManager.Instance.ProcessNextDay();
        StockInventory();
        IncrementDay();
    }

    /// <summary>
    /// Stocks inventory
    /// </summary>
    private void StockInventory()
    {
        foreach (Item item in storeInventory.Items)
        {
            if (item.ItemCategory == ItemCategory.EQUIPMENT)
            {
                continue;
            }
            if (item.QuantityStore < 3)
            {
                item.QuantityStore += 5;
            }
        }
    }

    /// <summary>
    /// Increments day.
    /// </summary>
    private void IncrementDay()
    {
        player.DayCount += 1;
    }
}