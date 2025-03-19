using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Scene UI")]
    [SerializeField] private UIInventory _inventoryPanel = default;
    
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
	    _inputReader.OpenInventoryEvent += SetInventoryScreen;
	    _inputReader.CloseInventoryEvent += CloseInventoryScreen;
    }
    
    private void OnDisable()
    {
	    _inputReader.OpenInventoryEvent -= SetInventoryScreen;
	    _inputReader.CloseInventoryEvent -= CloseInventoryScreen;
    }
    
    private void SetInventoryScreen()
    {
	    if (_gameStateManager.CurrentGameState == GameState.Gameplay)
	    {
		    OpenInventoryScreen();
	    }
    }

    private void OpenInventoryScreen()
    {
	    _inventoryPanel.gameObject.SetActive(true);
	    _inputReader.EnableMenuInput();
	    _gameStateManager.UpdateGameState(GameState.Inventory);
    }
	
    public void CloseInventoryScreen()
    {
	    _inventoryPanel.gameObject.SetActive(false);
	    _inputReader.EnableGameplayInput();
	    _gameStateManager.UpdateGameState(GameState.Gameplay);
    }
}