using UnityEngine;
using UnityEngine.UI;

public class PlantManager : MonoBehaviour
{
    public Sprite[] growthStages; // drag your 5 sprites in here
    public float growthTime = 3f; // seconds per stage

    public Button waterButton;
    public Button harvestButton;

    private SpriteRenderer sr;
    private int currentStage = 0;
    private bool isGrowing = false;
    private float timer = 0f;
    private bool isWatered = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = growthStages[0];
        harvestButton.gameObject.SetActive(false);
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

    public void OnWaterButtonPressed()
    {
        if (!isWatered && currentStage < growthStages.Length - 1)
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