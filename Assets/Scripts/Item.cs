using NUnit.Framework.Internal.Commands;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public new string name;
    public string description;
    public ItemCategory category;
    public int cost;
    public int quantity;
    public Sprite itemSprite;
    public string Name { get; }
    public string Description { get; }
}
