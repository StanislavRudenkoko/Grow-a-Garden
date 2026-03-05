using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlantDefinition
{
    public string id;                 // unique lowercase snake_case
    public string displayName;
    public string scientificName;
    public string family;
    public string descriptionShort;

    [System.Serializable]
    public class GrowthInfo
    {
        public int totalGrowthStages;
        public int daysPerStage;
        public bool regrowable;
    }
    public GrowthInfo growth;

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

    [System.Serializable]
    public class Economy
    {
        public int seedCost;
        public int sellValue;
        public int experienceReward;
    }
    public Economy economy;
}