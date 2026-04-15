using System;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Confirmation box
/// Author: Tin Trinh
/// Date: Mar. 4, 2026
/// Source: None
/// </summary>
public class ConfirmationBuyBox : ConfirmationBox
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        content = GameObject.FindGameObjectWithTag("ContentBuy");
    }

    /// <summary>
    /// Buys the item when clicked. *The logic is currently not implemented
    /// </summary>
    void Buy()
    {
        if (Item.QuantityStore > 0)
        {
            Store.BuyItem(Item.Price, Item);
            content.GetComponent<PopulateStore>().UpdateQuantity();
        }
        else
        {
            Debug.Log("another pop up");
        }
        // logic here
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Defines the action.
    /// </summary>
    public override void Action()
    {
        Buy();
    }

}
