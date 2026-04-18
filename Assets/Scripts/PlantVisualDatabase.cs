using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// PlantVisualDatabase
/// Author: Stanislav Rudenko
/// Date: April 18th, 2026
/// </summary>
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

    /// <summary>
    /// Get correct soil sprite for the pot
    /// </summary>
    /// <param name="soilType"></param>
    public Sprite GetSoilSprite(string soilType)
    {
        return soilVisuals.Find(s => s.soilType == soilType)?.sprite;
    }

    /// <summary>
    /// Return array of sprites for a specific plant
    /// </summary>
    /// <param name="plantDefinitionId"></param>
    public Sprite[] GetGrowthSprites(string plantDefinitionId)
    {
        return plantVisuals.Find(p => p.plantDefinitionId == plantDefinitionId)?.growthStageSprites;
    }
}