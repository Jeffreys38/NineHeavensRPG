using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory")]
public class InventorySO : ScriptableObject, IDataPersistence
{
    [SerializeField] private List<ItemStack> _items = new List<ItemStack>();
    [SerializeField] private List<InventoryTabSO> _inventoryTabs;

    public List<ItemStack> Items => _items;
    public List<InventoryTabSO> InventoryTabs => _inventoryTabs;

    public void Init()
    {
        Sort();
    }
    
    public void Add(ItemSO item, int count = 1)
    {
        if (item == null || count <= 0) return;

        foreach (var itemStack in _items)
        {
            if (itemStack.Item == item)
            {
                itemStack.Amount += count;
                Sort();
                return;
            }
        }

        _items.Add(new ItemStack(item, count));
        // Sort();
    }

    public void Remove(ItemSO item, int count = 1)
    {
        if (item == null || count <= 0) return;

        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].Item == item)
            {
                _items[i].Amount -= count;

                if (_items[i].Amount <= 0)
                {
                    _items.RemoveAt(i);
                }

                return;
            }
        }
    }

    private void Sort()
    {
        var groupedItems = GroupNonEquipmentItems();
        var separatedEquipment = SeparateEquipmentItems();
        
        _items = groupedItems.Concat(separatedEquipment).ToList();
    }
    
    private List<ItemStack> GroupNonEquipmentItems()
    {
        var itemCountMap = new Dictionary<ItemSO, int>();

        foreach (var itemStack in _items)
        {
            if (itemStack == null || itemStack.Item == null) continue;
            if (itemStack.Item is EquipmentItemSO) continue;

            if (itemCountMap.TryGetValue(itemStack.Item, out int existingAmount))
            {
                itemCountMap[itemStack.Item] = existingAmount + itemStack.Amount;
            }
            else
            {
                itemCountMap[itemStack.Item] = itemStack.Amount;
            }
        }
        
        return itemCountMap.Select(entry => new ItemStack(entry.Key, entry.Value)).ToList();
    }
    
    private List<ItemStack> SeparateEquipmentItems()
    {
        var equipmentList = new List<ItemStack>();

        foreach (var itemStack in _items)
        {
            if (itemStack == null || itemStack.Item == null) continue;

            if (itemStack.Item is EquipmentItemSO && itemStack.Amount > 1)
            {
                for (int i = 0; i < itemStack.Amount; i++)
                {
                    equipmentList.Add(new ItemStack(itemStack.Item, 1));
                }
            }
            else if (itemStack.Item is EquipmentItemSO)
            {
                equipmentList.Add(itemStack);
            }
        }

        return equipmentList;
    }

    public void SaveData(ref GameData data)
    {
        data.itemStacks.Clear();
            
        foreach (var item in _items)
        {
            var serializeItem = new SerializedItemStack(item.Item.Guid, item.Amount);
            data.itemStacks.Add(serializeItem);
        }
    }

    public void LoadData(GameData data)
    {
        data.itemStacks.Clear();
        
        List<string> itemGuids = GetItemGuidsByItemStacks(data.itemStacks);
        AddressableLoader.LoadAssetsByGuids<ItemStack>(itemGuids, (loadedItem) =>
        {
            _items.AddRange(loadedItem);
            Debug.Log($"Loaded {_items.Count} items from Addressables.");
        });
    }

    private List<string> GetItemGuidsByItemStacks(List<SerializedItemStack> itemStacks)
    {
        List<string> itemGuids = new List<string>();

        foreach (var itemStack in itemStacks)
        {
            itemGuids.Add(itemStack.itemGuid);
        }
        
        return itemGuids;
    }
}