using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UIItemSlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    [Header("Broadcasting On")] 
    [SerializeField] private ItemEventChannelSO _itemClickEvent;
    [SerializeField] private EquipmentEventChannelSO _removedEquipmentEvent;
    [SerializeField] private EquipmentEventChannelSO _equipmentAssignedEvent;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _quantityText;

    [Header("Slot Configuration")]
    [SerializeField] private bool _isEquipmentSlot = false;
    [SerializeField] private EquipmentType _slotType;

    private ItemStack _currentItemStack;
    private GameObject _item;

    public bool IsEmpty => _currentItemStack == null;
    public EquipmentType SlotType => _slotType;
    public bool IsEquipmentSlot => _isEquipmentSlot;
    public ItemStack CurrentItem => _currentItemStack; 
    
    public void SetItem(ItemStack itemStack)
    {
        _currentItemStack = itemStack;
    
        if (IsEquipmentSlot && itemStack.Item is EquipmentItemSO equipmentItem)
        {
            _equipmentAssignedEvent.RaiseEvent(equipmentItem);
        }
        
        itemStack.Item.Prefab.InstantiateAsync(transform).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _item = handle.Result;
                _item.transform.SetAsFirstSibling();
            
                UpdateQuantity(_currentItemStack.Amount);
                ApplyBehavior();
            }
            else
            {
                Debug.LogError("Failed to load item prefab.");
            }
        };
    }

    private void UpdateQuantity(int amount = 0)
    {
        if (SlotType == EquipmentType.None && _quantityText != null)
        {
            _quantityText.text = amount > 0 ? amount.ToString() : "";
        }
    }

    public void ClearItem()
    {
        if (IsEquipmentSlot && _currentItemStack.Item as EquipmentItemSO)
        {
            _removedEquipmentEvent.RaiseEvent(_currentItemStack.Item as EquipmentItemSO);
        }
        
        _currentItemStack = null;
        Destroy(_item);
        _quantityText.text = "";
    }

    private void ApplyBehavior()
    {
        _item.AddComponent<UIDragItem>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        try
        {
            var draggedItem = eventData.pointerDrag.GetComponent<UIDragItem>();
            if (draggedItem == null) return;

            var sourceSlot = draggedItem.ParentSlot;

            // If the target slot already contains an item, perform a swap instead of direct placement
            if (!this.IsEmpty && !sourceSlot.IsEmpty)
            {
                SwapItem(sourceSlot);
                return;
            }

            // If the target slot is an equipment slot, check if the dragged item is a valid equipment type
            var equipmentItemSO = sourceSlot.CurrentItem.Item as EquipmentItemSO;
            if (IsEquipmentSlot && (equipmentItemSO == null || this.SlotType != equipmentItemSO.EquipmentType))
            {
                Debug.Log("Cannot place this item in this slot!");
                return;
            }

            // Move the item from the source slot to the target slot
            SetItem(sourceSlot.CurrentItem);
            sourceSlot.ClearItem();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    private void SwapItem(UIItemSlot sourceSlot)
    {
        if (sourceSlot.IsEmpty || IsEmpty) return;

        var tempItem = _currentItemStack;
        var tempGameObject = _item;

        SetItem(sourceSlot.CurrentItem);
        sourceSlot.SetItem(tempItem);

        Destroy(tempGameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_currentItemStack == null) return;
        
        ItemSO item = _currentItemStack.Item;
        if (item != null)
        {
            _itemClickEvent.RaiseEvent(item);
        }
    }
}