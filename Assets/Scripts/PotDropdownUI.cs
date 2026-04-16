using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PotDropdownUI : MonoBehaviour
{
    public static PotDropdownUI Instance;

    [Header("References")]
    public GameObject buttonPrefab;
    public SoilPickerUI soilPicker;
    public SeedPickerUI seedPicker;

    private PlantPotController currentPot;
    private readonly List<GameObject> spawnedButtons = new List<GameObject>();

    private void Awake()
    {
        Debug.Log("PotDropdownUI Awake called");
        Instance = this;
        gameObject.SetActive(false);
    }

    // ── Public API ────────────────────────────────────────────────────────────

    public void OpenFor(PlantPotController pot)
    {
        soilPicker.gameObject.SetActive(false);
        seedPicker.gameObject.SetActive(false);

        currentPot = pot;
        BuildButtons();

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

    private List<Item> GetInventoryByCategory(System.Func<Item, bool> predicate)
    {
        var player = ObjectGetter.GetPlayer();
        if (player == null) return new List<Item>();

        var result = new List<Item>();
        foreach (var item in player.Inventory)
        {
            if (item != null && item.QuantityPlayer > 0 && predicate(item))
                result.Add(item);
        }
        return result;
    }

    private bool HasSoilInInventory() => GetInventoryByCategory(i => i.ItemCategory == ItemCategory.SOIL).Count > 0;
    private bool HasSeedInInventory() => GetInventoryByCategory(i => i.ItemCategory == ItemCategory.SEED).Count > 0;
    private bool HasWateringCan()
    {
        var player = ObjectGetter.GetPlayer();
        return PlayerEquipmentInventory.HasWateringCan(player?.Inventory);
    }
    private bool HasFertilizer() => GetInventoryByCategory(i => i.ItemCategory == ItemCategory.FERTILIZER).Count > 0;

    private void BuildButtons()
    {
        foreach (var b in spawnedButtons) Destroy(b);
        spawnedButtons.Clear();

        PlantInstance data = currentPot.potData;

        GardenInventoryUtil.ClearDeadPlantIfAny(currentPot);
        data = currentPot.potData;

        if (!data.hasSoil)
        {
            if (HasSoilInInventory())
                AddButton("Add Soil", OnAddSoil);
            else
                AddDisabledButton("No soil in inventory");

            AddButton("Remove Pot", OnRemovePot);
        }
        else if (!data.hasPlant)
        {
            if (HasSeedInInventory())
                AddButton("Add Plant", OnAddPlant);
            else
                AddDisabledButton("No seeds in inventory");

            AddButton("Remove Soil", OnRemoveSoil);
            AddButton("Remove Pot", OnRemovePot);
        }
        else
        {
            PlantDefinition def = PlantDatabaseManager.Instance
                   .GetPlantDefinition(data.plantDefinitionId);

            bool isFullyGrown = def != null &&
                data.currentGrowthStage >= def.growth.totalGrowthStages - 1;

            if (isFullyGrown)
                AddButton("Harvest", OnHarvest);
            else
                AddDisabledButton("Not ready to harvest");

            if (HasFertilizer())
                AddButton("Add Fertilizer", OnAddFertilizer);
            else
                AddDisabledButton("No fertilizer in inventory");

            if (HasWateringCan())
                AddButton("Water", OnWater);
            else
                AddDisabledButton("No watering can");

            AddButton("Remove Plant", OnRemovePlant);
        }
    }

    private void AddButton(string label, UnityEngine.Events.UnityAction action)
    {
        GameObject go = Instantiate(buttonPrefab, transform);
        go.GetComponentInChildren<TMP_Text>().text = label;
        go.GetComponent<Button>().onClick.AddListener(action);
        spawnedButtons.Add(go);
    }

    private void AddDisabledButton(string label)
    {
        GameObject go = Instantiate(buttonPrefab, transform);
        go.GetComponentInChildren<TMP_Text>().text = label;

        Button btn = go.GetComponent<Button>();
        btn.interactable = false;

        spawnedButtons.Add(go);
    }

    // ── Button callbacks ──────────────────────────────────────────────────────

    private void OnAddSoil()
    {
        soilPicker.OpenFor(currentPot, this);
    }

    private void OnAddPlant()
    {
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
        PlantInstance data = currentPot.potData;
        PlantDefinition def = PlantDatabaseManager.Instance.GetPlantDefinition(data.plantDefinitionId);
        if (!GardenInventoryUtil.IsFullyGrown(data, def))
            return;

        Player player = ObjectGetter.GetPlayer();
        if (!GardenInventoryUtil.TryGrantProduceHarvest(player, def))
            Debug.LogWarning("Harvest: produce was not added (see console).");

        data.ClearPlant();
        currentPot.RefreshVisuals();
        currentPot.GetComponent<PlantHoverHandler>()?.Initialize(data);
        Close();
    }

    private void OnWater()
    {
        PlantInstance instance = currentPot.potData;
        if (instance == null) return;

        instance.waterLevel += 100f;
        PlantManager.DeriveStatus(instance);
        if (instance.status == PlantStatus.Thirsty)
            instance.status = PlantStatus.Healthy;

        currentPot.RefreshVisuals();
        Close();
    }

    private void OnAddFertilizer()
    {
        PlantInstance instance = currentPot.potData;
        if (instance == null) return;

        Player player = ObjectGetter.GetPlayer();
        Item fert = GardenInventoryUtil.FindPlayerFertilizerItem(player);
        if (!GardenInventoryUtil.TryConsumeOneFromInventory(player, fert))
            return;

        instance.fertilizer = Mathf.Min(instance.health + 100f, 100f);

        currentPot.RefreshVisuals();
        Close();
    }

    private void OnRemovePlant()
    {
        currentPot.potData.ClearPlant();
        currentPot.RefreshVisuals();
        Close();
    }

    private void Update()
    {
        if (gameObject.activeSelf && Input.GetMouseButtonDown(0))
        {
            bool insideDropdown = RectTransformUtility.RectangleContainsScreenPoint(
                GetComponent<RectTransform>(), Input.mousePosition);

            bool insideSoilPicker = soilPicker.gameObject.activeSelf &&
                RectTransformUtility.RectangleContainsScreenPoint(
                    soilPicker.GetComponent<RectTransform>(), Input.mousePosition);

            bool insideSeedPicker = seedPicker.gameObject.activeSelf &&
                RectTransformUtility.RectangleContainsScreenPoint(
                    seedPicker.GetComponent<RectTransform>(), Input.mousePosition);

            if (!insideDropdown && !insideSoilPicker && !insideSeedPicker)
                Close();
        }
    }
}