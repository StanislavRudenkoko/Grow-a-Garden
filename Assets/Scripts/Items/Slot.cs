using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler
{
    public Item info;

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
}
