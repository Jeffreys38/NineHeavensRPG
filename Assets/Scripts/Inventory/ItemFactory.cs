using UnityEngine;

public static class ItemFactory
{
    public static EquipmentItemSO CreateEquipment(Rarity rarity, RarityStatRangeSO statRange, EquipmentItemSO baseItem)
    {
        EquipmentItemSO newItem = ScriptableObject.Instantiate(baseItem);
        newItem.rarity = rarity;

        newItem.SetStats(
            GetRandomStat(statRange.GetRange(rarity, BuffItemType.Attack)),
            GetRandomStat(statRange.GetRange(rarity, BuffItemType.Defense)),
            GetRandomStat(statRange.GetRange(rarity, BuffItemType.Intelligence)),
            GetRandomStat(statRange.GetRange(rarity, BuffItemType.Lucky))
        );

        return newItem;
    }

    public static BuffItemSO CreateBuffItem(Rarity rarity, RarityStatRangeSO statRange, BuffItemSO baseItem)
    {
        return null;
    }

    private static int GetRandomStat(Vector2 range)
    {
        return Mathf.RoundToInt(Random.Range(range.x, range.y));
    }
}