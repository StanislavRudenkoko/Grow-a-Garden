using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// A single context-menu panel that dynamically builds buttons
/// based on the state of the clicked pot.
///
/// HOW TO SET UP IN UNITY:
///   1. Create a Canvas child panel (e.g. "PotDropdownUI").
///   2. Give it a VerticalLayoutGroup so buttons stack neatly.
///   3. Create a Button prefab with a TMP_Text child — assign to buttonPrefab.
///   4. Attach this script to the panel.
///   5. Set this panel inactive by default.
/// </summary>
public class PotDropdownUI : MonoBehaviour
{
    public static PotDropdownUI Instance;

    [Header("References")]
    public GameObject      buttonPrefab;   // a Button prefab with TMP_Text child
    public SoilPickerUI    soilPicker;
    public SeedPickerUI    seedPicker;

    private PlantPotController currentPot;
    private readonly List<GameObject> spawnedButtons = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    // ── Public API ────────────────────────────────────────────────────────────

    public void OpenFor(PlantPotController pot)
    {
        // Close sub-panels
        soilPicker.gameObject.SetActive(false);
        seedPicker.gameObject.SetActive(false);

        currentPot = pot;
        BuildButtons();

        // Position the dropdown near the pot in screen space
        Vector3 screenPos = Camera.main.WorldToScreenPoint(pot.transform.position);
        transform.position = screenPos + new Vector3(120, 0, 0);

        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        soilPicker.gameObject.SetActive(false);
        seedPicker.gameObject.SetActive(false);
    }

    // ── Private helpers ───────────────────────────────────────────────────────

    private void BuildButtons()
    {
        // Clear old buttons
        foreach (var b in spawnedButtons) Destroy(b);
        spawnedButtons.Clear();

        PlantInstance data = currentPot.potData;

        if (!data.hasSoil)
        {
            AddButton("Add Soil",    OnAddSoil);
            AddButton("Remove Pot",  OnRemovePot);
        }
        else if (!data.hasPlant)
        {
            AddButton("Add Plant",   OnAddPlant);
            AddButton("Remove Soil", OnRemoveSoil);
            AddButton("Remove Pot",  OnRemovePot);
        }
        else
        {
            AddButton("Harvest",        OnHarvest);
            AddButton("Add Fertilizer", OnAddFertilizer);
            AddButton("Remove Plant",   OnRemovePlant);
        }
    }

    private void AddButton(string label, UnityEngine.Events.UnityAction action)
    {
        GameObject go = Instantiate(buttonPrefab, transform);
        go.GetComponentInChildren<TMP_Text>().text = label;
        go.GetComponent<Button>().onClick.AddListener(action);
        spawnedButtons.Add(go);
    }

    // ── Button callbacks ──────────────────────────────────────────────────────

    private void OnAddSoil()
    {
        // Open the soil sub-panel beside this dropdown
        soilPicker.OpenFor(currentPot, this);
    }

    private void OnAddPlant()
    {
        // Open the seed sub-panel beside this dropdown
        seedPicker.OpenFor(currentPot, this);
    }

    private void OnRemoveSoil()
    {
        currentPot.potData.ClearSoil();
        currentPot.RefreshVisuals();
        Close();
    }

    private void OnRemovePot()
    {
        PlantManager.Instance.RemovePot(currentPot);
        Close();
    }

    private void OnHarvest()
    {
        currentPot.potData.ClearPlant();
        currentPot.RefreshVisuals();
        Close();
    }

    private void OnAddFertilizer()
    {
        // TODO: implement fertilizer logic
        Debug.Log("Fertilizer not yet implemented.");
        Close();
    }

    private void OnRemovePlant()
    {
        currentPot.potData.ClearPlant();
        currentPot.RefreshVisuals();
        Close();
    }

    // Close the dropdown if the player clicks anywhere else
    private void Update()
    {
        if (gameObject.activeSelf && Input.GetMouseButtonDown(0))
        {
            // Check if the click is outside this panel
            if (!RectTransformUtility.RectangleContainsScreenPoint(
                    GetComponent<RectTransform>(), Input.mousePosition))
            {
                Close();
            }
        }
    }
}
