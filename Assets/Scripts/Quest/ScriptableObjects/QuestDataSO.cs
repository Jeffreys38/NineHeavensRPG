using UnityEngine;
using UnityEngine.Localization;
using System.Collections.Generic;

public enum QuestState { NotStarted, InProgress, Completed }
public enum QuestType
{
    Combat,
    Collection,
    Passive // Quest don't need action (Ex: wait 3 day, meet NPC)
}

[CreateAssetMenu(fileName = "Quest", menuName = "Quests/Quest")]
public abstract class QuestDataSO : SerializableScriptableObject
{
    [SerializeField] private LocalizedString _questTitle;
    [SerializeField] private LocalizedString _questDescription;
    [SerializeField] private QuestType _questType;
    [SerializeField] private QuestState _questState = QuestState.NotStarted;
    [SerializeField] private List<ItemReward> rewardItems;
    [SerializeField] private int rewardExp;
    
    public LocalizedString QuestTitle => _questTitle;
    public LocalizedString QuestDescription => _questDescription;
    public QuestType QuestType => _questType;
    public QuestState QuestState => _questState;
    public List<ItemReward> RewardItems => rewardItems;
    public int RewardExp => rewardExp;

    public abstract bool CheckCompletion();

    /// <summary>
    /// Mark quest state when the player accepts
    /// </summary>
    public void StartQuest()
    {
        if (_questState == QuestState.NotStarted)
        {
            _questState = QuestState.InProgress;
        }
    }

    /// <summary>
    ///  Mark quest is completed if the player completed
    /// </summary>
    public void CompleteQuest()
    {
        if (_questState == QuestState.InProgress && CheckCompletion())
        {
            _questState = QuestState.Completed;
        }
    }
}