using UnityEngine;
/// <summary>
/// Item class. Contains the item information and the quantity held by the store and the player.
/// Author: Tin Trinh
/// Date: Mar. 25, 2026
/// Source: None
/// </summary>
public class Item : MonoBehaviour
{
    [SerializeField] private int quantityStore;
    [SerializeField] private int quantityPlayer;
    [SerializeField] private ItemInfo itemInfo;
    public ItemInfo ItemInfo {set
        {
            itemInfo = value;
        }
    get => itemInfo;}

    public string Name { get => ItemInfo.Name; }
    public string Description { get => ItemInfo.Description; }
    public ItemCategory ItemCategory { get => ItemInfo.ItemCategory; }
    public int Price { get => ItemInfo.Price; }
    public Sprite ItemSprite { get => ItemInfo.ItemSprite; }
    public int QuantityStore { 
        get => quantityStore; 
        set { quantityStore = value; } 
    }
    public int QuantityPlayer { 
        get => quantityPlayer; 
        set { quantityPlayer = value; } 
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
