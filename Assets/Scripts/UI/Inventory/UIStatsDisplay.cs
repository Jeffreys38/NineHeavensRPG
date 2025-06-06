using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UIStatsDisplay : MonoBehaviour
{
    [Header("Listening On")] 
    [SerializeField] private ItemEventChannelSO _onItemClicked;

    [SerializeField] private Image _icon;
    [SerializeField] private LocalizeStringEvent _titleLocalizedEvent;
    [SerializeField] private TextMeshProUGUI _enhanceLevelText;
    [SerializeField] private LocalizeStringEvent _descriptionLocalizedEvent;
    [SerializeField] private GameObject _statPrefab;
    [SerializeField] private Transform _statParent;
    
    private readonly List<UIGroupStat> _statInstances = new();

    private void OnEnable()
    {
        _onItemClicked.OnEventRaised += HandleItem;
    }

    private void OnDisable()
    {
        _onItemClicked.OnEventRaised -= HandleItem;
    }

    void Start()
    {
        
    }

    private void HandleItem(ItemSO item)
    {
        if (item == null) return;
        ClearStats();

        _titleLocalizedEvent.StringReference = item.ItemName;
        _descriptionLocalizedEvent.StringReference = item.Description;
        
        if (item is EquipmentItemSO equipment)
        {
            DisplayEquipmentStats(equipment);
        }
        else if (item is BuffItemSO buffItem)
        {
            DisplayBuffStats(buffItem);
        }
    }

    private void DisplayEquipmentStats(EquipmentItemSO equipment)
    {
        _enhanceLevelText.text = "(+" + equipment.EnhancementLevel + ")";
        
        foreach (var stat in equipment.GetStats())
        {
            CreateStat(stat.Key, stat.Value);
        }
    }

    private void DisplayBuffStats(BuffItemSO buffItem)
    {
        // foreach (var buff in buffItem.Buffs)
        // {
        //     LocalizedString statKey = new LocalizedString
        //     {
        //         TableReference = "ItemAttributes",
        //         TableEntryReference = buff.Key.ToString().ToLower()
        //     };
        //     CreateStat(statKey, buff.Value.ToString());
        // }
    }

    private void CreateStat(LocalizedString key, string value)
    {
        GameObject statInstance = Instantiate(_statPrefab, _statParent);
        UIGroupStat stat = statInstance.GetComponent<UIGroupStat>();
        stat.Set(key, value, _statParent);
        _statInstances.Add(stat);
    }

    private void ClearStats()
    {
        foreach (var stat in _statInstances)
        {
            Destroy(stat.gameObject);
        }
        _statInstances.Clear();
    }
}