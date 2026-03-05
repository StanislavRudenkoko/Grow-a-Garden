using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name { get; }
    public int Coins { get; set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Coins = 100;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool UpdateCoins(int amount)
    {
        if (amount > Coins)
        {
            Debug.Log("Player does not have enough coins, alert pop up");
            return false;
        }
        Coins -= amount;
        return true;
    }
}
