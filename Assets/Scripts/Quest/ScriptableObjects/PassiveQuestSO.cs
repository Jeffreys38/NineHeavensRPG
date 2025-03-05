using UnityEngine;

/// <summary>
/// Passive quests that complete automatically (e.g., wait, talk to NPC).
/// </summary>
[CreateAssetMenu(fileName = "NewPassiveQuest", menuName = "Quests/Passive Quest")]
public class PassiveQuestSO : QuestDataSO
{
    /// <summary>
    /// Always returns true for passive quests.
    /// </summary>
    public override bool CheckCompletion()
    {
        return true;
    }
}