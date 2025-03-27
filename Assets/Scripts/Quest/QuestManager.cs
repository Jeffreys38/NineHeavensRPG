using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class QuestManager : MonoBehaviour
{
    [SerializeField] private QuestListSO _questListSaved;
    
    [Header("Broadcasting On")] 
    [SerializeField] private ItemStackEventChannelSO _addItemEvent;

    [Header("Listening To")] 
    [SerializeField] private ItemStackEventChannelSO _onQuestRewardRequested;
    [SerializeField] private QuestEventChannelSO _onQuestAddRequested;
    
    public List<QuestDataSO> CurrentQuests => _questListSaved.CurrentQuests;

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
        _questListSaved.AddQuest(quest);
        
        Debug.Log("Active quest: " + quest.questState.ToString());
    }

    private void GrantRewards(ItemStack rewards)
    {
        _addItemEvent.RaiseEvent(rewards);
    }
}