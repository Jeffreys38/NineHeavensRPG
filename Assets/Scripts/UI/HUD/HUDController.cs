using System;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO _onSceneReady = default;

    public GameObject canvasGameplay;

    private void OnEnable()
    {
        _onSceneReady.OnEventRaised += ShowHUD;
    }

    private void OnDisable()
    {
        _onSceneReady.OnEventRaised -= ShowHUD;
    }

    private void ShowHUD()
    {
        canvasGameplay.SetActive(true);
    }
}