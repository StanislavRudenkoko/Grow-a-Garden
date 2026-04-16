using UnityEngine;
using System.Collections.Generic;

public class PlantDatabaseManager : MonoBehaviour
{
    public static PlantDatabaseManager Instance;

    private Dictionary<string, PlantDefinition> plantDict;

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

    [System.Serializable]
    private class PlantsWrapper
    {
        public PlantDefinition[] plants;
    }
}
