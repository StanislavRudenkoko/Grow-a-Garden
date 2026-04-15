using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Slot class that represents a single item slot in the inventory.
/// Author: Tin Trinh
/// Date: Mar. 4, 2026
/// Revision: Apr. 15, 2026
/// Source: None
/// </summary>
public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item info;
    public GameObject itemText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemText = gameObject.transform.root.GetChild(3).gameObject;
    }

    /// <summary>
    /// Displays information when mouse is hovering over a slot. Also highlights the slot.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponent<Image>().color = Color.yellow;
        TextMeshProUGUI itemNameDisplay = itemText.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI itemDescDisplay = itemText.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        itemNameDisplay.text = info.Name;
        itemDescDisplay.text = info.Description;
    }

    /// <summary>
    /// Removes the highlight.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponent<Image>().color = Color.clear;
    }
}

