using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class UIInventoryTabContainer
{
	public InventoryTabSO Tab;
	public Transform Container;
}

public class UIInventory : MonoBehaviour
{
	[SerializeField] private InputReader _inputReader = default;
	[SerializeField] private InventorySO _currentInventory = default;
	[SerializeField] private GameObject _emptySlotPrefab = default; 
	[SerializeField] private List<UIInventoryTabContainer> _tabContainers;
	
	[Header("Listening to")]
	[SerializeField] private UIInventoryTabs _tabsPanel = default;

	[Header("Broadcasting on")]
	[SerializeField] private ItemEventChannelSO _useItemEvent = default;
	[SerializeField] private IntEventChannelSO _restoreHealth = default;
	[SerializeField] private ItemEventChannelSO _equipItemEvent = default;

	private InventoryTabSO _selectedTab = default;
	private Dictionary<InventoryTabSO, Transform> _tabItemContainers = new Dictionary<InventoryTabSO, Transform>();
	
	private void Awake()
	{
		foreach (var tabContainer in _tabContainers)
		{
			_tabItemContainers[tabContainer.Tab] = tabContainer.Container;
			tabContainer.Container.gameObject.SetActive(false);
		}

		_currentInventory.Init();
	}
	
	private void OnEnable()
	{
		_tabsPanel.TabChanged += OnTabChanged;
	}

	private void OnDisable()
	{
		_tabsPanel.TabChanged -= OnTabChanged;
	}

	private void OnTabChanged(UIInventoryTab tab)
	{
		if (_selectedTab != null && _tabItemContainers.ContainsKey(_selectedTab))
		{
			_tabItemContainers[_selectedTab].gameObject.SetActive(false);
		}
		
		_selectedTab = tab.TabType;
        
		if (_tabItemContainers.ContainsKey(_selectedTab))
		{
			_tabItemContainers[_selectedTab].gameObject.SetActive(true);
			LoadItems();
		}
	}
	
	private void LoadItems()
	{
		if (!_tabItemContainers.ContainsKey(_selectedTab)) return;

		Transform itemContainer = _tabItemContainers[_selectedTab];
		
		// Clear previous item
		foreach (Transform child in itemContainer)
		{
			Destroy(child.gameObject);
		}

		List<ItemStack> items = _currentInventory.Items.FindAll(i => i.Item.TabType == _selectedTab.TabType);
		int maxSlots = _selectedTab._maxSlots;

		for (int i = 0; i < maxSlots; i++)
		{
			GameObject slot = Instantiate(_emptySlotPrefab, itemContainer);

			if (i < items.Count)
			{
				UIItemSlot itemSlot = slot.GetComponent<UIItemSlot>();
				itemSlot.SetItem(items[i]);
			}
		}
	}
}