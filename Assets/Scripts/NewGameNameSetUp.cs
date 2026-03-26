using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameNameSetUp : MonoBehaviour
{
    public TMP_InputField nameInput;
    public Player player;
    public PlayerGetter playerGetter;
    public void StartGame()
    {
        playerGetter = PlayerGetter.GetInstance;
        DontDestroyOnLoad(player);
        PlayerGetter.SetPlayer(player);
        string name = nameInput.text;
        player.PlayerName = name;
        SceneManager.LoadScene("Garden");
    }
}
