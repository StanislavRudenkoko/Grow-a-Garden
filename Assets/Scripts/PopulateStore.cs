using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateStore : MonoBehaviour
{
    public ItemSlot slot;
    public List<Item> items;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Populate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Populate()
    {
        ItemSlot obj;
        foreach (Item item in items)
        {
            obj = Instantiate(slot, transform);
            obj.GetComponent<Image>().sprite = item.itemSprite;
            
        }
    }
}
