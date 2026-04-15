/// <summary>
/// SeedPickerUI
/// Author: Stanislav Rudenko
/// Date: Mar. 12 - Mar. 26, 2026
/// Source: with help of Claude AI
/// </summary>

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// A side panel that lists seeds the player can plant in this pot,
/// filtered by soil compatibility.
///
/// HOW TO SET UP IN UNITY:
///   Same as SoilPickerUI.
/// </summary>
public class SeedPickerUI : MonoBehaviour
{
    [Header("References")]
    public GameObject buttonPrefab;

    private PlantPotController targetPot;
    private PotDropdownUI ownerDropdown;

    public static SeedPickerUI Instance;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    /// <summary>Builds seed buttons for soil-compatible plants and shows beside the dropdown.</summary>
    public void OpenFor(PlantPotController pot, PotDropdownUI dropdown)
    {
        targetPot = pot;
        ownerDropdown = dropdown;

        // Clear old buttons
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        // Get seeds compatible with this soil type
        List<PlantDefinition> compatible = PlantManager.Instance
            .GetCompatiblePlants(pot.potData.soilType);

        if (compatible.Count == 0)
        {
            // Show a disabled "none available" label
            GameObject go = Instantiate(buttonPrefab, transform);
            go.GetComponentInChildren<TMP_Text>().text = "No seeds available";
            go.GetComponent<Button>().interactable = false;
        }
        else
        {
            foreach (PlantDefinition def in compatible)
            {
                PlantDefinition captured = def;
                GameObject go = Instantiate(buttonPrefab, transform);
                go.GetComponentInChildren<TMP_Text>().text = def.displayName;
                go.GetComponent<Button>().onClick.AddListener(() => OnSeedSelected(captured));
            }
        }

        transform.position = ownerDropdown.transform.position + new Vector3(200, 0, 0);
        gameObject.SetActive(true);
    }

    private void OnSeedSelected(PlantDefinition def)
    {
        Debug.Log("Seed selected: " + def.id);
        PlantInstance data = targetPot.potData;
        data.plantDefinitionId = def.id;
        data.customName = def.displayName;
        data.currentGrowthStage = 0;
        data.waterLevel = 0f;
        data.health = 100f;
        data.status = PlantStatus.Healthy;
        data.daysToGrow = def.growth.daysPerStage;
        targetPot.RefreshVisuals();

        // Re-initialize hover handler so tooltip works
        targetPot.GetComponent<PlantHoverHandler>().Initialize(data);

        gameObject.SetActive(false);
        ownerDropdown.Close();
    }
}
