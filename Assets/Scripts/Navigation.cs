using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Navigation functions for buttons.
/// Author: Tin Trinh
/// Date: Mar. 25, 2026
/// Source: None
/// </summary>
public class Navigation : MonoBehaviour
{
    /// <summary>
    /// Takes the player to the Store scene.
    /// </summary>
    public void ToStore()
    {
        SceneManager.LoadScene("Store");
    }
    /// <summary>
    /// Takes the player to the Garden scene.
    /// </summary>
    public void ToGarden()
    {
        SceneManager.LoadScene("Garden");
    }
    /// <summary>
    /// Takes the player to the Main Menu scene.
    /// </summary>
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    /// <summary>
    /// Takes the player to the Inventory scene.
    /// </summary>
    public void ToInventory()
    {
        SceneManager.LoadScene("Inventory");
    }
    /// <summary>
    /// Takes the player to the NewGameNameSetUp scene.
    /// </summary>
    public void ToNewGameNameSetUp()
    {
        SceneManager.LoadScene("NewGameNameSetUp");
    }

}
