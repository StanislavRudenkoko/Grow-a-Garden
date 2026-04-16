using System;
using System.Collections.Generic;

/// <summary>
/// Detects owned equipment using shop display names and legacy ScriptableObject-style names.
/// </summary>
public static class PlayerEquipmentInventory
{
    public static bool HasWateringCan(IList<Item> inventory) =>
        HasEquipmentMatchingAny(inventory, "Watering Can", "EquipmentWateringCan");

    public static bool HasTrowel(IList<Item> inventory) =>
        HasEquipmentMatchingAny(inventory, "Trowel", "EquipmentTrowel");

    private static bool HasEquipmentMatchingAny(IList<Item> inventory, params string[] acceptedNames)
    {
        if (inventory == null) return false;

        foreach (Item item in inventory)
        {
            if (item == null || item.ItemCategory != ItemCategory.EQUIPMENT || item.QuantityPlayer <= 0)
                continue;

            string name = item.Name ?? "";
            foreach (string accepted in acceptedNames)
            {
                if (string.IsNullOrEmpty(accepted)) continue;
                if (string.Equals(name, accepted, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
        }

        return false;
    }
}
