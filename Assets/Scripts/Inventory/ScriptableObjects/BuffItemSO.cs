using System;
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

[Serializable]
public class BuffEntry
{
    public BuffItemType Type;
    public int Value;
}

[CreateAssetMenu(fileName = "NewBuffItem", menuName = "Inventory/BuffItem")]
public class BuffItemSO : ItemSO
{
    [SerializeField] private List<BuffEntry> _buffs = new List<BuffEntry>();

    public Dictionary<BuffItemType, int> Buffs
    {
        get
        {
            var dict = new Dictionary<BuffItemType, int>();
            foreach (var entry in _buffs)
            {
                dict[entry.Type] = entry.Value;
            }
            return dict;
        }
    }
}