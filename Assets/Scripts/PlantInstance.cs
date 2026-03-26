using UnityEngine;

/// <summary>
/// Attached to a PlantPot prefab. Stores all runtime state for one pot.
/// </summary>
public class PlantInstance : MonoBehaviour
{
    // ── Plant identity ────────────────────────────────────────────────────────
    public string plantDefinitionId;   // matches PlantDefinition.id in plants.json
    public string customName;

    // ── Growth ────────────────────────────────────────────────────────────────
    public int    currentGrowthStage = 0;
    public float  waterLevel         = 0f;
    public float  health             = 100f;
    public PlantStatus status        = PlantStatus.Healthy;

    // ── Pot state ─────────────────────────────────────────────────────────────
    public string soilType = "";       // e.g. "Loamy", "Sandy", "Clay" — empty = no soil
    public bool   hasSoil  => !string.IsNullOrEmpty(soilType);
    public bool   hasPlant => !string.IsNullOrEmpty(plantDefinitionId);

    // ── Internal timer ────────────────────────────────────────────────────────
    [HideInInspector] public float timer = 0f;

    /// <summary>Remove the plant from this pot (after harvest).</summary>
    public void ClearPlant()
    {
        plantDefinitionId  = "";
        customName         = "";
        currentGrowthStage = 0;
        waterLevel         = 0f;
        health             = 100f;
        status             = PlantStatus.Healthy;
        timer              = 0f;
    }

    /// <summary>Remove soil (and plant) from this pot.</summary>
    public void ClearSoil()
    {
        ClearPlant();
        soilType = "";
    }
}

public enum PlantStatus
{
    Healthy,
    Thirsty,
    Growing,
    Wilting
}
