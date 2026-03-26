using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


///<summary>
/// Scene for the main Garden screen
/// Authors: Joshua Trepanier
/// Date/revisions: March 4th 2026
///</summary>
public class GardenManager : MonoBehaviour
{
    public GameObject soil;
    public GameObject plant;
    public Sprite[] growthStages;

    public SpriteRenderer flowerRenderer;

    ///<summary>
    /// Load the sprites of the plants from the data passed from the PlantData Object
    ///</summary>
    void Start()
    {
    if (PlantData.Instance == null)
    {
        Debug.Log("PlantData not found!");
        return;
    }
    int stage = PlantData.Instance.currentStage;
    bool hasSoil = PlantData.Instance.hasSoil;
    bool hasSeed = PlantData.Instance.hasSeed;
    soil.SetActive(hasSoil);
    flowerRenderer.gameObject.SetActive(hasSeed);
    }

    ///<summary>
    /// Load the Plant Manager scene from the Garden Scene
    /// <param name="value"></param>
    ///</summary>
    public void focusOnPlant(int value)
    {
    if (PlantData.Instance != null)
    {
        PlantData.Instance.currentPot = value;
    }
    SceneManager.LoadScene(0);
    }
}