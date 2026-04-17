using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Inventory class.
/// Author: Tin Trinh
/// Date: Mar. 25, 2026
/// Source: None
/// </summary>
public class Inventory : MonoBehaviour
{
    public Player player;
    public GameObject categories;
    public GameObject inventoryHolder;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = ObjectGetter.GetInstance.player;
        TextMeshProUGUI coins = this.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        coins.text = $"Coins: ${player.Coins}";
    }

    /// <summary>
    /// Disables all buttons in a group and enables the specified one.
    /// </summary>
    /// <param name="categories"></param>
    /// <param name="index"></param>
    private void CategoryPress(int index)
    {
        var children = categories.transform.GetComponentsInChildren<Button>();
        foreach (Button item in children)
        {
            item.interactable = true;
        }
        children[index].interactable = false;
    }

    /// <summary>
    /// Changes the category of the inventory to show all.
    /// </summary>
    public void InventoryAllButton()
    {
        CategoryPress(0);
        inventoryHolder.GetComponent<PopulateInventory>().Refresh();
    }

    /// <summary>
    /// Changes the category of the inventory to equipment.
    /// </summary>
    public void InventoryEquipmentButton()
    {
        CategoryPress(1);
        inventoryHolder.GetComponent<PopulateInventory>().Refresh(ItemCategory.EQUIPMENT);
    }
    /// <summary>
    /// Changes the category of the inventory to soil.
    /// </summary>
    public void InventorySoilButton()
    {
        CategoryPress(2);
        inventoryHolder.GetComponent<PopulateInventory>().Refresh(ItemCategory.SOIL);
    }
    /// <summary>
    /// Changes the category of the inventory to fertilizer.
    /// </summary>

    public void InventoryFertilizerButton()
    {
        CategoryPress(3);
        inventoryHolder.GetComponent<PopulateInventory>().Refresh(ItemCategory.FERTILIZER);
    }
    /// <summary>
    /// Changes the category of the inventory to seeds.
    /// </summary>

    public void InventorySeedsButton()
    {
        CategoryPress(4);
        inventoryHolder.GetComponent<PopulateInventory>().Refresh(ItemCategory.SEED);
    }
    /// <summary>
    /// Changes the category of the inventory to produce.
    /// </summary>

    public void InventoryProduceButton()
    {
        CategoryPress(5);
        inventoryHolder.GetComponent<PopulateInventory>().Refresh(ItemCategory.PRODUCE);
    }

}
