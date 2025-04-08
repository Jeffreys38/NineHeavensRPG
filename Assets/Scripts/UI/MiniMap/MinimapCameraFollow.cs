using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MinimapCameraFollow : MonoBehaviour
{
    [Header("Camera Settings")]
    public float followHeight = 20f;
    public float followSpeed = 5f;

    [Header("Event Listener")]
    public TransformEventChannelSO playerSpawnedEvent;

    private Transform target;
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        if (playerSpawnedEvent != null)
        {
            playerSpawnedEvent.OnEventRaised += SetTarget;
        }
    }

    private void OnDisable()
    {
        if (playerSpawnedEvent != null)
        {
            playerSpawnedEvent.OnEventRaised -= SetTarget;
        }
    }

    private void SetTarget(Transform player)
    {
        target = player;
    }

    private void LateUpdate()
    {
        if (target == null) return;
        transform.position = target.transform.position + new Vector3(0, followHeight, -5);
    }
}