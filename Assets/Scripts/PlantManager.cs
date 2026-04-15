/// <summary>
/// PlantManager
/// Author: Stanislav Rudenko, Joshua Trepanier, Tin Trinh
/// Date: Mar. 12 - Mar. 26, 2026
/// Source: with help of Claude AI
/// </summary>

using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// Manages all pots in the scene:
///   • Spawning / removing pots
///   • The day-tick growth loop
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
    public GameObject plantPotPrefab;
    public Transform potParent;
    public Transform[] potSlots;

    [Header("Soil Types")]
    public List<string> soilTypes = new List<string> { "All purpose soil", "Premium soil" };

    // ── Runtime ───────────────────────────────────────────────────────────────
    private List<PlantPotController> activePots;

    // ── Constants ─────────────────────────────────────────────────────────────
    private const float HealthDrainDry = 15f;
    private const float HealthDrainOverwater = 8f;
    private const float FertilizerHealAmount = 10f;
    private const float FertilizerCostAmount = 20f;
    private const float WaterOverflowThreshold = 200f;
    private const float WaterThirstyThreshold = 20f;
    private const float HealthWiltThreshold = 65f;

    // ── Unity lifecycle ───────────────────────────────────────────────────────
    /// <summary>
    /// on scene load
    /// </summary>
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

    /// <summary>
    /// On scene start
    /// </summary>
    private void Start()
    {
        activePots = ObjectGetter.getPots();
        if (activePots == null)
        {
            activePots = new List<PlantPotController>();
        }
        else
        {
            foreach (PlantPotController pot in activePots)
            {
                pot.gameObject.SetActive(true);
            }
        }
    }


    // ── Day tick ──────────────────────────────────────────────────────────────
    /// <summary>
    /// Call this from your "Next Day" button. Advances all plant simulations by one day.
    /// </summary>
    public void ProcessNextDay()
    {
        ObjectGetter.setPots(activePots);
        foreach (PlantPotController pot in activePots)
        {
            PlantInstance data = pot.potData;

            if (!data.hasPlant) continue;

            PlantDefinition def = PlantDatabaseManager.Instance
                .GetPlantDefinition(data.plantDefinitionId);
            if (def == null) continue;

            // ── 1. Water drain ────────────────────────────────────────────────
            data.waterLevel = Mathf.Max(0f, data.waterLevel - def.environmentRequirements.waterLevels);

            // ── 2. Health / fertilizer ────────────────────────────────────────
            if (data.waterLevel == 0f)
            {
                // Dry — lose health, fertilizer does nothing
                data.health = Mathf.Max(0f, data.health - HealthDrainDry);
            }
            else if (data.waterLevel > WaterOverflowThreshold)
            {
                // Overwatered — lose health, fertilizer does nothing
                data.health = Mathf.Max(0f, data.health - HealthDrainOverwater);
            }
            else if (data.fertilizer > 0f && data.health < 100f)
            {
                // Good water + fertilizer present — heal and consume fertilizer
                data.health = Mathf.Min(100f, data.health + FertilizerHealAmount);
                data.fertilizer = Mathf.Max(0f, data.fertilizer - FertilizerCostAmount);
            }

            // ── 3. Status derivation ──────────────────────────────────────────
            DeriveStatus(data);

            // ── 4. Growth ─────────────────────────────────────────────────────
            int maxStage = def.growth.totalGrowthStages - 1;

            if (data.status == PlantStatus.Healthy && data.currentGrowthStage < maxStage)
            {
                data.daysToGrow--;

                if (data.daysToGrow <= 0)
                {
                    data.currentGrowthStage++;
                    pot.RefreshVisuals();

                    // Reset counter only if there are more stages left
                    if (data.currentGrowthStage < maxStage)
                        data.daysToGrow = def.growth.daysPerStage;
                }
            }
        }
    }

    /// <summary>
    /// Derives and assigns plant status from current health and water values.
    /// Call this after any interaction that modifies health or waterLevel.
    /// </summary>
    public static void DeriveStatus(PlantInstance data)
    {
        if (data.health < HealthWiltThreshold)
            data.status = PlantStatus.Wilting;
        else if (data.waterLevel > WaterOverflowThreshold)
            data.status = PlantStatus.Overwatered;
        else if (data.waterLevel < WaterThirstyThreshold)
            data.status = PlantStatus.Thirsty;
        else
            data.status = PlantStatus.Healthy;

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

    /// <summary>
    /// Removes the pot from tracking and destroys its GameObject.
    /// </summary>
    public void RemovePot(PlantPotController pot)
    {
        activePots.Remove(pot);
        Destroy(pot.gameObject);
    }

    /// <summary>
    /// Returns the index of a soil name in soilTypes (for sprite lookup).
    /// </summary>
    public int GetSoilIndex(string soilName)
    {
        return soilTypes.IndexOf(soilName);
    }

    /// <summary>
    /// Returns all PlantDefinitions whose idealSoilTypes include the given soil.
    /// </summary>
    public List<PlantDefinition> GetCompatiblePlants(string soilType)
    {
        List<PlantDefinition> result = new List<PlantDefinition>();

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

    /// <summary>
    /// Spawn a pot in the scene and convert it from a child to a parent to allow
    /// the pot to be set as DontDestroyOnLoad.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="index"></param>
    private void SpawnPot(Vector3 position, int index)
    {
        GameObject go = Instantiate(plantPotPrefab, position, Quaternion.identity, potParent);

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

        go.transform.SetParent(null);
        DontDestroyOnLoad(go);
        activePots.Add(ctrl);
    }

    /// <summary>
    /// Run when PlantManager is destroyed
    /// </summary>
    private void OnDestroy()
    {
        Debug.Log("PlantManager was DESTROYED");
        foreach (PlantPotController pot in activePots)
        {
            pot.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Run when PlantManager gets disabled
    /// </summary>
    private void OnDisable()
    {
        Debug.Log("PlantManager was DISABLED");
    }
}