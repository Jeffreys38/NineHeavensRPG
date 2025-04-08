using System;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO _onSceneReady = default;
    [SerializeField] private GameStateSO _gameState = default;

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
        if (_gameState.CurrentGameState == GameState.Gameplay)
        {
            canvasGameplay.SetActive(true);
        }
        else
        {
            HideHUD();
        }
    }
    
    private void HideHUD()
    {
        canvasGameplay.SetActive(false);
    }
}