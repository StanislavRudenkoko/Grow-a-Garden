using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public Item info;
    public ConfirmationBox confirmationBox;
    public GameObject store;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        store = GameObject.Find("Store");
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

    public void OnPointerClick(PointerEventData eventData)
    {
        ConfirmationBox box = Instantiate(confirmationBox, store.transform);
        TextMeshProUGUI boxTitle = box.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI boxDesc = box.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        boxTitle.text = $"Buy {info.Name}?";
        boxDesc.text = $"{info.Description}";
    }
}
