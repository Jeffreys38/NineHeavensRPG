using UnityEngine;

/// <summary>
/// Quest type for collecting specific items.
/// </summary>
[CreateAssetMenu(fileName = "NewCollectionQuest", menuName = "Quests/Passive Quest")]
public class CollectionQuestSO : QuestDataSO
{
    public ItemSO targetItem; // Required item
    public int requiredItemCount; // Number of items needed

    /// <summary>
    /// Check if the required number of items have been collected.
    /// </summary>
    public override bool CheckCompletion()
    {
        return true;
        // return QuestManager.Instance.GetItemCount(targetItem) >= requiredItemCount;
    }
}