using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Populates the store items.
/// Author: Tin Trinh
/// Date: Mar. 4, 2026
/// Revised: Mar.25, 2026
/// Source: Various discussions on https://discussions.unity.com
/// </summary>
public class PopulateStoreSell : MonoBehaviour
{
    public SlotSellStore slot;
    public Store store;
    public Item item;
    public Player player;
    public GameObject container;

    void Awake()
    {
        player = ObjectGetter.GetPlayer();
    }

    /// <summary>
    /// Populates the inventory items slots.
    /// </summary>
    void Populate(ItemCategory? itemCategory = null)
    {
        if (!player)
        {
            player = ObjectGetter.GetPlayer();
        }
        SlotSellStore obj;
        foreach (Item item in player.Inventory)
        {
            if (itemCategory != null && item.ItemCategory != itemCategory || item.ItemCategory == ItemCategory.EQUIPMENT)
            {
                continue;
            }
            obj = Instantiate(slot, transform);
            obj.info = item;
            obj.store = store;
            Image objImage = obj.transform.GetChild(1).GetComponent<Image>();
            TextMeshProUGUI objTitle = obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI objPrice = obj.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI objQuantity = obj.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
            objImage.sprite = obj.info.ItemSprite;
            objImage.preserveAspect = true;
            objTitle.text = obj.info.Name;
            objPrice.text = $"${obj.info.SellPrice}";
            objQuantity.text = $"x{obj.info.QuantityPlayer}";
        }
    }

    /// <summary>
    /// Updates the quantity of each item.
    /// </summary>
    public void Refresh(ItemCategory? itemCategory = null)
    {
        foreach (Transform child in container.transform)
        {
            Destroy(child.gameObject);
        }
        Populate(itemCategory);
    }
}
