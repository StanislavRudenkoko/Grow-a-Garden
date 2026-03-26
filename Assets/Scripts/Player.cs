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
        Coins = 100;
        Inventory = new List<Item>();
        PlantsHarvested = 0;
        DayCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

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
        Inventory.Add(item);
        return true;
    }

    /// <summary>
    /// Method for player to gain coins.
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool GainCoins(int amount)
    {
        Coins += amount;
        return true;
    }

    public override string ToString()
    {
        return string.Format("Player: {0} / {1} / {2}", PlayerName, Coins, PlantsHarvested);
    }
}
