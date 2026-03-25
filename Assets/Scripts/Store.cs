
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Store class.
/// </summary>
public class Store : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    void Start()
    {
        
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{Player.Coins} coins";
    }
    /// <summary>
    /// Takes the player back the the Garden scene when they click on the Go to Garden button.
    /// </summary>
    public void BackToGarden()
    {
        Debug.Log("Need to add data logic that stays between scenes.");
        SceneManager.LoadScene("Garden");
    }

    public void BuyItem(int amount)
    {
        Player.SpendCoins(amount);
    }


}
