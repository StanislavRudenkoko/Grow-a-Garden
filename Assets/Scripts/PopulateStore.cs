using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PopulateStore : MonoBehaviour
{
    public Slot slot;
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
        Slot obj;
        foreach (Item item in items)
        {
            obj = Instantiate(slot, transform);
            obj.info = item;
            Image objImage = obj.transform.GetChild(0).GetComponent<Image>();
            TextMeshProUGUI objTitle = obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI objPrice = obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI objQuantity = obj.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            objImage.sprite = obj.info.ItemSprite;
            objTitle.text = obj.info.Name;
            objPrice.text = $"${obj.info.Price}";
            Debug.Log(obj.info.Price);
            objQuantity.text = obj.info.Quantity.ToString();
        }
    }
}
