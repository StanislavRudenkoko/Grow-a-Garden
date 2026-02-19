using UnityEngine;
using UnityEngine.UI;

public class PlantManager : MonoBehaviour
{
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
            plant.SetActive(true);
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
}