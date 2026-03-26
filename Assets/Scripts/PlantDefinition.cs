/// <summary>
/// PlantDefinition
/// Author: Stanislav Rudenko
/// Date: Mar. 12 - Mar. 26, 2026
/// Source: with help of Claude AI
/// </summary>

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Serializable plant species data (identity, growth, environment, economy).
/// </summary>
[System.Serializable]
public class PlantDefinition
{
    public string id;                 // unique lowercase snake_case
    public string displayName;
    public string scientificName;
    public string family;
    public string descriptionShort;

    /// <summary>Growth timing and stage configuration.</summary>
    [System.Serializable]
    public class GrowthInfo
    {
        public int totalGrowthStages;
        public int daysPerStage;
        public bool regrowable;
    }
    public GrowthInfo growth;

    /// <summary>Soil, water, light, and temperature needs.</summary>
    [System.Serializable]
    public class EnvironmentRequirements
    {
        public List<string> idealSoilTypes;
        public int waterPerDay;
        public string sunlightLevel; // "FullSun", "PartialSun", "Shade"
        public int temperatureMin;
        public int temperatureMax;
    }
    public EnvironmentRequirements environmentRequirements;

    /// <summary>Shop and progression values.</summary>
    [System.Serializable]
    public class Economy
    {
        public int seedCost;
        public int sellValue;
        public int experienceReward;
    }
    public Economy economy;
}
