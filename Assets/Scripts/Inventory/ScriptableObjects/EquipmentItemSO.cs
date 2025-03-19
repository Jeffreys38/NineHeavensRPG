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
public class EquipmentItemSO: ItemSO, IItemInfoProvider
{
    [SerializeField] private int _bonusAttack;
    [SerializeField] private int _bonusDefense;
    [SerializeField] private float _bonusIntelligence;
    [SerializeField] private float _bonusLucky;
    [SerializeField] private EquipmentType _equipmentType;

    public int BonusAttack => _bonusAttack;
    public int BonusDefense => _bonusDefense;
    public float BonusIntelligence => _bonusIntelligence;
    public float BonusLucky => _bonusLucky;
    public EquipmentType EquipmentType => _equipmentType;

    private LocalizedString attackStat = new LocalizedString { TableReference = "ItemAttributes", TableEntryReference = "attack" };
    private LocalizedString defenseStat = new LocalizedString { TableReference = "ItemAttributes", TableEntryReference = "defense" };
    private LocalizedString intelligenceStat = new LocalizedString { TableReference = "ItemAttributes", TableEntryReference = "intelligence" };
    private LocalizedString luckyStat = new LocalizedString { TableReference = "ItemAttributes", TableEntryReference = "lucky" };

    public LocalizedString GetTitle() => ItemName;
    public LocalizedString GetDescription() => Description;

    public Dictionary<LocalizedString, string> GetStats()
    {
        return new Dictionary<LocalizedString, string>
        {
            { attackStat, _bonusAttack.ToString() },
            { defenseStat, _bonusDefense.ToString() },
            { intelligenceStat, _bonusIntelligence.ToString() },
            { luckyStat, _bonusLucky.ToString() },
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