using UnityEngine;

[System.Serializable]
public class PlantInstance : MonoBehaviour
{
    public string plantDefinitionId;
    public string customName;

    public int currentGrowthStage = 0;
    public float waterLevel = 0f;
    public float health = 100f;
    public PlantStatus status = PlantStatus.Healthy;

    public bool hasSoil = false;
    public bool hasSeed = false;

    [HideInInspector] public float timer = 0f; // used for growth timing
}

public enum PlantStatus
{
    Healthy,
    Thirsty,
    Growing,
    Wilting
}