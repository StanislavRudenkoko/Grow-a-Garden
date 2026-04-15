using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds/Populates the store inventory
/// Author: Tin Trinh
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

    public void RemoveItem(Item itemToRemove)
    {
        Items.Remove(itemToRemove);
    }

}
