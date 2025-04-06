public class SmartMonsterAI : IMonsterAI
{
    public void UpdateAI(MonsterController monster)
    {
        MonsterHealth monsterHealth = monster.GetComponent<MonsterHealth>();
        
        if (monster.stateHandler.CurrentState == MonsterState.Idle)
        {
            monster.Patrol();
        }
        else if (monster.CanSeePlayer())
        {
            if (monsterHealth.IsLowHealth())
            {
                monster.Dodge();
            }
            else
            {
                monster.stateHandler.SetState(MonsterState.Chasing);
                monster.ChasePlayer();
            }
        }
    }
}