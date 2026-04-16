/// <summary>
/// SeedPickerUI
/// Author: Stanislav Rudenko
/// Date: Mar. 12 - Mar. 26, 2026
/// Source: with help of Claude AI
/// </summary>
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// A side panel that lists seeds the player can plant in this pot,
/// filtered by BOTH soil compatibility AND inventory ownership.
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

    public void OpenFor(PlantPotController pot, PotDropdownUI dropdown)
    {
        targetPot = pot;
        ownerDropdown = dropdown;

        foreach (Transform child in transform)
            Destroy(child.gameObject);

        // ── Soil-compatible seeds ─────────────────────────────────────────────
        List<PlantDefinition> compatible =
            PlantManager.Instance.GetCompatiblePlants(pot.potData.soilType);

        // ── Inventory filter ──────────────────────────────────────────────────
        var inventory = ObjectGetter.GetPlayer()?.Inventory ?? new List<Item>();

        bool anyOwned = false;

        if (compatible.Count == 0)
        {
            // No seeds work with this soil at all — soil-side message
            GameObject go = Instantiate(buttonPrefab, transform);
            go.GetComponentInChildren<TMP_Text>().text = "No seeds for this soil";
            go.GetComponent<Button>().interactable = false;
        }
        else
        {
            foreach (PlantDefinition def in compatible)
            {
                PlantDefinition captured = def;

                // Shop seed ItemInfo names are usually human-readable (e.g. "Tomato Seeds") while
                // plant data uses ids (e.g. "tomato") — match via normalized keys, not raw equality.
                bool inInventory = inventory.Any(item =>
                    item != null &&
                    item.ItemCategory == ItemCategory.SEED &&
                    item.QuantityPlayer > 0 &&
                    GardenInventoryUtil.SeedItemMatchesPlant(item, captured));

                GameObject go = Instantiate(buttonPrefab, transform);
                var tmp = go.GetComponentInChildren<TMP_Text>();
                var btn = go.GetComponent<Button>();

                if (inInventory)
                {
                    tmp.text = def.displayName;
                    btn.interactable = true;
                    btn.onClick.AddListener(() => OnSeedSelected(captured));
                    anyOwned = true;
                }
                else
                {
                    // Compatible with soil but not in inventory — show greyed out
                    tmp.text = def.displayName + " (not owned)";
                    btn.interactable = false;
                }
            }

            // If every compatible seed is missing from inventory, add a header message
            if (!anyOwned)
            {
                GameObject msg = Instantiate(buttonPrefab, transform);
                msg.GetComponentInChildren<TMP_Text>().text = "No seeds in inventory";
                msg.GetComponent<Button>().interactable = false;
                msg.transform.SetAsFirstSibling();
            }
        }

        transform.position = ownerDropdown.transform.position + new Vector3(200, 0, 0);
        gameObject.SetActive(true);
    }

    private void OnSeedSelected(PlantDefinition def)
    {
        Debug.Log("Seed selected: " + def.id);

        Player player = ObjectGetter.GetPlayer();
        Item seedItem = GardenInventoryUtil.FindPlayerSeedItem(player, def);
        if (!GardenInventoryUtil.TryConsumeOneFromInventory(player, seedItem))
        {
            Debug.LogWarning("SeedPickerUI: could not consume seed from inventory.");
            return;
        }

        PlantInstance data = targetPot.potData;
        data.plantDefinitionId = def.id;
        data.customName = def.displayName;
        data.currentGrowthStage = 0;
        data.waterLevel = 0f;
        data.health = 100f;
        data.status = PlantStatus.Healthy;
        data.daysToGrow = def.growth.daysPerStage;

        targetPot.RefreshVisuals();
        targetPot.GetComponent<PlantHoverHandler>().Initialize(data);

        gameObject.SetActive(false);
        ownerDropdown.Close();
    }
}