using UnityEngine;

public class UIManager
{
    [Header("Scene UI")]
    // [SerializeField] private UIInventory _inventoryPanel = default;
    
    [Header("Gameplay")]
    [SerializeField] private GameStateSO _gameStateManager = default;
    [SerializeField] private InputReader _inputReader = default;
    
    [Header("Listening on")]
    [SerializeField] private VoidEventChannelSO _onSceneReady = default;
    
    
    [Header("Broadcasting on")]
    [SerializeField] private LoadEventChannelSO _loadMenuEvent = default;
    [SerializeField] private VoidEventChannelSO _onInteractionEndedEvent = default;

    private void OnEnable()
    {
        
    }
    
    private void OnDisable()
    {
        
    }
    
    
}