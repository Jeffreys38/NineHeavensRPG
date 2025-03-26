using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Dialogue Event Channel")]
public class DialogueEventChannelSO : DescriptionBaseSO
{
    public event UnityAction<DialogueDataSO> OnEventRaised;

    public void RaiseEvent(DialogueDataSO value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}