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
    public string Name { get; set; }
    public int Coins { get; set; }
    public List<Item> Inventory { get; set; }
    public int PlantsHarvested { get; set; }
    public int DayCount { get; set; }

    /// <summary>
    /// Constructor for existing player.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="coins"></param>
    /// <param name="plantsHarvested"></param>
    /// <param name="dayCount"></param>
    public Player(string name, int coins, List<Item> inventory, int plantsHarvested, int dayCount)
    {
        // Set coins to starting amount
        Name = name;
        Coins = coins;
        Inventory = inventory;
        PlantsHarvested = plantsHarvested;
        DayCount = dayCount;
    }
    /// <summary>
    /// Constructor for new player.
    /// </summary>
    /// <param name="name"></param>
    public Player(string name)
    {
        Name = name;
        Coins = 100;
        Inventory = new List<Item>();
        PlantsHarvested = 0;
        DayCount = 0;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
}
