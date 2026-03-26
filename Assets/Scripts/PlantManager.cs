using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

///<summary>
/// Scene for the main Garden screen
/// Authors: Joshua Trepanier, Stanislav Rudenoko
/// Date/revisions: March 4th 2026
///</summary>
public class PlantManager : MonoBehaviour
{
    public GameObject plantData;
    public GameObject soil;
    public GameObject plant;
    public Sprite[] growthStages; // drag your 5 sprites in here
    public float growthTime = 3f; // seconds per stage

    public Button soilButton;
    public Button seedButton;
    public Button waterButton;
    public Button harvestButton;

    private SpriteRenderer sr;
    private bool hasSoil = false;
    private bool hasSeed = false;
    private int currentStage = 0;
    private bool isGrowing = false;
    private float timer = 0f;
    private bool isWatered = false;

    ///<summary>
    /// On Scene start load plant sprites and buttons to correct state
    ///</summary>
    void Start()
{
    var data = PlantData.Instance;

    hasSoil = data.hasSoil;
    hasSeed = data.hasSeed;
    currentStage = data.currentStage;

    soil.SetActive(hasSoil);
    plant.SetActive(hasSeed);
    sr = plant.GetComponent<SpriteRenderer>();
    sr.sprite = growthStages[currentStage];
}

    ///<summary>
    /// Every frame check if plant is growing to increase the growth stage
    /// and update the sprite
    ///</summary>
    void Update()
    {
        if (isWatered && !isGrowing && currentStage < growthStages.Length - 1)
        {
            isGrowing = true;
        }

        if (isGrowing)
        {
            timer += Time.deltaTime;
            if (timer >= growthTime)
            {
                timer = 0f;
                isWatered = false;
                isGrowing = false;
                currentStage++;
                sr.sprite = growthStages[currentStage];

                if (currentStage == growthStages.Length - 1)
                {
                    waterButton.gameObject.SetActive(false);
                    harvestButton.gameObject.SetActive(true);
                }
            }
        }
    }

    ///<summary>
    /// Switch scene back to the garden and save currents plants data to the
    /// PlantData gameObject
    ///</summary>
    public void BackToGarden()
    {
    if (PlantData.Instance == null)
    {
        Debug.LogError("PlantData instance not found!");
        return;
    }
    PlantData.Instance.currentStage = currentStage;
    PlantData.Instance.hasSoil = hasSoil;
    PlantData.Instance.hasSeed = hasSeed;
    SceneManager.LoadScene(1);
    }

    ///<summary>
    /// If pot has no soil add soil to the pot and update the sprite
    ///</summary>
    public void OnSoilButtonPressed()
    {
        if (!hasSoil)
        {
            hasSoil = true;
            soil.SetActive(true);
            soilButton.gameObject.SetActive(false);
            seedButton.gameObject.SetActive(true);
        }
    }

    ///<summary>
    /// If pot has no seed, but has soil. Add a seed to the pot and update the sprite
    ///</summary>
    public void OnSeedButtonPressed()
    {
        if (hasSoil && !hasSeed)
        {
            hasSeed = true;
            plant.SetActive(true);
            seedButton.gameObject.SetActive(false);
            waterButton.gameObject.SetActive(true);
        }
    }

    ///<summary>
    /// Water the plant and update a boolean
    ///</summary>
    public void OnWaterButtonPressed()
    {
        if (!isWatered && hasSeed && currentStage < growthStages.Length - 1)
        {
            isWatered = true;
        }
    }

    ///<summary>
    /// Remove the plant from the pot and reset the growing process
    ///</summary>
    public void OnHarvestButtonPressed()
    {
        currentStage = 0;
        sr.sprite = growthStages[0];
        isWatered = false;
        isGrowing = false;
        timer = 0f;
        waterButton.gameObject.SetActive(true);
        harvestButton.gameObject.SetActive(false);
    }
}