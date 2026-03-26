
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Store class.
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

    public void BuyItem(int amount, Item item)
    {
        player.BuyItem(amount, item);
    }


}
