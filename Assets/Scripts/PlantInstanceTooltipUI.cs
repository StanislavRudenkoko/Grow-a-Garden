/// <summary>
/// PlantInstanceTooltipUI
/// Author: Stanislav Rudenko
/// Date: Mar. 12 - Mar. 26, 2026
/// Source: with help of Claude AI
/// </summary>
using UnityEngine;
using TMPro;

/// <summary>
/// Combined tooltip UI displaying live <see cref="PlantInstance"/> stats
/// alongside static <see cref="PlantDefinition"/> info (name, growth, water, health, status, description).
/// </summary>
public class PlantInstanceTooltipUI : TooltipBase
{
    [Header("Instance Fields")]
    public TMP_Text nameText;
    public TMP_Text growthText;
    public TMP_Text waterText;
    public TMP_Text healthText;
    public TMP_Text statusText;
    public TMP_Text fretilizerText;

    [Header("Definition Fields")]
    public TMP_Text descriptionText;

    private PlantInstance currentInstance;

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("PlantInstanceTooltipUI Awake - rectTransform null: " + (rectTransform == null));
    }

    /// <summary>
    /// Populates all labels from the instance and its linked definition.
    /// </summary>
    public void SetData(PlantInstance instance)
    {
        currentInstance = instance;

        nameText.text = instance.customName;
        growthText.text = "Growth: Stage " + instance.currentGrowthStage;
        waterText.text = "Water: " + instance.waterLevel + "%";
        healthText.text = "Health: " + instance.health + "%";
        fretilizerText.text = "Fertilizer: " + instance.fertilizer + "%";
        statusText.text = "Status: " + instance.status.ToString();

        PlantDefinition def = PlantDatabaseManager.Instance
            .GetPlantDefinition(instance.plantDefinitionId);

        descriptionText.text = def != null ? def.descriptionShort : string.Empty;
    }

    /// <summary>Positions near the cursor and fades in.</summary>
    public void Show(Vector2 mousePosition)
    {
        Position(mousePosition);
        FadeIn();
    }

    /// <summary>Places the tooltip left or right of the pointer depending on screen half.</summary>
    private void Position(Vector2 mousePosition)
    {
        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        rectTransform.pivot = mousePosition.x > Screen.width / 2f
            ? new Vector2(1, 0.5f)
            : new Vector2(0, 0.5f);

        rectTransform.position = mousePosition +
            (rectTransform.pivot.x == 1
                ? new Vector2(-20, 0)
                : new Vector2(20, 0));

        ClampToScreen();
    }
}