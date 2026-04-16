using System;
using UnityEngine;

/// <summary>
/// Soil/seed matching, consuming items when used in the garden, and granting harvest produce.
/// </summary>
public static class GardenInventoryUtil
{
    public static bool IsFullyGrown(PlantInstance data, PlantDefinition def)
    {
        if (data == null || def?.growth == null) return false;
        return data.currentGrowthStage >= def.growth.totalGrowthStages - 1;
    }

    public static bool TryConsumeOneFromInventory(Player player, Item item)
    {
        if (player == null || item == null || item.QuantityPlayer <= 0) return false;
        item.QuantityPlayer--;
        if (item.QuantityPlayer == 0)
            player.Inventory.Remove(item);
        return true;
    }

    public static bool TryGrantProduceHarvest(Player player, PlantDefinition def)
    {
        if (player == null || def == null) return false;

        Item produceItem = FindStoreProduceItemForPlant(def);
        if (produceItem == null)
        {
            Debug.LogWarning("Harvest: could not find a produce Item in the store for plant '" + def.id + "'.");
            return false;
        }

        if (!player.Inventory.Contains(produceItem))
            player.Inventory.Add(produceItem);

        produceItem.QuantityPlayer++;
        player.PlantsHarvested++;
        return true;
    }

    public static Item FindStoreProduceItemForPlant(PlantDefinition def)
    {
        StoreInventory store = ObjectGetter.GetStoreInventory();
        if (store?.Items == null) return null;

        ItemInfo produceInfo = null;
        foreach (Item seedCandidate in store.Items)
        {
            if (seedCandidate == null || seedCandidate.ItemCategory != ItemCategory.SEED) continue;
            if (!SeedItemMatchesPlant(seedCandidate, def)) continue;
            produceInfo = seedCandidate.Produce;
            break;
        }

        if (produceInfo == null) return null;

        foreach (Item item in store.Items)
        {
            if (item != null && item.ItemCategory == ItemCategory.PRODUCE && item.ItemInfo == produceInfo)
                return item;
        }

        return null;
    }

    public static Item FindPlayerSoilItem(Player player, string soilTypeId)
    {
        if (player?.Inventory == null) return null;
        foreach (Item item in player.Inventory)
        {
            if (item != null && item.ItemCategory == ItemCategory.SOIL && item.QuantityPlayer > 0 &&
                SoilLabelsMatch(item.Name, soilTypeId))
                return item;
        }
        return null;
    }

    public static Item FindPlayerSeedItem(Player player, PlantDefinition def)
    {
        if (player?.Inventory == null) return null;
        foreach (Item item in player.Inventory)
        {
            if (item != null && item.ItemCategory == ItemCategory.SEED && item.QuantityPlayer > 0 &&
                SeedItemMatchesPlant(item, def))
                return item;
        }
        return null;
    }

    public static Item FindPlayerFertilizerItem(Player player)
    {
        if (player?.Inventory == null) return null;
        foreach (Item item in player.Inventory)
        {
            if (item != null && item.ItemCategory == ItemCategory.FERTILIZER && item.QuantityPlayer > 0)
                return item;
        }
        return null;
    }

    public static void ClearDeadPlantIfAny(PlantPotController pot)
    {
        if (pot == null) return;
        PlantInstance data = pot.potData;
        if (data == null || !data.hasPlant || data.health > 0f) return;

        data.ClearPlant();
        pot.RefreshVisuals();
        PlantHoverHandler hover = pot.GetComponent<PlantHoverHandler>();
        if (hover != null)
            hover.Initialize(pot.potData);
    }

    // ── Soil labels (shop name vs gameplay soil id) ───────────────────────

    public static bool SoilLabelsMatch(string inventoryItemName, string soilTypeId) =>
        NormalizeSoilLabel(inventoryItemName) == NormalizeSoilLabel(soilTypeId);

    private static string NormalizeSoilLabel(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return "";
        string t = s.Trim().ToLowerInvariant().Replace('-', ' ');
        while (t.Contains("  "))
            t = t.Replace("  ", " ");
        return t;
    }

    // ── Seed item vs plant definition (same rules as SeedPickerUI) ───────────

    public static bool SeedItemMatchesPlant(Item item, PlantDefinition def)
    {
        if (def == null || string.IsNullOrEmpty(def.id)) return false;

        string id = def.id.Trim().ToLowerInvariant();
        string rawName = item.Name?.Trim() ?? "";

        if (string.Equals(rawName, def.id, StringComparison.OrdinalIgnoreCase))
            return true;

        string stripped = StripSeedSuffixFromItemName(rawName);
        string key = ToSnakeKey(stripped);

        if (string.IsNullOrEmpty(key)) return false;

        if (key == id) return true;
        if (id == key + "s") return true;
        if (key.StartsWith(id + "_", StringComparison.Ordinal)) return true;

        return false;
    }

    private static string StripSeedSuffixFromItemName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return "";

        string t = name.Trim();
        if (t.EndsWith(" seeds", StringComparison.OrdinalIgnoreCase))
            return t.Substring(0, t.Length - " seeds".Length).TrimEnd();
        if (t.EndsWith(" seed", StringComparison.OrdinalIgnoreCase))
            return t.Substring(0, t.Length - " seed".Length).TrimEnd();
        return t;
    }

    private static string ToSnakeKey(string spaced)
    {
        if (string.IsNullOrWhiteSpace(spaced)) return "";

        string[] parts = spaced.Trim()
            .ToLowerInvariant()
            .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

        return string.Join("_", parts);
    }
}
