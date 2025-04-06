using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	[SerializeField] private InventorySO _currentInventory;
	[SerializeField] private SaveSystem _saveSystem;

	[Header("Listening on")] 
	[SerializeField] private ItemEventChannelSO _onItemUsed;
	[SerializeField] private ItemEventChannelSO _onItemGained;
	[SerializeField] private ItemEventChannelSO _onItemRemoved;
	
	private void OnEnable()
	{
		_onItemGained.OnEventRaised += AddItem;
	}

	private void OnDisable()
	{
		_onItemGained.OnEventRaised -= AddItem;
	}

	private void AddItem(ItemSO item)
	{
		_currentInventory.Add(item);
	}
}

