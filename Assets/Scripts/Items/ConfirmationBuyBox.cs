
using TMPro;
using UnityEngine;
/// <summary>
/// Confirmation box
/// Author: Tin Trinh
/// Date: Mar. 4, 2026
/// Source: None
/// </summary>
public class ConfirmationBuyBox : ConfirmationBox
{
    public Transform speechBubble;
    public Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        content = GameObject.FindGameObjectWithTag("ContentBuy");
        speechBubble = content.transform.root.GetChild(3);
        player = ObjectGetter.GetPlayer();
    }

    /// <summary>
    /// Buys the item when clicked if conditions are met.
    /// </summary>
    public void Buy()
    {
        if (player.Coins < Item.Price)
        {
            NotEnoughCoins();
        }
        else if (Item.QuantityStore > 0)
        {
            Store.BuyItem(Item.Price, Item);
            content.GetComponent<PopulateStore>().UpdateQuantity();
            if (Item.QuantityStore == 0)
            {
                Slot.interactable = false;
            }
            Destroy(this.gameObject);
        }



    }

    /// <summary>
    /// Changes the dialogue box to show that the player does not have enough coins to buy the item.
    /// </summary>
    private void NotEnoughCoins()
    {
        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = "Okay";
        this.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = $"Could not buy {Item.Name}!";
        this.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Insufficient coins.";
    }

    /// <summary>
    /// Defines the action.
    /// </summary>
    public override void Action()
    {
        Buy();
    }

}
