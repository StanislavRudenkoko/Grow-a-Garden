
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Store class.
/// </summary>
public class Store : MonoBehaviour
{

    /// <summary>
    /// Takes the player back the the Garden scene when they click on the Go to Garden button.
    /// </summary>
    public void BackToGarden()
    {
        Debug.Log("Need to add data logic that stays between scenes.");
        SceneManager.LoadScene("Garden");
    }



}
