using System;
using System.Collections.Generic;
using UnityEngine;

public class UIQuestPanel : MonoBehaviour
{ 
    [SerializeField] private QuestManager _questManager;

    private List<QuestDataSO> _currentQuests;

    private void OnEnable()
    {
        InitQuestBoard();
    }

    private void OnDisable()
    {
        throw new NotImplementedException();
    }

    private void InitQuestBoard()
    {
        
    }

    private void UpdateQuestBoard(List<QuestDataSO> quests)
    {
        
    }
}