using UnityEngine;
using UnityEngine.Localization;
using System.Collections.Generic;

/// <summary>
/// Represents a chapter containing a list of quests.
/// </summary>
[CreateAssetMenu(fileName = "NewChapter", menuName = "Quests/Chapter")]
public class ChapterSO : SerializableScriptableObject
{
    public LocalizedString chapterTitle;
    public List<QuestDataSO> quests;
    // public List<ItemReward> chapterRewards;

    /// <summary>
    /// Check completed state of all quests in the list
    /// </summary>
    public bool IsCompleted()
    {
        foreach (var quest in quests)
        {
            if (quest.QuestState != QuestState.Completed) return false;
        }
        return true;
    }
}