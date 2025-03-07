using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	[SerializeField] private InputReader _inputReader;
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
		_inputReader.OnOpenInventoryEvent += OpenInventory;
		_inputReader.OnCloseInventoryEvent += CloseInventory;
		_inputReader.OnStartDragItemEvent += StartDraggingItem;
		_inputReader.OnDropItemEvent += DropItem;
	}

	private void OnDisable()
	{
		_inputReader.OnOpenInventoryEvent -= OpenInventory;
		_inputReader.OnCloseInventoryEvent -= CloseInventory;
		_inputReader.OnStartDragItemEvent -= StartDraggingItem;
		_inputReader.OnDropItemEvent -= DropItem;
	}
	
	private void OpenInventory() { Debug.Log("Inventory Opened!"); }
	private void CloseInventory() { Debug.Log("Inventory Closed!"); }
	private void StartDraggingItem() { Debug.Log("Dragging Item!"); }
	private void DropItem() { Debug.Log("Dropped Item!"); }
}

