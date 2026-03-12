using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name { get; set; }
    public int Coins { get; set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set coins to starting amount
        Coins = 100;
    }

    // Update is called once per frame
    void Update()
    {

    }

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

    public bool GainCoins(int amount)
    {
        Coins += amount;
        return true;
    }
}
