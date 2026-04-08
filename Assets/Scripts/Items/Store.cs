
using TMPro;
using UnityEngine;
using UnityEngine.UI;


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
    public Button buy;
    public Button sell;
    void Start()
    {
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
        sell.GetComponent<Button>().interactable = false;
        buy.GetComponent<Button>().interactable = true;
        contentSell.SetActive(true);
        contentBuy.SetActive(false);   
        contentSell.GetComponent<PopulateStoreSell>().Refresh();     
    }

    public void BuyMode()
    {
        buy.GetComponent<Button>().interactable = false;
        sell.GetComponent<Button>().interactable = true;
        contentSell.SetActive(false);
        contentBuy.SetActive(true);
        contentSell.GetComponent<PopulateStoreSell>().Refresh();
    }


}
