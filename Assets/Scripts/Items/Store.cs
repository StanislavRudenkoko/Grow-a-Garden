
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
    public GameObject contentBuy;
    public GameObject contentSell;
    private bool isBuyMode;
    void Start()
    {
        isBuyMode = true;
        player = ObjectGetter.GetPlayer();
        StoreInventory = ObjectGetter.GetStoreInventory();
    }

    void Update()
    {
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Coins: ${player.Coins}";
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
    
    /// <summary>
    /// Sell an item.
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="item"></param>
    public void SellItem(int amount, Item item)
    {
        player.SellItem(amount, item);
    }

    public void SellMode()
    {
        if (isBuyMode == true)
        {  
            contentSell.SetActive(true);
            contentBuy.SetActive(false);
            isBuyMode = false;
            transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = "Buy";
            contentSell.GetComponent<PopulateStoreSell>().Refresh();
        } 
        else
        {
            contentSell.SetActive(false);
            contentBuy.SetActive(true);
            isBuyMode = true;
            transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = "Sell";
            
        }
    }


}
