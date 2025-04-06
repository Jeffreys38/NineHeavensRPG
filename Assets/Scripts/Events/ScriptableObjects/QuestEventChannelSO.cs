using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Quest Event Channel")]
public class QuestEventChannelSO : DescriptionBaseSO
{
    public UnityAction<QuestDataSO> OnEventRaised;
	    
    public void RaiseEvent(QuestDataSO value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}