public class SimpleMonsterAI : IMonsterAI
{
    public void UpdateAI(MonsterController monster)
    {
        if (monster.stateHandler.CurrentState == MonsterState.Idle)
        {
            monster.Patrol();
        }
        else if (monster.CanSeePlayer())
        {
            monster.stateHandler.SetState(MonsterState.Attacking);
            monster.Attack();
        }
    }
}