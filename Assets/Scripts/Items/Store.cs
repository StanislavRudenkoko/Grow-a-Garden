
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Store class
/// Author: Tin Trinh
/// Date: Mar. 4, 2026
/// Revision: Apr. 14, 2026
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
    public GameObject speechBubble;
    public Button buy;
    public Button sell;
    public ItemCategory? sellCategory;
    void Start()
    {
        player = ObjectGetter.GetPlayer();
        StoreInventory = ObjectGetter.GetStoreInventory();
    }

    void Update()
    {
        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"Coins: ${player.Coins}";
    }

    /// <summary>
    /// Buy an item.
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="item"></param>
    public void BuyItem(int amount, Item item)
    {
        if (amount < player.Coins)
        {
            player.BuyItem(amount, item);
        }
        else
        {
            speechBubble.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            speechBubble.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "You don't have enough coins for that!";
        }
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

    /// <summary>
    /// Switches store to sell mode.
    /// </summary>
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

    /// <summary>
    /// Switches store to buy mode.
    /// </summary>
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

    /// <summary>
    /// Disables all buttons in a group and enables the specified one.
    /// </summary>
    /// <param name="categoryGroup"></param>
    /// <param name="index"></param>
    private void CategoryPress(GameObject categoryGroup, int index)
    {
        var children = categoryGroup.transform.GetComponentsInChildren<Button>();
        foreach (Button item in children)
        {
            item.interactable = true;
        }
        children[index].interactable = false;
    }

    /// <summary>
    /// Switches category to all items in buy section.
    /// </summary>
    public void BuyAllButton()
    {
        CategoryPress(categoriesBuy, 0);
        contentBuy.GetComponent<PopulateStore>().Refresh();
    }
    /// <summary>
    /// Switches category to equipment in buy section.
    /// </summary>
    public void BuyEquipmentButton()
    {
        CategoryPress(categoriesBuy, 1);
        contentBuy.GetComponent<PopulateStore>().Refresh(ItemCategory.EQUIPMENT);
    }
    /// <summary>
    /// Switches category to soil in buy section.
    /// </summary>
    public void BuySoilButton()
    {
        CategoryPress(categoriesBuy, 2);
        contentBuy.GetComponent<PopulateStore>().Refresh(ItemCategory.SOIL);
    }
    /// <summary>
    /// Switches category to fertilizer in buy section.
    /// </summary>
    public void BuyFertilizerButton()
    {
        CategoryPress(categoriesBuy, 3);
        contentBuy.GetComponent<PopulateStore>().Refresh(ItemCategory.FERTILIZER);
    }
    /// <summary>
    /// Switches category to seeds in buy section.
    /// </summary>
    public void BuySeedsButton()
    {
        CategoryPress(categoriesBuy, 4);
        contentBuy.GetComponent<PopulateStore>().Refresh(ItemCategory.SEED);
    }
    /// <summary>
    /// Switches category to all items in sell section.
    /// </summary>
    public void SellAllButton()
    {
        CategoryPress(categoriesSell, 0);
        sellCategory = null;
        contentSell.GetComponent<PopulateStoreSell>().Refresh();
    }
    /// <summary>
    /// Switches category to produce in buy section.
    /// </summary>
    public void SellProduceButton()
    {
        CategoryPress(categoriesSell, 1);
        sellCategory = ItemCategory.PRODUCE;
        contentSell.GetComponent<PopulateStoreSell>().Refresh(ItemCategory.PRODUCE);
    }
    /// <summary>
    /// Switches category to soil in buy section.
    /// </summary>
    public void SellSoilButton()
    {
        CategoryPress(categoriesSell, 2);
        sellCategory = ItemCategory.SOIL;
        contentSell.GetComponent<PopulateStoreSell>().Refresh(ItemCategory.SOIL);
    }
    /// <summary>
    /// Switches category to soil in buy section.
    /// </summary>
    public void SellFertilizerButton()
    {
        CategoryPress(categoriesSell, 3);
        sellCategory = ItemCategory.FERTILIZER;
        contentSell.GetComponent<PopulateStoreSell>().Refresh(ItemCategory.FERTILIZER);
    }
    /// <summary>
    /// Switches category to seeds in buy section.
    /// </summary>
    public void SellSeedsButton()
    {
        CategoryPress(categoriesSell, 4);
        sellCategory = ItemCategory.SEED;
        contentSell.GetComponent<PopulateStoreSell>().Refresh(ItemCategory.SEED);
    }

}
