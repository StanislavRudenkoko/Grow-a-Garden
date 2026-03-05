using NUnit.Framework.Internal.Commands;
using UnityEngine;

/// <summary>
/// Item ScriptableObject
/// Author: Tin Trinh
/// Date: Mar. 4, 2026
/// Source: None
/// </summary>
[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    private new string name;
    private string description;
    private ItemCategory category;
    private int price;
    private int quantity;
    private Sprite itemSprite;

    public string Name { get => name; }
    public string Description { get => description; }
    public ItemCategory ItemCategory { get => category; }
    public int Price { get => price; }
    public int Quantity { get => quantity; }
    public Sprite ItemSprite { get => itemSprite; }
}
