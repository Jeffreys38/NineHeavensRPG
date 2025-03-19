using UnityEngine;
using UnityEngine.UI;

public class UIDropItem : MonoBehaviour
{
    private SpriteRenderer itemIcon;
    // [SerializeField] private TMP_Text itemNameText;
    private ItemSO _itemData;

    void Awake()
    {
        itemIcon = GetComponent<SpriteRenderer>();
    }

    public void Initialize(ItemSO item)
    {
        _itemData = item;
        itemIcon.sprite = item.Icon;
    }

    public ItemSO GetItem()
    {
        return _itemData;
    }
}