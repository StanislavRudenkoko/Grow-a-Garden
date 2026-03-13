using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlantInstance))]
[RequireComponent(typeof(PlantHoverHandler))]
public class PlantPotController : MonoBehaviour, IPointerClickHandler
{
    [HideInInspector] public PlantInstance potData;

    [Header("Pot Visuals")]
    public SpriteRenderer soilSpriteRenderer;
    public SpriteRenderer plantSpriteRenderer;

    // No more sprite arrays here — all sprites live in PlantManager.VisualDatabase

    private void Awake()
    {
        potData = GetComponent<PlantInstance>();
        GetComponent<PlantHoverHandler>().Initialize(potData);

        if (soilSpriteRenderer != null) soilSpriteRenderer.gameObject.SetActive(false);
        if (plantSpriteRenderer != null) plantSpriteRenderer.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (TooltipManager.Instance != null)
            TooltipManager.Instance.HideAll();

        if (PotDropdownUI.Instance != null)
            PotDropdownUI.Instance.OpenFor(this);
    }

    public void RefreshVisuals()
    {
        PlantVisualDatabase db = PlantManager.Instance.visualDatabase;

        // Soil
        if (soilSpriteRenderer != null)
        {
            if (potData.hasSoil)
            {
                soilSpriteRenderer.sprite = db.GetSoilSprite(potData.soilType);
                soilSpriteRenderer.gameObject.SetActive(true);
            }
            else
            {
                soilSpriteRenderer.gameObject.SetActive(false);
            }
        }

        // Plant
        if (plantSpriteRenderer != null)
        {
            if (potData.hasPlant)
            {
                Sprite[] stages = db.GetGrowthSprites(potData.plantDefinitionId);
                if (stages != null && stages.Length > 0)
                {
                    int stage = Mathf.Clamp(potData.currentGrowthStage, 0, stages.Length - 1);
                    plantSpriteRenderer.sprite = stages[stage];
                }
                plantSpriteRenderer.gameObject.SetActive(true);
            }
            else
            {
                plantSpriteRenderer.gameObject.SetActive(false);
            }
        }
    }
}