using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

// References from: https://forum.unity.com/threads/inventory-system.980646/

public enum Rarity { Common, Rare, Legendary }

public enum ItemType { Consumable, Equipment, Ingredient}

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class ItemSO : SerializableScriptableObject
{
    [Tooltip("The name of the item")]
    [SerializeField] private LocalizedString _name = default;
    
    [SerializeField] private LocalizedString _description = default;
    
    [SerializeField] private Sprite _previewImage = default;
    
    [SerializeField] private GameObject _prefab = default;
    
    [SerializeField] private ItemType _type = default;
    
    [SerializeField] private InventoryTabSO _inventoryTab = default;
    
    public LocalizedString Name => _name;
    public GameObject Prefab => _prefab;
    public ItemType Type => _type;
    public Sprite PreviewImage => _previewImage;
    public LocalizedString Description => _description;
    public InventoryTabSO InventoryTab => _inventoryTab;
}