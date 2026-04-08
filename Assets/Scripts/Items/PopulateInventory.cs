using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Populates the player inventory.
/// Author: Tin Trinh
/// Date: Mar. 25, 2026
/// Source: Various discussions on https://discussions.unity.com
/// </summary>
public class PopulateInventory : MonoBehaviour
{
    public Slot slot;
    public Player player;
    public GameObject container;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Populate();
    }

    /// <summary>
    /// Populates the inventory items slots.
    /// </summary>
    void Populate()
    {
        if (!player)
        {
            player = ObjectGetter.GetPlayer();
        }
        Slot obj;
        foreach (Item item in player.Inventory)
        {
            obj = Instantiate(slot, transform);
            obj.info = item;
            Image objImage = obj.transform.GetChild(0).GetComponent<Image>();
            TextMeshProUGUI objTitle = obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI objPrice = obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI objQuantity = obj.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            objImage.sprite = obj.info.ItemSprite;
            objImage.preserveAspect = true;
            objTitle.text = obj.info.Name;
            objPrice.text = $"${obj.info.Price}";
            objQuantity.text = $"x{obj.info.QuantityPlayer}";
        }
    }
    public void Refresh()
    {
        foreach(Transform child in container.transform)
        {
            Destroy(child.gameObject);
        }
        Populate();
    }
}
