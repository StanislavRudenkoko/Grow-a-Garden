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

    private SpriteRenderer sr; 


    ///<summary>
    /// Load the sprites of the plants from the data passed from the PlantData Object
    ///</summary>
    void Start()
    {
        if (PlantData.Instance == null)
        {
            Debug.LogError("PlantData not found!");
            return;
        }
        sr = plant.GetComponent<SpriteRenderer>();
        int stage = Mathf.Clamp(PlantData.Instance.currentStage, 0, growthStages.Length - 1);
        bool hasSoil = PlantData.Instance.hasSoil;
        bool hasSeed = PlantData.Instance.hasSeed;
        soil.SetActive(hasSoil);
        plant.SetActive(hasSeed);
        if (hasSeed)
        {
            sr.sprite = growthStages[stage];
        }
    }

    ///<summary>
    /// Load the Plant Manager scene from the Garden Scene
    /// <param name="value"></param>
    ///</summary>
    public void focusOnPlant(int value)
    {
        SceneManager.LoadScene(0);
    }
}