using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantManager : MonoBehaviour
{
    public GameObject plantPrefab;
    private List<GameObject> spawnedPlants = new List<GameObject>();
    public Transform potParent;
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

    void Start()
    {
        sr = plant.GetComponent<SpriteRenderer>();
        soil.SetActive(false);
        plant.SetActive(false);
        sr.sprite = growthStages[0];
        harvestButton.gameObject.SetActive(false);
        seedButton.gameObject.SetActive(false);
        waterButton.gameObject.SetActive(false);
    }

    void Update()
    {
        foreach (var plantObj in spawnedPlants)
        {
            PlantHoverHandler hover = plantObj.GetComponent<PlantHoverHandler>();
            PlantInstance instance = hover.GetInstance();
            SpriteRenderer sr = plantObj.GetComponent<SpriteRenderer>();

            if (instance.status == PlantStatus.Healthy && instance.waterLevel > 0 && instance.currentGrowthStage < growthStages.Length - 1)
            {
                instance.status = PlantStatus.Growing;
            }

            if (instance.status == PlantStatus.Growing)
            {
                instance.timer += Time.deltaTime;
                if (instance.timer >= growthTime)
                {
                    instance.timer = 0f;
                    instance.waterLevel = 0;
                    instance.status = PlantStatus.Healthy;
                    instance.currentGrowthStage++;
                    sr.sprite = growthStages[instance.currentGrowthStage];
                }
            }
        }
    }

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

    public void OnSeedButtonPressed()
    {
        if (hasSoil && !hasSeed)
        {
            hasSeed = true;

            // Instantiate the plant prefab
            GameObject newPlant = Instantiate(plantPrefab, plant.transform.position, Quaternion.identity);

            // Initialize PlantInstance data
            PlantInstance instanceData = new PlantInstance
            {
                customName = "My Plant",
                currentGrowthStage = 0,
                waterLevel = 0,
                health = 100,
                status = PlantStatus.Healthy,
                plantDefinitionId = "example_plant"
            };

            // Assign to hover handler
            PlantHoverHandler hover = newPlant.GetComponent<PlantHoverHandler>();
            hover.Initialize(instanceData);

            // Assign SpriteRenderer for growth
            SpriteRenderer sr = newPlant.GetComponent<SpriteRenderer>();
            sr.sprite = growthStages[0];

            // Track spawned plants
            spawnedPlants.Add(newPlant);

            // Hide seed button, enable water
            seedButton.gameObject.SetActive(false);
            waterButton.gameObject.SetActive(true);
        }
    }

    public void OnWaterButtonPressed()
    {
        if (!isWatered && hasSeed && currentStage < growthStages.Length - 1)
        {
            isWatered = true;
        }
    }

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

    public void OnAddPotButtonPressed()
    {
        if (plantPrefab == null)
        {
            Debug.LogError("PlantPotPrefab not assigned in PlantManager!");
            return;
        }

        GameObject newPot = Instantiate(plantPrefab, Vector3.zero, Quaternion.identity, potParent);

        //// Optionally, you can position it somewhere in the scene
        //newPot.transform.position = GetNextPotPosition();
    }

}
