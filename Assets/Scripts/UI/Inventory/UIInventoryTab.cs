using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIInventoryTab : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _tabImage;
    [SerializeField] private Color _selectedIconColor;
    [SerializeField] private Color _deselectedIconColor;
    [SerializeField] private GameObject _tabViewGameObject;
    [SerializeField] private UIInventoryTabs _parentTabs;
    [SerializeField] private InventoryTabSO _tabType;
    
    public InventoryTabSO TabType => _tabType;
    public GameObject TabViewGameObject => _tabViewGameObject;

    public void OnPointerClick(PointerEventData eventData)
    {
        _parentTabs?.ChangeTab(_tabType);
    }

    public void ActiveTab()
    {
        TabViewGameObject.SetActive(true);
        
        SetTabColor(_selectedIconColor);
    }

    public void DeactiveTab()
    {
        TabViewGameObject.SetActive(false);
        
        SetTabColor(_deselectedIconColor);
    }
    
    private void SetTabColor(Color baseColor)
    {
        Color newColor = baseColor;
        newColor.a = 1f;
        _tabImage.color = newColor;
    }
}