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
public class PopulateStore : MonoBehaviour
{
    public SlotStore slot;
    public Store store;
    public List<Item> items;
    public Item item;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        items = ObjectGetter.GetStoreInventory().Items;
        Populate();
    }

    /// <summary>
    /// Populates the Store items slots.
    /// </summary>
    void Populate(ItemCategory? itemCategory = null)
    {
        SlotStore obj;
        foreach (Item itemInfoWrapper in items)
        {
            if (itemCategory != null && itemInfoWrapper.ItemCategory != itemCategory)
            {
                continue;
            }
            obj = Instantiate(slot, transform);
            obj.info = itemInfoWrapper;
            obj.store = store;
            Image objImage = obj.transform.GetChild(0).GetComponent<Image>();
            TextMeshProUGUI objTitle = obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI objPrice = obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI objQuantity = obj.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            objImage.sprite = obj.info.ItemSprite;
            objImage.preserveAspect = true;
            objTitle.text = obj.info.Name;
            objPrice.text = $"${obj.info.Price}";
            objQuantity.text = $"x{obj.info.QuantityStore}";
        }
    }

    /// <summary>
    /// Updates the quantity of each item.
    /// </summary>
    public void UpdateQuantity()
    {
        foreach (Transform slot in transform)
        {
            TextMeshProUGUI objQuantity = slot.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            objQuantity.text = $"x{slot.GetComponent<SlotStore>().info.QuantityStore}";
        }
        Debug.Log("Refreshed");
    }
    /// <summary>
    /// Updates the items.
    /// </summary>
    public void Refresh(ItemCategory? itemCategory = null)
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
        Populate(itemCategory);
    }
}
