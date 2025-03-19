using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Item can be dragged & dropped at equipment slot or suit slot
/// </summary>
public class UIDragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform _originalParent;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    public UIItemSlot ParentSlot { get; private set; }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        ParentSlot = GetComponentInParent<UIItemSlot>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (ParentSlot.IsEmpty) return;

        _originalParent = transform.parent;
        transform.SetParent(_originalParent.root);
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        transform.SetParent(ParentSlot.transform);
        transform.SetAsFirstSibling();
        transform.localPosition = Vector3.zero;
    }
}