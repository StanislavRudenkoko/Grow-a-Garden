/// <summary>
/// SoilPickerUI
/// Author: Stanislav Rudenko
/// Date: Mar. 12 - Mar. 26, 2026
/// Source: with help of Claude AI
/// </summary>
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// A side panel that lists soil types the player actually has in inventory.
/// Appears when the player picks "Add Soil" from the dropdown.
/// </summary>
public class SoilPickerUI : MonoBehaviour
{
    [Header("References")]
    public GameObject buttonPrefab;

    private PlantPotController targetPot;
    private PotDropdownUI ownerDropdown;
    public static SoilPickerUI Instance;

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

        // ── Inventory filter ──────────────────────────────────────────────────
        var inventory = ObjectGetter.GetPlayer()?.Inventory ?? new System.Collections.Generic.List<Item>();

        bool anyShown = false;

        foreach (string soil in PlantManager.Instance.soilTypes)
        {
            string captured = soil;

            // Shop ItemInfo names often differ in casing/hyphens from gameplay soil ids
            // (e.g. "All-purpose Soil" vs "All purpose soil") — compare normalized labels.
            bool inInventory = inventory.Any(item =>
                item != null &&
                item.ItemCategory == ItemCategory.SOIL &&
                item.QuantityPlayer > 0 &&
                GardenInventoryUtil.SoilLabelsMatch(item.Name, captured));

            GameObject go = Instantiate(buttonPrefab, transform);
            var tmp = go.GetComponentInChildren<TMP_Text>();
            var btn = go.GetComponent<Button>();

            if (inInventory)
            {
                tmp.text = soil;
                btn.interactable = true;
                btn.onClick.AddListener(() => OnSoilSelected(captured));
                anyShown = true;
            }
            else
            {
                // Show greyed-out entry so the player knows it exists but isn't owned
                tmp.text = soil + " (not owned)";
                btn.interactable = false;
            }
        }

        // If every soil type is missing, add a clear message at the top
        if (!anyShown)
        {
            GameObject msg = Instantiate(buttonPrefab, transform);
            msg.GetComponentInChildren<TMP_Text>().text = "No soil in inventory";
            msg.GetComponent<Button>().interactable = false;
            msg.transform.SetAsFirstSibling();
        }

        transform.position = ownerDropdown.transform.position + new Vector3(200, 0, 0);
        gameObject.SetActive(true);
    }

    private void OnSoilSelected(string soilType)
    {
        Player player = ObjectGetter.GetPlayer();
        Item soilItem = GardenInventoryUtil.FindPlayerSoilItem(player, soilType);
        if (!GardenInventoryUtil.TryConsumeOneFromInventory(player, soilItem))
        {
            Debug.LogWarning("SoilPickerUI: could not consume soil from inventory.");
            return;
        }

        targetPot.potData.soilType = soilType;
        targetPot.RefreshVisuals();
        gameObject.SetActive(false);
        ownerDropdown.Close();
    }
}