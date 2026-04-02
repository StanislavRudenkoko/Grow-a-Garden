using UnityEngine;
/// <summary>
/// Confirmation box
/// Author: Tin Trinh
/// Date: Mar. 4, 2026
/// Source: None
/// </summary>
public class ConfirmationSellBox : ConfirmationBox
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        content = GameObject.FindGameObjectWithTag("ContentSell");

    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// Buys the item when clicked. *The logic is currently not implemented
    /// </summary>
    public void Sell()
    {
        if (Item.QuantityPlayer > 0)
        {   
            Store.SellItem(Item.SellPrice, Item);
            content.GetComponent<PopulateStoreSell>().Refresh();
        }
        else
        {
            Debug.Log("another pop up");
        }
        // logic here
        Destroy(this.gameObject);
    }
        public override void Action()
    {
        Sell();
    }

}
