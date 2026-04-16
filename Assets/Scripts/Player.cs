using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player class.
/// Author: Tin Trinh
/// Date: Mar. 12, 2026
/// Source: None
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField]
    private string playerName;
    [SerializeField]
    private int coins;
    [SerializeField]
    private List<Item> inventory;
    [SerializeField]
    private int plantsHarvested;
    [SerializeField]
    private int dayCount;
    public string PlayerName { get => playerName; set => playerName = value; }
    public int Coins { get => coins; set => coins = value; }
    public List<Item> Inventory { get => inventory; set => inventory = value; }
    public int PlantsHarvested { get => plantsHarvested; set => plantsHarvested = value; }
    public int DayCount { get => dayCount; set => dayCount = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Coins = 500;
        Inventory = new List<Item>();
        PlantsHarvested = 0;
        DayCount = 1;
    }

    /// <summary>
    /// Method to buy an item.
    /// </summary>
    /// <param name="amount"></param>
    /// <returns>a bool</returns>
    public bool BuyItem(int amount, Item item)
    {
        if (amount > Coins)
        {
            return false;
        }

        Coins -= amount;
        if (!Inventory.Contains(item))
        {
            Inventory.Add(item);
        }
        item.QuantityPlayer++;
        item.QuantityStore--;
        return true;
    }

    /// <summary>
    /// Method for player to gain coins.
    /// </summary>
    /// <param name="amount"></param>
    /// <returns>a bool</returns>
    public bool SellItem(int amount, Item item)
    {
        item.QuantityPlayer--;
        Coins += amount;
        if (item.QuantityPlayer == 0)
        {
            Inventory.Remove(item);
        }
        return true;
    }
    /// <summary>
    /// Returns a representation of the player.
    /// </summary>
    /// <returns>a string</returns>
    public override string ToString()
    {
        return string.Format("Player: {0} / {1} / {2}", PlayerName, Coins, PlantsHarvested);
    }
}
