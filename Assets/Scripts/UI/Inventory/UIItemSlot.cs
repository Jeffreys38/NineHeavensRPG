using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIItemSlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    [Header("Broadcasting On")] 
    [SerializeField] private ItemEventChannelSO _itemClickEvent;

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
        
        _item = Instantiate(_currentItemStack.Item.Prefab, transform);
        _item.transform.SetAsFirstSibling();

        UpdateQuantity(_currentItemStack.Amount);
        
        // Apply behaviors for item (event when clicking, drag & drop)
        ApplyBehavior();
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
        var draggedItem = eventData.pointerDrag?.GetComponent<UIDragItem>();
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
        if (this.IsEquipmentSlot && (equipmentItemSO == null || this.SlotType != equipmentItemSO.EquipmentType))
        {
            Debug.Log("Cannot place this item in this slot!");
            return;
        }

        // Move the item from the source slot to the target slot
        this.SetItem(sourceSlot.CurrentItem);
        sourceSlot.ClearItem();
    }

    private void SwapItem(UIItemSlot sourceSlot)
    {
        if (sourceSlot.IsEmpty || this.IsEmpty) return;

        var tempItem = this._currentItemStack;
        var tempGameObject = this._item;

        this.SetItem(sourceSlot.CurrentItem);
        sourceSlot.SetItem(tempItem);

        Destroy(tempGameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemSO item = _currentItemStack.Item;

        if (item != null)
        {
            _itemClickEvent.RaiseEvent(item);
        }
    }
}