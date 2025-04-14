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
    
}