using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Minimap/Tracker Event Channel")]
public class MinimapTrackerEventChannelSO : ScriptableObject
{
    public UnityAction<MinimapTracker> OnTrackerRegistered;
    public UnityAction<MinimapTracker> OnTrackerUnregistered;

    public void RaiseRegister(MinimapTracker tracker)
    {
        OnTrackerRegistered?.Invoke(tracker);
    }

    public void RaiseUnregister(MinimapTracker tracker)
    {
        OnTrackerUnregistered?.Invoke(tracker);
    }
}