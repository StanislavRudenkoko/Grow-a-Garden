using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGameNameSetUp : MonoBehaviour
{
    public TMP_InputField nameInput;
    public Player player;
    public void StartGame()
    {
        DontDestroyOnLoad(player);
        string name = nameInput.text;
        player.PlayerName = name;
        Debug.Log(player);
        Debug.Log(name);
        SceneManager.LoadScene("Garden");
    }
}
