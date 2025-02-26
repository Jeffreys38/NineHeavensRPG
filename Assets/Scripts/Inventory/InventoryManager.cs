using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	[SerializeField] private InventorySO _currentInventory = default;
	[SerializeField] private SaveSystem _saveSystem;

	[Header("Listening on")]
	[SerializeField] private ItemEventChannelSO _useItemEvent = default;
	[SerializeField] private ItemEventChannelSO _equipItemEvent = default;
	[SerializeField] private ItemStackEventChannelSO _rewardItemEvent = default;
	[SerializeField] private ItemEventChannelSO _giveItemEvent = default;
	[SerializeField] private ItemEventChannelSO _addItemEvent = default;
	[SerializeField] private ItemEventChannelSO _removeItemEvent = default;
	
	private void OnEnable()
	{
	
	}

	private void OnDisable()
	{
	
	}
}

