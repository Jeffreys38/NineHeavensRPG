using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private BoolEventChannelSO _setHUDStatus = default;
    [SerializeField] private VoidEventChannelSO _onSceneReady = default;
    [SerializeField] private GameStateSO _gameState = default;

    public GameObject canvasGameplay;

    private void OnEnable()
    {
        _setHUDStatus.OnEventRaised += ShowHUD;
        _onSceneReady.OnEventRaised += InitializedHUD;
    }

    private void OnDisable()
    {
        _setHUDStatus.OnEventRaised -= ShowHUD;
        _onSceneReady.OnEventRaised -= InitializedHUD;
    }

    private void InitializedHUD()
    {
        canvasGameplay.SetActive(true);
    }

    private void ShowHUD(bool value = true)
    {
        canvasGameplay.SetActive(value);
    }
}