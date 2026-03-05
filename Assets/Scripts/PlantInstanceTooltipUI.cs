using UnityEngine;
using TMPro;

public class PlantInstanceTooltipUI : TooltipBase
{
    public TMP_Text nameText;
    public TMP_Text growthText;
    public TMP_Text waterText;
    public TMP_Text healthText;
    public TMP_Text statusText;

    private PlantInstance currentInstance;

    public void SetData(PlantInstance instance)
    {
        currentInstance = instance;

        nameText.text = instance.customName;
        growthText.text = "Stage: " + instance.currentGrowthStage;
        waterText.text = "Water: " + instance.waterLevel;
        healthText.text = "Health: " + instance.health + "%";
        statusText.text = instance.status.ToString();
    }

    public void Show(Vector2 mousePosition)
    {
        Position(mousePosition);
        FadeIn();
    }

    private void Position(Vector2 mousePosition)
    {
        rectTransform.pivot = mousePosition.x > Screen.width / 2f
            ? new Vector2(1, 0.5f)
            : new Vector2(0, 0.5f);

        rectTransform.position = mousePosition +
            (rectTransform.pivot.x == 1
                ? new Vector2(-20, 0)
                : new Vector2(20, 0));

        ClampToScreen();
    }

    public void OnShowMoreHover()
    {
        PlantDefinition def = PlantDatabaseManager.Instance
            .GetPlantDefinition(currentInstance.plantDefinitionId);

        TooltipManager.Instance.ShowDefinition(def, rectTransform.position);
    }
}