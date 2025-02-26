using UnityEngine;
using UnityEngine.Localization;

public enum InventoryTabType
{
	Cultivation,
	Equipment,
	Backpack
}

[CreateAssetMenu(fileName = "InventoryTabType", menuName = "Inventory/Inventory Tab Type")]
public class InventoryTabSO : ScriptableObject
{
	[SerializeField] private InventoryTabType _tabType = default;

	public InventoryTabType TabType => _tabType;
}
