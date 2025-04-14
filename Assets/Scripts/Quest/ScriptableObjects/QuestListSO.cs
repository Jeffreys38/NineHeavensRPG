using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[CreateAssetMenu(fileName = "Storage_QuestList", menuName = "Quest/Current Quest List")]
public class QuestListSO : ScriptableObject
{
    [Header("Data")]
    [SerializeField] private List<QuestDataSO> _currentQuests = new List<QuestDataSO>();
    
    public List<QuestDataSO> CurrentQuests => _currentQuests;

    public void AddQuest(QuestDataSO quest)
    {
        _currentQuests.Add(quest);
    }

    public void ResetQuestlines()
    {
        _currentQuests.Clear();
    }

    public void SetQuestlineItemsFromSave(List<string> finishedItemsGUIds)
    {
        
    }
}