using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages quest progress and chapters.
/// </summary>
public class QuestManager : MonoBehaviour, IDataPersistence
{
    public List<ChapterSO> activeChapters = new List<ChapterSO>();

    /// <summary>
    /// Accepts a quest and assigns it to the player.
    /// </summary>
    public void AcceptQuest(QuestDataSO quest)
    {
        if (quest.QuestState == QuestState.NotStarted)
        {
            quest.StartQuest();
            Debug.Log($"Quest Accepted: {quest.QuestTitle.GetLocalizedString()}");
        }
    }

    public void CheckQuestProgress(QuestDataSO quest)
    {
        if (quest.QuestState == QuestState.Completed) return;

        if (quest.CheckCompletion())
        {
            quest.CompleteQuest();
            GiveQuestRewards(quest);
            Debug.Log($"Quest Completed: {quest.QuestTitle.GetLocalizedString()}");

            CheckChapterProgress();
        }
    }

    private void GiveQuestRewards(QuestDataSO quest)
    {
        foreach (var reward in quest.RewardItems)
        {
            // InventoryManager.Instance.AddItem(reward.item, reward.quantity);
        }
        
        // Player.Instance.AddExp(quest.rewardExp);
    }

    private void CheckChapterProgress()
    {
        foreach (var chapter in activeChapters)
        {
            if (chapter.IsCompleted())
            {
                // chapter.ClaimChapterRewards();
            }
        }
    }

    public void LoadData(GameData data)
    {
        
    }

    public void SaveData(ref GameData data)
    {
       
    }
}