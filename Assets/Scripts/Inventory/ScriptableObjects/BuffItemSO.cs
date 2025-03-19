using System.Collections.Generic;
using UnityEngine;

public enum BuffItemType
{
    Health,
    Mana,
    Attack,
    Defense,
    Intelligence,
    Lucky
}

[CreateAssetMenu(fileName = "NewBuffItem", menuName = "Inventory/BuffItem")]
public class BuffItemSO : ItemSO
{
    [SerializeField] private Dictionary<BuffItemType, int> _buffs = new Dictionary<BuffItemType, int>();
    
    public Dictionary<BuffItemType, int> Buffs => _buffs;
    
    public void SetBuff(BuffItemType type, int value)
    {
        if (_buffs.ContainsKey(type))
            _buffs[type] = value;
        else
            _buffs.Add(type, value);
    }
}