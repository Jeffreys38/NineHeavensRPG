using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

public class UIQuestMiniBoard : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private LocalizeStringEvent titleEvent;
    [SerializeField] private LocalizeStringEvent descriptionEvent;
    [SerializeField] private TextMeshProUGUI progressText;
    
    [SerializeField] private QuestManager _questManager;

    private QuestDataSO _activeQuest;
    
    private void OnEnable()
    {
        List<QuestDataSO> activeQuests = _questManager.CurrentQuests;

        if (activeQuests.Count > 0)
        {
            _activeQuest = activeQuests[0];
        }

        UpdateQuest();
    }

    private void OnDisable()
    {
        
    }

    private void UpdateQuest()
    {
        if (_activeQuest == null) return;
        
        titleEvent.StringReference = _activeQuest.Name;
        descriptionEvent.StringReference = _activeQuest.Description;
        progressText.SetText("0 / 3");
    }
}