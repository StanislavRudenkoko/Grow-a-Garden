using UnityEngine;

public class NextDayButton : MonoBehaviour
{
    public void OnNextDay()
    {
        foreach (PlantPotController pot in FindObjectsByType<PlantPotController>(FindObjectsSortMode.None))
        {
            PlantInstance data = pot.potData;

            if (!data.hasPlant) continue;

            PlantDefinition def = PlantDatabaseManager.Instance
                .GetPlantDefinition(data.plantDefinitionId);

            if (def == null) continue;

            int maxStage = def.growth.totalGrowthStages - 1;

            if (data.currentGrowthStage < maxStage)
            {
                data.currentGrowthStage++;
                pot.RefreshVisuals();
            }
        }
    }
}