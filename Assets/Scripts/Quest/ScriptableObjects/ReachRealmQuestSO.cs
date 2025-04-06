using UnityEngine;

[CreateAssetMenu(fileName = "Quest_ReachRealm_Mortal_Late", menuName = "Quests/Reach Realm Quest Data")]
public class ReachRealmQuestSO : QuestDataSO
{
    [SerializeField] private ProtagonistStateSO _playerData;
    [SerializeField] private RealmTier requiredTier;
    [SerializeField] private RealmStage requiredStage;
    
    public override bool IsComplete()
    {
        return HasReachedRequiredTier() && HasReachedRequiredStage();
    }

    public override void RegisterEvent() { }

    public override void RemoveEvent() { }

    private bool HasReachedRequiredTier()
    {
        return _playerData.currentRealmTier >= requiredTier;
    }

    private bool HasReachedRequiredStage()
    {
        return _playerData.currentRealmStage >= requiredStage;
    }
}