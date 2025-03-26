using UnityEngine;

[CreateAssetMenu(fileName = "Quest_Kill_BloodServant_3", menuName = "Quests/Kill Quest Data")]
public class KillEnemyQuestSO : QuestDataSO
{
    [SerializeField] private string enemyID;
    [SerializeField] private int requiredKill;
    [SerializeField] private int currentKill = 0;

    [Header("Listening To")] 
    [SerializeField] private StringEventChannelSO _onEnemyKilled;

    public override bool IsComplete()
    {
        return currentKill >= requiredKill;
    }

    private void TrackEnemyKillProgress(string killedEnemyID)
    {
        if (killedEnemyID == enemyID) currentKill++;
    }
        
    public override void RegisterEvent()
    {
        _onEnemyKilled.OnEventRaised += TrackEnemyKillProgress;
    }

    public override void RemoveEvent()
    {
        _onEnemyKilled.OnEventRaised -= TrackEnemyKillProgress;
    }
}