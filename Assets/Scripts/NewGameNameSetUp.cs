using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
