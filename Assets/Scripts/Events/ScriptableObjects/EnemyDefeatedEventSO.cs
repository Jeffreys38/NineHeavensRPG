using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event channel used to send when a monster died
/// Example: Fire this event to
/// Position of monster: Vector2
/// Rewards: ItemSO[]
/// </summary>
[CreateAssetMenu(menuName = "Events/Enemy Defeated Event")]
public class EnemyDefeatedEventSO : DescriptionBaseSO
{
    public UnityAction<Vector2, List<ItemSO>> OnEventRaised;

    public void RaiseEvent(Vector2 position, List<ItemSO> droppedItems)
    {
        OnEventRaised?.Invoke(position, droppedItems);
    }
}