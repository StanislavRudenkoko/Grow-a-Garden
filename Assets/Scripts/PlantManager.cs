/// <summary>
/// PlantManager
/// Author: Stanislav Rudenko, Tin Trinh
/// Date: Mar. 12 - Mar. 26, 2026
/// Source: with help of Claude AI
/// </summary>

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages all pots in the scene:
///   • Spawning / removing pots
///   • The growth update loop
///   • Soil type registry
///   • Plant compatibility queries
/// </summary>
public class PlantManager : MonoBehaviour
{
    public static PlantManager Instance;

    // ── Inspector ─────────────────────────────────────────────────────────────
    [Header("Visuals")]
    public PlantVisualDatabase visualDatabase;

    [Header("Prefab & Layout")]
    public GameObject plantPotPrefab;   // the PlantPot prefab
    public Transform potParent;        // empty GameObject that holds all pots
    public Transform[] potSlots;        // 12 pre-placed slot transforms in the scene

    [Header("Growth")]
    public float growthTimePerStage = 10f;  // seconds per growth stage

    [Header("Soil Types")]
    public List<string> soilTypes = new List<string> { "All purpose soil", "Premium soil" };

    // ── Runtime ───────────────────────────────────────────────────────────────

    private readonly List<PlantPotController> activePots = new List<PlantPotController>();

    // ── Unity lifecycle ───────────────────────────────────────────────────────

    private void Awake()
    {
        Debug.Log("PlantManager Awake called. Instance is null: " + (Instance == null));
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("This instance is now the singleton");
        }
        else
        {
            Debug.Log("Duplicate found! Destroying this one. Existing instance: " + Instance.gameObject.name);
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        foreach (PlantPotController pot in activePots)
        {
            PlantInstance data = pot.potData;

            if (!data.hasPlant) continue;

            PlantDefinition def = PlantDatabaseManager.Instance
                .GetPlantDefinition(data.plantDefinitionId);
            if (def == null) continue;

            int maxStage = def.growth.totalGrowthStages - 1;
            if (data.currentGrowthStage >= maxStage) continue;

            // Start growing if watered
            if (data.status == PlantStatus.Healthy && data.waterLevel > 0)
                data.status = PlantStatus.Growing;

            if (data.status == PlantStatus.Growing)
            {
                data.timer += Time.deltaTime;
                if (data.timer >= growthTimePerStage)
                {
                    data.timer = 0f;
                    data.waterLevel = 0f;
                    data.status = PlantStatus.Healthy;
                    data.currentGrowthStage = Mathf.Min(data.currentGrowthStage + 1, maxStage);
                    pot.RefreshVisuals();
                }
            }
        }
    }

    // ── Public API ────────────────────────────────────────────────────────────

    /// <summary>
    /// Called by the "Add Pot" button. Finds the first empty slot and spawns a pot.
    /// </summary>
    public void AddPot()
    {
        Debug.Log("AddPot called");

        if (plantPotPrefab == null)
        {
            Debug.LogError("plantPotPrefab is NULL");
            return;
        }

        if (potSlots == null || potSlots.Length == 0)
        {
            Debug.LogError("potSlots is empty!");
            return;
        }

        Debug.Log("Slot count: " + potSlots.Length);

        foreach (Transform slot in potSlots)
        {
            if (slot == null)
            {
                Debug.LogWarning("A slot entry is null, skipping");
                continue;
            }

            bool occupied = activePots.Exists(p => p.transform.position == slot.position);

            if (!occupied)
            {
                Debug.Log("Spawning pot at: " + slot.position);
                SpawnPot(slot.position, slot.GetSiblingIndex());
                return;
            }
        }

        Debug.Log("All slots full or no valid slot found");
    }

    /// <summary>Removes the pot from tracking and destroys its GameObject.</summary>
    public void RemovePot(PlantPotController pot)
    {
        activePots.Remove(pot);
        Destroy(pot.gameObject);
    }

    /// <summary>Returns the index of a soil name in soilTypes (for sprite lookup).</summary>
    public int GetSoilIndex(string soilName)
    {
        return soilTypes.IndexOf(soilName);
    }

    /// <summary>
    /// Returns all PlantDefinitions whose idealSoilTypes include the given soil.
    /// In a real game you would also filter by the player's seed inventory.
    /// </summary>
    public List<PlantDefinition> GetCompatiblePlants(string soilType)
    {
        List<PlantDefinition> result = new List<PlantDefinition>();

        // PlantDatabaseManager holds all definitions loaded from plants.json
        // We iterate by asking it for each known id — extend this if you add
        // a public GetAll() method to PlantDatabaseManager.
        foreach (string id in PlantDatabaseManager.Instance.GetAllIds())
        {
            PlantDefinition def = PlantDatabaseManager.Instance.GetPlantDefinition(id);
            if (def != null &&
                def.environmentRequirements != null &&
                def.environmentRequirements.idealSoilTypes.Contains(soilType))
            {
                result.Add(def);
            }
        }
        return result;
    }

    // ── Private ───────────────────────────────────────────────────────────────

    private void SpawnPot(Vector3 position, int index)
    {
        GameObject go = Instantiate(plantPotPrefab, position, Quaternion.identity, potParent);

        // sets the plant + front of the pot in front of the previous pot
        int plantIndex;
        int potIndex;
        if (index < 4)
        {
            plantIndex = 2;
            potIndex = 3;
        }
        else if (index < 8)
        {
            plantIndex = 4;
            potIndex = 5;
        }
        else
        {
            plantIndex = 6;
            potIndex = 7;
        }
        go.transform.GetChild(2).GetComponent<SpriteRenderer>().sortingOrder = plantIndex;
        go.transform.GetChild(3).GetComponent<SpriteRenderer>().sortingOrder = potIndex;


        PlantPotController ctrl = go.GetComponent<PlantPotController>();
        activePots.Add(ctrl);
    }

    private void OnDestroy()
    {
        Debug.Log("PlantManager was DESTROYED");
    }

    private void OnDisable()
    {
        Debug.Log("PlantManager was DISABLED");
    }




}
