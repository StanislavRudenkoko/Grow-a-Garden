using System.Collections.Generic;
using UnityEngine;

public class StoreInventory : MonoBehaviour
{
    [SerializeField]
    private List<Item> items;
    public List<ItemInfo> itemsinfo;
    public List<Item> Items {get => items; set { items = value;}}
    public Item item;

    void Start()
    {
        Item newItem;
        items = new List<Item>();
        foreach (ItemInfo itemInfo in itemsinfo)
        {
            Debug.Log(itemInfo.Name);
            newItem = Instantiate(item, transform);
            newItem.ItemInfo = itemInfo;
            newItem.QuantityStore = itemInfo.StartingQuantity;
            items.Add(newItem);
        }
        
    }

}
