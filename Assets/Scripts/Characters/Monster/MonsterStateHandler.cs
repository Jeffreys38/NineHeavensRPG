using UnityEngine;

public enum MonsterState
{
    Idle,
    Patrolling,
    Chasing,
    Attacking,
    Knockback,
    Dead
}

public class MonsterStateHandler : MonoBehaviour
{
    public MonsterState CurrentState { get; private set; }

    public void SetState(MonsterState newState)
    {
        CurrentState = newState;
        Debug.Log("Monster state changed to: " + CurrentState);
    }
}