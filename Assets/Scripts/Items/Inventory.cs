using TMPro;
using UnityEngine;

/// <summary>
/// Inventory class.
/// Author: Tin Trinh
/// Date: Mar. 25, 2026
/// Source: None
/// </summary>
public class Inventory : MonoBehaviour
{
    public Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = ObjectGetter.GetInstance.player;
        TextMeshProUGUI coins = this.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        coins.text = $"Coins: ${player.Coins}";
    }

}
