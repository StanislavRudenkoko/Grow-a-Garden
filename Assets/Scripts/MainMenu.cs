using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main Menu.
/// Author: Tin Trinh
/// Date: Mar. 4, 2026
/// Source: None
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Starts a new game when clicked.
    /// </summary>
    public void NewGame()
    {
        SceneManager.LoadScene("NewGameNameSetUp");
    }

    /// <summary>
    /// Continues game when clicked.
    /// </summary>
    public void ContinueGame()
    {
        Debug.Log("The player data will load");
        ///Player
        SceneManager.LoadScene("Garden");
    }

    /// <summary>
    /// Quits game when clicked.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Game quit");
        Application.Quit();
    }
}
