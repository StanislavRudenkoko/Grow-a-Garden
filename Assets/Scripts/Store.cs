
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    
    public void BackToGarden()
    {
        Debug.Log("Need to add data logic that stays between scenes.");
        SceneManager.LoadScene("Garden");
    }



}
