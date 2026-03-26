using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Slot class that represents a single item slot in the store.
/// Author: Tin Trinh
/// Date: Mar. 4, 2026
/// Source: None
/// </summary>
public class SlotStore : Slot, IPointerEnterHandler, IPointerClickHandler
{
    public ConfirmationBox confirmationBox;
    public Store Store {get; set;}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Displays information when mouse is hovering over a slot.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        TextMeshProUGUI itemNameDisplay = GameObject.Find("ItemName").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI itemDescDisplay = GameObject.Find("ItemDesc").GetComponent<TextMeshProUGUI>();
        itemNameDisplay.text = info.Name;
        itemDescDisplay.text = info.Description;
    }
    
    /// <summary>
    /// Opens a confirmation box when an item is clicked.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        ConfirmationBox box = Instantiate(confirmationBox, Store.transform);
        box.Item = info;
        box.Store = Store;
        TextMeshProUGUI boxTitle = box.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI boxDesc = box.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        boxTitle.text = $"Buy {info.Name}?";
        boxDesc.text = $"{info.Description}";
    }
}
