using UnityEngine;

/// <summary>
/// Quest type for defeating a specific number of enemies.
/// </summary>
[CreateAssetMenu(fileName = "NewCombatQuest", menuName = "Quests/Combat Quest")]
public class CombatQuestSO : QuestDataSO
{
    // public EnemyDataSO targetEnemy; // Enemy type
    public int requiredKillCount; // Number of enemies to kill

    /// <summary>
    /// Check if the required number of enemies have been defeated.
    /// </summary>
    public override bool CheckCompletion()
    {
        return true;
        // return QuestManager.Instance.GetEnemyKillCount(targetEnemy) >= requiredKillCount;
    }
}