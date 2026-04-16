/// <summary>
/// PlantVisualData
/// Author: Stanislav Rudenko
/// Date: Mar. 12 - Mar. 26, 2026
/// Source: with help of Claude AI
/// </summary>

using UnityEngine;

/// <summary>
/// ScriptableObject holding growth-stage sprites for one plant id (used by <see cref="PlantVisualDatabase"/>).
/// </summary>
[CreateAssetMenu(fileName = "PlantVisualData", menuName = "Plants/Plant Visual Data")]
public class PlantVisualData : ScriptableObject
{
    public string plantDefinitionId;  // must match the id in plants.json
    public Sprite[] growthStageSprites;
}
