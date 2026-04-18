/// <summary>
/// PlantDatabaseManager
/// Author: Stanislav Rudenko
/// Date: Apr. 17, 2026
/// Source: with help of Claude AI
/// </summary>

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Loads plant definitions from <c>Resources/plants.json</c> and exposes lookups by id.
/// Persists as a singleton across scenes.
/// </summary>
public class PlantDatabaseManager : MonoBehaviour
{
    /// <summary>Singleton instance; created on first load and not destroyed on scene change.</summary>
    public static PlantDatabaseManager Instance;

    private Dictionary<string, PlantDefinition> plantDict;

    /// <summary>Initializes the singleton, loads the plant dictionary, or destroys duplicate instances.</summary>
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadPlants();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>Parses <c>plants.json</c> and fills <see cref="plantDict"/> keyed by <see cref="PlantDefinition.id"/>.</summary>
    private void LoadPlants()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("plants"); // plants.json in Resources/
        if (jsonFile == null)
        {
            Debug.LogError("plants.json not found in Resources!");
            return;
        }

        PlantsWrapper wrapper = JsonUtility.FromJson<PlantsWrapper>(jsonFile.text);
        plantDict = new Dictionary<string, PlantDefinition>();
        foreach (var plant in wrapper.plants)
        {
            plantDict[plant.id] = plant;
        }
    }

    /// <summary>Returns the plant definition for <paramref name="id"/>, or <c>null</c> if missing.</summary>
    public PlantDefinition GetPlantDefinition(string id)
    {
        if (plantDict != null && plantDict.TryGetValue(id, out PlantDefinition def))
            return def;

        Debug.LogWarning($"PlantDefinition '{id}' not found!");
        return null;
    }

    /// <summary>Returns all plant IDs in the database.</summary>
    public IEnumerable<string> GetAllIds()
    {
        if (plantDict == null) yield break;
        foreach (string key in plantDict.Keys)
            yield return key;
    }

    /// <summary>JSON root object matching the shape of <c>plants.json</c>.</summary>
    [System.Serializable]
    private class PlantsWrapper
    {
        public PlantDefinition[] plants;
    }
}
