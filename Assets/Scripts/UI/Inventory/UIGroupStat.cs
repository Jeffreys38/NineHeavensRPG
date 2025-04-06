using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class UIGroupStat : MonoBehaviour
{
    [SerializeField] private LocalizeStringEvent keyLocalizedEvent;
    [SerializeField] private TextMeshProUGUI valueText;
     
    public void Set(LocalizedString key, string value, Transform parent)
    {
        keyLocalizedEvent.StringReference = key;
        valueText.SetText(value);
         
        transform.SetParent(parent, false);
    }
}