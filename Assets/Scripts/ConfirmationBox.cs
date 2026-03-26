using UnityEngine;
/// <summary>
/// Confirmation box
/// Author: Tin Trinh
/// Date: Mar. 4, 2026
/// Source: None
/// </summary>
public class ConfirmationBox : MonoBehaviour
{
    public Item Item { get; set; }
    public Store Store {get; set;}
    public GameObject content;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        content = GameObject.FindGameObjectWithTag("Content");
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// Buys the item when clicked. *The logic is currently not implemented
    /// </summary>
    public void Buy()
    {
        if (Item.QuantityStore > 0)
        {   
            Store.BuyItem(Item.Price, Item);
            content.GetComponent<PopulateStore>().Refresh();
        }
        else
        {
            Debug.Log("another pop up");
        }
        // logic here
        Destroy(gameObject);
    }

    /// <summary>
    /// Closes the confirmation object when clicked.
    /// </summary>
    public void Cancel()
    {
        Destroy(gameObject);
    }
}
