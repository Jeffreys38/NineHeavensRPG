using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<QuestDataSO> _currentQuests = new List<QuestDataSO>();
    
    [Header("Broadcasting On")] 
    [SerializeField] private ItemStackEventChannelSO _addItemEvent;

    [Header("Listening To")] 
    [SerializeField] private ItemStackEventChannelSO _onQuestRewardRequested;
    [SerializeField] private QuestEventChannelSO _onQuestAddRequested;
    
    public List<QuestDataSO> CurrentQuests => _currentQuests;

    private void OnEnable()
    {
        // _onQuestRewardRequested.OnEventRaised += GrantRewards;
        _onQuestAddRequested.OnEventRaised += AddQuest;
    }

    private void OnDisable()
    {
       // _onQuestRewardRequested.OnEventRaised -= GrantRewards;
        _onQuestAddRequested.OnEventRaised -= AddQuest;
    }

    private void AddQuest(QuestDataSO quest)
    {
        quest.questState = QuestState.InProgress;
        _currentQuests.Add(quest);
        
        Debug.Log("Active quest: " + quest.questState.ToString());
    }

    private void GrantRewards(ItemStack rewards)
    {
        _addItemEvent.RaiseEvent(rewards);
    }
}