using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

// References from: https://forum.unity.com/threads/inventory-system.980646/

public enum Rarity { Common, Rare, Legendary }

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class ItemSO : SerializableScriptableObject
{
    [Tooltip("The name of the item")]
    [SerializeField] private LocalizedString _name = default;
    
    [SerializeField] private Sprite _previewImage = default;
    
    [SerializeField] private LocalizedString _description = default;
    
    [SerializeField] private InventoryTabSO _inventoryTab = default;
    
    public LocalizedString Name => _name;
    public Sprite PreviewImage => _previewImage;
    public LocalizedString Description => _description;
    public InventoryTabSO InventoryTab => _inventoryTab;
    
    // Only used for crafted item
    public virtual List<ItemStack> IngredientsList { get; }
    public virtual ItemSO ResultingCrafting { get; }
}