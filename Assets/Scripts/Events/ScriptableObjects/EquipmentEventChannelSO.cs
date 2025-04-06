using UnityEngine.Events;
using UnityEngine;

/// <summary>
///
/// </summary>

[CreateAssetMenu(menuName = "Events/Equipment Event Channel")]
public class EquipmentEventChannelSO : DescriptionBaseSO
{
    public UnityAction<EquipmentItemSO> OnEventRaised;
	
    public void RaiseEvent(EquipmentItemSO item)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(item);
    }
}