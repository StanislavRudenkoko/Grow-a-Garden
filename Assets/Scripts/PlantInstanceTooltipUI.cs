/// <summary>
/// PlantInstanceTooltipUI
/// Author: Stanislav Rudenko
/// Date: Mar. 12 - Mar. 26, 2026
/// Source: with help of Claude AI
/// </summary>

using UnityEngine;
using TMPro;

/// <summary>
/// Tooltip UI for live <see cref="PlantInstance"/> stats (name, growth, water, health, status).
/// </summary>
public class PlantInstanceTooltipUI : TooltipBase
{
    public TMP_Text nameText;
    public TMP_Text growthText;
    public TMP_Text waterText;
    public TMP_Text healthText;
    public TMP_Text statusText;

    private PlantInstance currentInstance;

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("PlantInstanceTooltipUI Awake - rectTransform null: " + (rectTransform == null));
    }

    /// <summary>Updates labels from the given instance.</summary>
    public void SetData(PlantInstance instance)
    {
        currentInstance = instance;
        nameText.text = instance.customName;
        growthText.text = "Stage: " + instance.currentGrowthStage;
        waterText.text = "Water: " + instance.waterLevel;
        healthText.text = "Health: " + instance.health + "%";
        statusText.text = instance.status.ToString();
    }

    /// <summary>Positions near the cursor and fades in.</summary>
    public void Show(Vector2 mousePosition)
    {
        Position(mousePosition);
        FadeIn();
    }

    /// <summary>Places the tooltip to the left or right of the pointer depending on screen half.</summary>
    private void Position(Vector2 mousePosition)
    {
        // Initialize if Awake hasn't run yet
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

    /// <summary>Opens the definition tooltip for the current plant.</summary>
    public void OnShowMoreHover()
    {
        PlantDefinition def = PlantDatabaseManager.Instance
            .GetPlantDefinition(currentInstance.plantDefinitionId);
        TooltipManager.Instance.ShowDefinition(def, rectTransform.position);
    }
}
