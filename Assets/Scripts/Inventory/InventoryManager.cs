using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
	[SerializeField] private GameObject _inventoryUI;
	[SerializeField] private InputReader _inputReader;
	[SerializeField] private InventorySO _currentInventory;
	[SerializeField] private SaveSystem _saveSystem;

	[Header("Listening on")] 
	[SerializeField] private ItemEventChannelSO _useItemEvent;
	[SerializeField] private ItemEventChannelSO _equipItemEvent;
	[SerializeField] private ItemEventChannelSO _addItemEvent;
	[SerializeField] private ItemEventChannelSO _removeItemEvent;
	
	private void OnEnable()
	{
		
	}

	private void OnDisable()
	{
		
	}
}

