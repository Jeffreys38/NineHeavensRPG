using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory")]
public class InventorySO : ScriptableObject
{
	[Tooltip("The collection of items and their quantities.")]
	[SerializeField] private List<ItemStack> _items = new List<ItemStack>();
	
	public List<ItemStack> Items => _items;

	public void Init()
	{
		
	}

	public void Add(ItemSO item, int count = 1)
	{
	
	}

	public void Remove(ItemSO item, int count = 1)
	{
	
	}
}
