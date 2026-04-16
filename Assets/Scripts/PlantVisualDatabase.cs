using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlantVisualDatabase", menuName = "Plants/Plant Visual Database")]
public class PlantVisualDatabase : ScriptableObject
{
    [System.Serializable]
    public class SoilVisualEntry
    {
        public string soilType;   // e.g. "Loamy" — must match PlantManager.soilTypes
        public Sprite sprite;
    }

    public List<SoilVisualEntry> soilVisuals;
    public List<PlantVisualData> plantVisuals;

    public Sprite GetSoilSprite(string soilType)
    {
        return soilVisuals.Find(s => s.soilType == soilType)?.sprite;
    }

    public Sprite[] GetGrowthSprites(string plantDefinitionId)
    {
        return plantVisuals.Find(p => p.plantDefinitionId == plantDefinitionId)?.growthStageSprites;
    }
}