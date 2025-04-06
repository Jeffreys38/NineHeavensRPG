using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Cutscene Event Channel")]
public class CutsceneEventChannelSO : ScriptableObject
{
    public UnityAction<CutsceneSO> OnEventRaised;
	
    public void RaiseEvent(CutsceneSO value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}