using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Slot : MonoBehaviour, IPointerEnterHandler
{
    public Item info;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Image slotImage = GetComponent<Image>();
        // slotImage.sprite = info.itemSprite;
        // slotImage.preserveAspect = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TextMeshProUGUI itemNameDisplay = GameObject.Find("ItemName").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI itemDescDisplay = GameObject.Find("ItemDesc").GetComponent<TextMeshProUGUI>();
        itemNameDisplay.text = info.Name;
        itemDescDisplay.text = info.Description;

    }
}
