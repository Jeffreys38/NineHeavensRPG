using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleSkipCutscene : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private VoidEventChannelSO _onSkipCutsceneRequested;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        _onSkipCutsceneRequested?.RaiseEvent();
    }
}