using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Cutscene Event Channel")]
public class CutsceneEventChannelSO : ScriptableObject
{
    public UnityAction<GameSceneSO> OnEventRaised;
	
    public void RaiseEvent(GameSceneSO value)
    {
         OnEventRaised?.Invoke(value);
    }
}