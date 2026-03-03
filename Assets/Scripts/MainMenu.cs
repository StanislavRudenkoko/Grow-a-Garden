using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("Garden");
    }

    public void ContinueGame()
    {
        Debug.Log("The player data will load");
        SceneManager.LoadScene("Garden");
    }

    public void QuitGame()
    {
        Debug.Log("Game quit");
        Application.Quit();
    }
}
