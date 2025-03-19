using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropRate
{
    public int minDrops;            // Minimum number of items dropped
    public int maxDrops;            // Maximum number of items dropped
    public float rareChance;        // Drop chance for Rare items (%)
    public float legendaryChance;   // Drop chance for Legendary items (%)
}

[CreateAssetMenu(menuName = "Table/Monster Drop")]
public class MonsterDropTableSO : ScriptableObject
{
    // Default drop rates based on monster rarity
    [SerializeField] private DropRate commonDrop = new DropRate { minDrops = 3, maxDrops = 5, rareChance = 100f, legendaryChance = 0f };
    [SerializeField] private DropRate rareDrop = new DropRate { minDrops = 3, maxDrops = 5, rareChance = 20f, legendaryChance = 0f };
    [SerializeField] private DropRate legendaryDrop = new DropRate { minDrops = 3, maxDrops = 5, rareChance = 70f, legendaryChance = 10f };

    // Lists of items based on rarity
    [SerializeField] private List<ItemSO> commonItems;     
    [SerializeField] private List<ItemSO> rareItems;       
    [SerializeField] private List<ItemSO> legendaryItems;  
    
    public List<ItemSO> GetDrops(Rarity rarity)
    {
        // Determine the drop rate based on the monster's rarity
        DropRate dropRate = commonDrop;
        if (rarity == Rarity.Rare) dropRate = rareDrop;
        if (rarity == Rarity.Legendary) dropRate = legendaryDrop;

        // Determine the number of items dropped
        int itemCount = Random.Range(dropRate.minDrops, dropRate.maxDrops + 1);
        List<ItemSO> droppedItems = new List<ItemSO>();
        
        // Always drop Common items
        for (int i = 0; i < itemCount; i++)
        {
            droppedItems.Add(GetRandomItem(commonItems));  
        }

        // Roll for Rare item drop
        if (Random.value * 100 < dropRate.rareChance) 
        {
            droppedItems.Add(GetRandomItem(rareItems));
        }

        // Roll for Legendary item drop
        if (Random.value * 100 < dropRate.legendaryChance) 
        {
            droppedItems.Add(GetRandomItem(legendaryItems));
        }

        return droppedItems;
    }
    
    private ItemSO GetRandomItem(List<ItemSO> itemList)
    {
        if (itemList.Count == 0) return null;
        return itemList[Random.Range(0, itemList.Count)];
    }
}