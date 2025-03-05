using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public enum ItemType { Consumable, Equipment, Ingredient }
public enum Rarity { Common, Rare, Legendary }

/// <summary>
/// Base class for all items in the game.
/// </summary>
public abstract class ItemSO : SerializableScriptableObject
{
    [SerializeField] private string _itemName;
    [SerializeField] private LocalizedString _description;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private ItemType _itemType;
    
    public string ItemName => _itemName;
    public LocalizedString Description => _description;
    public GameObject Prefab => _prefab;
    public ItemType ItemType => _itemType;
}

/// <summary>
/// Represents an item reward with quantity.
/// </summary>
[System.Serializable]
public class ItemReward
{
    public ItemSO item;
    public int quantity;
}