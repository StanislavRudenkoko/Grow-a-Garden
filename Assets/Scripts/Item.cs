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
    [SerializeField] private new string name;
    [SerializeField] private string description;
    [SerializeField] private ItemCategory category;
    [SerializeField] private int price;
    [SerializeField] private int quantity;
    [SerializeField] private Sprite itemSprite;

    public string Name { get => name; }
    public string Description { get => description; }
    public ItemCategory ItemCategory { get => category; }
    public int Price { get => price; }
    public int Quantity { get => quantity; }
    public Sprite ItemSprite { get => itemSprite; }
}
