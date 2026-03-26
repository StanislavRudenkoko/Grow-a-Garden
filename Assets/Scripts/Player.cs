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
    /// Method to spend player coins.
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool SpendCoins(int amount)
    {
        if (amount > Coins)
        {
            Debug.Log("Player does not have enough coins, alert pop up");
            return false;
        }
        Debug.Log("Player's Coins get updated. Remember to update Store");
        Coins -= amount;
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
