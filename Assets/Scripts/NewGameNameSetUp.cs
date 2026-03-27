using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// New Game Name Set Up screen
/// Author: Tin Trinh
/// Date: Mar. 25, 2026
/// Source: None
/// </summary>
public class NewGameNameSetUp : MonoBehaviour
{
    public TMP_InputField nameInput;
    public Player player;
    public StoreInventory storeInventory;
    public ObjectGetter playerGetter;
    public void StartGame()
    {
        playerGetter = ObjectGetter.GetInstance;
        DontDestroyOnLoad(player);
        ObjectGetter.SetPlayer(player);
        ObjectGetter.SetStoreInventory(storeInventory);
        string name = nameInput.text;
        player.PlayerName = name;
        SceneManager.LoadScene("Garden");
    }
}
