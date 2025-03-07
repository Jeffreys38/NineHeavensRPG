using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event channel used to send an EntitySO as an argument.
/// Example: A system selects a monster or NPC and fires this event with the selected EntitySO.
/// </summary>
[CreateAssetMenu(menuName = "Events/Entity Event Channel")]
public class EntityEventChannelSO : DescriptionBaseSO
{
    public UnityAction<EntitySO> OnEventRaised;

    public void RaiseEvent(EntitySO entity)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(entity);
    }
}