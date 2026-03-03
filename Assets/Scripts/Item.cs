using NUnit.Framework.Internal.Commands;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public new string name;
    public string description;
    
    public Sprite itemSprite;
    public int cost;
    public int quantity;
}
