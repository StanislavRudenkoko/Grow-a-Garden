using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds/Populates the store inventory
/// Author: Tin Trinh, Stan
/// Date: Mar. 25, 2026
/// Revision: Apr. 14, 2026
/// Source: None
/// </summary>
public class StoreInventory : MonoBehaviour
{
    [SerializeField]
    private List<Item> items;
    public List<ItemInfo> itemsinfo;
    public List<Item> Items { get => items; set { items = value; } }
    public Item item;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Item newItem;
        items = new List<Item>();
        foreach (ItemInfo itemInfo in itemsinfo)
        {
            newItem = Instantiate(item, transform);
            newItem.ItemInfo = itemInfo;
            newItem.QuantityStore = itemInfo.StartingQuantity;
            items.Add(newItem);
        }
    }

    /// <summary>
    /// Removes an item from the store inventory.
    /// </summary>
    /// <param name="itemToRemove"></param>
    public void RemoveItem(Item itemToRemove)
    {
        Items.Remove(itemToRemove);
    }

    /// <summary>
    /// Returns an existing store row for this <see cref="ItemInfo"/>, or instantiates one (e.g. produce
    /// rows are often omitted from <see cref="itemsinfo"/> but are still referenced by seeds).
    /// </summary>
    public Item GetOrCreateItemForInfo(ItemInfo info)
    {
        if (info == null || item == null) return null;
        if (items == null)
            items = new List<Item>();

        foreach (Item existing in items)
        {
            if (existing != null && existing.ItemInfo == info)
                return existing;
        }

        Item newItem = Instantiate(item, transform);
        newItem.ItemInfo = info;
        newItem.QuantityStore = info.StartingQuantity;
        newItem.QuantityPlayer = 0;
        return newItem;
    }

}
