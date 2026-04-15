using UnityEngine;

/// <summary>
/// Logic for when NextDayButton is clicked
/// Authors:  , Tin Trinh
/// Date: Apr. 14, 2026
/// Source: 
/// </summary>
public class NextDayButton : MonoBehaviour
{
    [SerializeField]
    private StoreInventory storeInventory;
    private Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set player and store inventory.
        storeInventory = ObjectGetter.GetStoreInventory();
        player = ObjectGetter.GetPlayer();

    }

    public void OnNextDay()
    {
        foreach (PlantPotController pot in FindObjectsByType<PlantPotController>(FindObjectsSortMode.None))
        {
            PlantInstance data = pot.potData;

            if (!data.hasPlant) continue;

            PlantDefinition def = PlantDatabaseManager.Instance
                .GetPlantDefinition(data.plantDefinitionId);

            if (def == null) continue;

            int maxStage = def.growth.totalGrowthStages - 1;

            if (data.currentGrowthStage < maxStage)
            {
                data.currentGrowthStage++;
                pot.RefreshVisuals();
            }
        }

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