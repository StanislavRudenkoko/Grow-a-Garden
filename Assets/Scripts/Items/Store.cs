
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
    public StoreInventory StoreInventory { get; set; }
    public GameObject contentBuy;
    public GameObject contentSell;
    public GameObject categoriesBuy;
    public GameObject categoriesSell;
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
        categoriesSell.SetActive(true);
        categoriesBuy.SetActive(false);
        contentSell.GetComponent<PopulateStoreSell>().Refresh();
    }

    public void BuyMode()
    {
        buy.GetComponent<Button>().interactable = false;
        sell.GetComponent<Button>().interactable = true;
        contentSell.SetActive(false);
        contentBuy.SetActive(true);
        categoriesSell.SetActive(false);
        categoriesBuy.SetActive(true);
        contentSell.GetComponent<PopulateStoreSell>().Refresh();
    }

    private void CategoryPress(GameObject categoryGroup, int index)
    {
        var children = categoryGroup.transform.GetComponentsInChildren<Button>();
        foreach (Button item in children)
        {
            item.interactable = true;
        }
        children[index].interactable = false;
    }

    public void BuyAllButton()
    {
        CategoryPress(categoriesBuy, 0);
        contentBuy.GetComponent<PopulateStore>().Refresh();
    }
    public void BuyEquipmentButton()
    {
        CategoryPress(categoriesBuy, 1);
        contentBuy.GetComponent<PopulateStore>().Refresh(ItemCategory.EQUIPMENT);
    }
    public void BuySoilButton()
    {
        CategoryPress(categoriesBuy, 2);
        contentBuy.GetComponent<PopulateStore>().Refresh(ItemCategory.SOIL);
    }
    public void BuySeedsButton()
    {
        CategoryPress(categoriesBuy, 3);
        contentBuy.GetComponent<PopulateStore>().Refresh(ItemCategory.SEED);
    }
    public void SellAllButton()
    {
        CategoryPress(categoriesSell, 0);
        contentSell.GetComponent<PopulateStoreSell>().Refresh();
    }

    public void SellProduceButton()
    {
        CategoryPress(categoriesSell, 1);
        contentSell.GetComponent<PopulateStoreSell>().Refresh(ItemCategory.PRODUCE);
    }
    public void SellSoilButton()
    {
        CategoryPress(categoriesSell, 2);
        contentSell.GetComponent<PopulateStoreSell>().Refresh(ItemCategory.SOIL);
    }
    public void SellSeedsButton()
    {
        CategoryPress(categoriesSell, 3);
        contentSell.GetComponent<PopulateStoreSell>().Refresh(ItemCategory.SEED);
    }

}
