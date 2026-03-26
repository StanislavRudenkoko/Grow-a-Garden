using UnityEngine;

[CreateAssetMenu(fileName = "PlantVisualData", menuName = "Plants/Plant Visual Data")]
public class PlantVisualData : ScriptableObject
{
    public string plantDefinitionId;  // must match the id in plants.json
    public Sprite[] growthStageSprites;
}