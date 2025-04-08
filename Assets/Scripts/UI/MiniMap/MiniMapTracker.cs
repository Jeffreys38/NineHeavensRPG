using UnityEngine;

public class MinimapTracker : MonoBehaviour
{
    [Header("Settings")]
    public GameObject iconPrefab;

    [Header("References")]
    public MinimapTrackerEventChannelSO trackerEventChannel;

    private void OnEnable()
    {
        if (trackerEventChannel != null)
            trackerEventChannel.RaiseRegister(this);
    }

    private void OnDisable()
    {
        if (trackerEventChannel != null)
            trackerEventChannel.RaiseUnregister(this);
    }
}