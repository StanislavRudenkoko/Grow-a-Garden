using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// New Game Name Set Up screen
/// Author: Tin Trinh
/// Date: Mar. 25, 2026
/// Revision: Apr. 14, 2026
/// Source: None
/// </summary>
public class NewGameNameSetUp : MonoBehaviour
{
    public TMP_InputField nameInput;
    public GameObject error;
    public Player player;
    public StoreInventory storeInventory;
    public ObjectGetter playerGetter;

    /// <summary>
    /// Starts the game.
    /// </summary>
    private void StartGame()
    {
        playerGetter = ObjectGetter.GetInstance;
        DontDestroyOnLoad(player);
        ObjectGetter.SetPlayer(player);
        ObjectGetter.SetStoreInventory(storeInventory);
        string name = nameInput.text.Trim();
        player.PlayerName = name;
        SceneManager.LoadScene("Garden");
    }

    /// <summary>
    /// Checks if the name is not empty before starting the game.
    /// If the name is empty, it will show an error message.
    /// </summary>
    public void CheckName()
    {
        if (nameInput.text == "")
        {
            error.SetActive(true);
        }
        else
        {
            StartGame();
        }
    }
}
