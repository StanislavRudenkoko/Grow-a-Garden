
using TMPro;
using UnityEngine;


/// <summary>
/// Store class
/// Author: Tin Trinh
/// Date: Mar. 4, 2026
/// Revision: Mar. 25, 2026
/// Source: None
/// </summary>
public class Store : MonoBehaviour
{
    [SerializeField]
    private Player player;
    public StoreInventory StoreInventory {get; set;}
    void Start()
    {
        player = ObjectGetter.GetPlayer();
        StoreInventory = ObjectGetter.GetStoreInventory();
    }

    void Update()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Coins: ${player.Coins}";
    }

    /// <summary>
    /// Buy an item.
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="item"></param>
    public void BuyItem(int amount, Item item)
    {
        player.BuyItem(amount, item);
    }


}
