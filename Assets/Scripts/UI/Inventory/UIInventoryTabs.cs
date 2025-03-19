using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIInventoryTabs : MonoBehaviour
{
    [SerializeField] private List<UIInventoryTab> _instantiatedTabs;
    [SerializeField] private UIInventoryTab _currentActiveTab;
    
    public event UnityAction<UIInventoryTab> TabChanged;

    void Start()
    {
        ChangeTab(_currentActiveTab.TabType);
    }
    
    public void ChangeTab(InventoryTabSO newTabType)
    {
        if (_currentActiveTab != null)
        {
            _currentActiveTab.DeactiveTab();
        }

        _currentActiveTab = _instantiatedTabs.Find(t => t.TabType == newTabType);
        if (_currentActiveTab != null)
        {
            _currentActiveTab.ActiveTab();
        }
        
        TabChanged?.Invoke(_currentActiveTab);
    }
}