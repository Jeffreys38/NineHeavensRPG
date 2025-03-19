using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public enum ItemType { Consumable, Equipment, Ingredient }
public enum Rarity { Common, Rare, Legendary }

public interface IItemInfoProvider
{
    LocalizedString GetTitle();
    LocalizedString GetDescription();
    Dictionary<LocalizedString, string> GetStats();
}

/// <summary>
/// Base class for all items in the game.
/// </summary>
public abstract class ItemSO : SerializableScriptableObject
{
    [SerializeField] private LocalizedString _itemName;
    [SerializeField] private LocalizedString _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private ItemType _itemType;
    [SerializeField] private InventoryTabType _tabType; 
    
    public Rarity rarity;
    
    public LocalizedString ItemName => _itemName;
    public LocalizedString Description => _description;
    public Sprite Icon => _icon;
    public GameObject Prefab => _prefab;
    public ItemType ItemType => _itemType;
    public InventoryTabType TabType => _tabType;
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