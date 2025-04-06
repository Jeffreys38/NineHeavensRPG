using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Listening To")] 
    [SerializeField] private VoidEventChannelSO _onSceneReady;
    [SerializeField] private VoidEventChannelSO _onPlayerSpawned;

    public CinemachineCamera cinemachineCamera;

    private void OnEnable()
    {
        _onSceneReady.OnEventRaised += AttachTilemap;
        _onPlayerSpawned.OnEventRaised += AttachPlayer;
    }
    
    private void OnDisable()
    {
        _onSceneReady.OnEventRaised -= AttachTilemap;
        _onPlayerSpawned.OnEventRaised -= AttachPlayer;
    }

    private void AttachPlayer()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            cinemachineCamera.Target.TrackingTarget = player.transform;
        }
    }
    
    private void AttachTilemap()
    {
        
    }
}