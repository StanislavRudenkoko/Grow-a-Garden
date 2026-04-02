using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Slot class that represents a single item slot in the store.
/// Author: Tin Trinh
/// Date: Mar. 4, 2026
/// Source: None
/// </summary>
public class SlotSellStore : Slot, IPointerClickHandler
{
    public ConfirmationBox confirmationBox;
    public Store store;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Opens a confirmation box when an item is clicked.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        ConfirmationBox box = Instantiate(confirmationBox, store.transform);
        box.GetComponent<RectTransform>().localPosition = Vector3.zero;
        box.Item = info;
        box.Store = store;
        TextMeshProUGUI boxTitle = box.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI boxDesc = box.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        boxTitle.text = $"Sell {info.Name} for ${info.Price}?";
        boxDesc.text = $"{info.Description}";
    }
}
