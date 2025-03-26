using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public enum EquipmentType
{
    None,
    Glove,
    Armor,
    Ring,
    Necklace,
    Boot
}

[CreateAssetMenu(fileName = "NewEquipmentItem", menuName = "Inventory/EquipmentItem")]
public class EquipmentItemSO : ItemSO, IItemInfoProvider
{
    [SerializeField] private int _bonusAttack;
    [SerializeField] private int _bonusDefense;
    [SerializeField] private float _bonusIntelligence;
    [SerializeField] private float _bonusLucky;
    [SerializeField] private EquipmentType _equipmentType;
    
    [SerializeField] private int _enhancementLevel = 0;
    [SerializeField] private float enhancementMultiplier = 0.1f;

    public int BaseAttack => _bonusAttack;
    public int BaseDefense => _bonusDefense;
    public float BaseIntelligence => _bonusIntelligence;
    public float BaseLucky => _bonusLucky;
    
    public int EnhancedAttack => Mathf.RoundToInt(_bonusAttack * _enhancementLevel * enhancementMultiplier);
    public int EnhancedDefense => Mathf.RoundToInt(_bonusDefense * _enhancementLevel * enhancementMultiplier);
    public float EnhancedIntelligence => _bonusIntelligence * _enhancementLevel * enhancementMultiplier;
    public float EnhancedLucky => _bonusLucky * _enhancementLevel * enhancementMultiplier;

    public EquipmentType EquipmentType => _equipmentType;
    public int EnhancementLevel => _enhancementLevel;

    private static readonly Dictionary<Rarity, int> MaxEnhancementLevels = new Dictionary<Rarity, int>
    {
        { Rarity.Common, 10 },
        { Rarity.Rare, 12 },
        { Rarity.Legendary, 15 }
    };

    public event System.Action<EquipmentItemSO> OnEnhanced;

    public bool Enhance()
    {
        if (MaxEnhancementLevels.TryGetValue(rarity, out int maxLevel) && _enhancementLevel < maxLevel)
        {
            _enhancementLevel++;
            OnEnhanced?.Invoke(this);
            return true;
        }
        return false;
    }

    public LocalizedString GetTitle() => ItemName;
    public LocalizedString GetDescription() => Description;

    public Dictionary<LocalizedString, string> GetStats()
    {
        return new Dictionary<LocalizedString, string>
        {
            { new LocalizedString { TableReference = "ItemAttributes", TableEntryReference = "attack" }, $"{BaseAttack} (+{EnhancedAttack})" },
            { new LocalizedString { TableReference = "ItemAttributes", TableEntryReference = "defense" }, $"{BaseDefense} (+{EnhancedDefense})" },
            { new LocalizedString { TableReference = "ItemAttributes", TableEntryReference = "intelligence" }, $"{BaseIntelligence:F1} (+{EnhancedIntelligence:F1})" },
            { new LocalizedString { TableReference = "ItemAttributes", TableEntryReference = "lucky" }, $"{BaseLucky:F1} (+{EnhancedLucky:F1})" },
        };
    }

    public void SetStats(int attack, int defense, float intelligence, float lucky)
    {
        _bonusAttack = attack;
        _bonusDefense = defense;
        _bonusIntelligence = intelligence;
        _bonusLucky = lucky;
    }
}