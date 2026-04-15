/// <summary>
/// PlantPotController
/// Author: Stanislav Rudenko
/// Date: Mar. 12 - Mar. 26, 2026
/// Source: with help of Claude AI
/// </summary>

using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Per-pot behaviour: click opens the dropdown; updates soil/plant sprites from <see cref="PlantVisualDatabase"/>.
/// </summary>
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
        Debug.Log("Pot clicked");
        Debug.Log("PotDropdownUI.Instance is null: " + (PotDropdownUI.Instance == null));
        Debug.Log("TooltipManager.Instance is null: " + (TooltipManager.Instance == null));

        if (TooltipManager.Instance != null)
            TooltipManager.Instance.HideAll();

        if (PotDropdownUI.Instance != null)
            PotDropdownUI.Instance.OpenFor(this);
    }

    /// <summary>
    /// Syncs soil and plant sprites to <see cref="PlantInstance"/> state.
    /// </summary>
    public void RefreshVisuals()
    {
        PlantVisualDatabase db = PlantManager.Instance.visualDatabase;

        // Soil
        if (soilSpriteRenderer != null)
        {
            if (potData.hasSoil)
            {
                soilSpriteRenderer.sprite = db.GetSoilSprite("Dirt");
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
