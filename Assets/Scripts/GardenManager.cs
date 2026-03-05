using System.Diagnostics.Contracts;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GardenManager : MonoBehaviour
{

    public Button seedButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void focusOnPlant(int value)
    {
        Debug.Log(value);
        SceneManager.LoadScene(0);
    }
}
