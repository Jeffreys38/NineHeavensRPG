using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public bool isBoss = false;
    public MonsterStateHandler stateHandler;
    
    private IMonsterAI _ai;

    private void Awake()
    {
        _ai = isBoss ? new SmartMonsterAI() : new SimpleMonsterAI();
        stateHandler = GetComponent<MonsterStateHandler>();
    }

    private void Start()
    {
        stateHandler.SetState(MonsterState.Idle);
    }

    public bool CanSeePlayer()
    {
        return true;
    }

    public void Patrol()
    {
        
    }
    
    public void Attack()
    {
        
    }

    public void Dodge()
    {
        
    }

    public void ChasePlayer()
    {
        
    }
}