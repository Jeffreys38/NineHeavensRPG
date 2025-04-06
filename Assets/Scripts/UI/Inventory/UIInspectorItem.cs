using System.Collections.Generic;
using UnityEngine;

public enum InspectorActionType { Delete, Equip, Gift, Use }

/// <summary>
/// Attached on each tab, storage inspector action type list
/// Player tab: Delete equipment, Equip equipment
/// Bag tag: Delete equipment, Use/Gift
/// </summary>
public class UIInspectorItem : MonoBehaviour
{
    [SerializeField] private List<InspectorActionType> _actions = new List<InspectorActionType>();
    [SerializeField] private GameObject _inspectorPrefab;

    public void ShowInspector(GameObject itemObject)
    {
        Instantiate(_inspectorPrefab, itemObject.transform);
    }
}