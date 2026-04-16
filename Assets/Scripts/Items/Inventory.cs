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

    public void BuyAllButton()
    {
        CategoryPress(0);
        inventoryHolder.GetComponent<PopulateInventory>().Refresh();
    }
    public void BuyEquipmentButton()
    {
        CategoryPress(1);
        inventoryHolder.GetComponent<PopulateInventory>().Refresh(ItemCategory.EQUIPMENT);
    }
    public void BuySoilButton()
    {
        CategoryPress(2);
        inventoryHolder.GetComponent<PopulateInventory>().Refresh(ItemCategory.SOIL);
    }
    public void BuyFertilizerButton()
    {
        CategoryPress(3);
        inventoryHolder.GetComponent<PopulateInventory>().Refresh(ItemCategory.FERTILIZER);
    }
    public void BuySeedsButton()
    {
        CategoryPress(4);
        inventoryHolder.GetComponent<PopulateInventory>().Refresh(ItemCategory.SEED);
    }
    public void BuyProduceButton()
    {
        CategoryPress(5);
        inventoryHolder.GetComponent<PopulateInventory>().Refresh(ItemCategory.PRODUCE);
    }

}
