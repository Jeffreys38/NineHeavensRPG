using UnityEngine;
using System.Collections.Generic;

public class MinimapController : MonoBehaviour
{
    [Header("Tracker Event Channel")]
    public MinimapTrackerEventChannelSO trackerEventChannel;

    [Header("UI")]
    public RectTransform minimapRoot; // UI panel contain minimap
    public RectTransform iconContainer; // Icon of target
    public Camera minimapCamera;

    private Dictionary<MinimapTracker, RectTransform> activeTrackers = new();

    private void OnEnable()
    {
        trackerEventChannel.OnTrackerRegistered += RegisterTracker;
        trackerEventChannel.OnTrackerUnregistered += UnregisterTracker;
    }

    private void OnDisable()
    {
        trackerEventChannel.OnTrackerRegistered -= RegisterTracker;
        trackerEventChannel.OnTrackerUnregistered -= UnregisterTracker;
    }

    private void Update()
    {
        foreach (var kvp in activeTrackers)
        {
            MinimapTracker tracker = kvp.Key;
            RectTransform icon = kvp.Value;

            Vector3 worldPos = tracker.transform.position;
            Vector3 viewportPos = minimapCamera.WorldToViewportPoint(worldPos);

            Vector2 minimapSize = minimapRoot.sizeDelta;
            Vector2 iconPos = new Vector2(
                (viewportPos.x - 0.5f) * minimapSize.x,
                (viewportPos.y - 0.5f) * minimapSize.y
            );

            icon.anchoredPosition = iconPos;
        }
    }

    private void RegisterTracker(MinimapTracker tracker)
    {
        if (tracker.iconPrefab == null) return;

        GameObject icon = Instantiate(tracker.iconPrefab, iconContainer);
        RectTransform rect = icon.GetComponent<RectTransform>();
        activeTrackers[tracker] = rect;
    }

    private void UnregisterTracker(MinimapTracker tracker)
    {
        if (activeTrackers.TryGetValue(tracker, out RectTransform icon))
        {
            Destroy(icon.gameObject);
            activeTrackers.Remove(tracker);
        }
    }
}
