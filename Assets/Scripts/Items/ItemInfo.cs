using System;
using NUnit.Framework.Internal.Commands;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Item ScriptableObject
/// Author: Tin Trinh
/// Date: Mar. 4, 2026
/// Source: None
/// </summary>
[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class ItemInfo : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private string description;
    [SerializeField] private ItemCategory category;
    [SerializeField] private int price;
    [SerializeField] private int sellPrice;
    [SerializeField] private int startingQuantity;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private ItemInfo produce;

    public string Name { get => name; }
    public string Description { get => description; }
    public ItemCategory ItemCategory { get => category; }
    public int Price { get => price; }
    public int SellPrice { get => sellPrice; }
    public int StartingQuantity { get => startingQuantity; }
    public Sprite ItemSprite { get => itemSprite; }
    public ItemInfo Produce { get => produce; }
}
