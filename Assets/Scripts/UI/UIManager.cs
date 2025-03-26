using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Scene UI")]
    [SerializeField] private GameObject _inGameScreen = default;
    [SerializeField] private UIInventory _inventoryPanel = default;
    [SerializeField] private UIDialogue _dialoguePanel = default;
	 
    [Header("Gameplay")]
    [SerializeField] private GameStateSO _gameStateManager = default;
    [SerializeField] private InputReader _inputReader = default;
    
    [Header("Listening on")]
    [SerializeField] private VoidEventChannelSO _onSceneReady = default;
    [SerializeField] private DialogueEventChannelSO _onGainedDialogue;
    [SerializeField] private VoidEventChannelSO _onEndedDialogue;
    
    [Header("Broadcasting on")]
    [SerializeField] private LoadEventChannelSO _loadMenuEvent = default;
    [SerializeField] private VoidEventChannelSO _onInteractionEndedEvent = default;
    [SerializeField] private DialogueEventChannelSO _startDialogueEvent;
    
    private void OnEnable()
    {
	    _onSceneReady.OnEventRaised += ShowHUD;
	    
	    _inputReader.OpenInventoryEvent += SetInventoryScreen;
	    _inputReader.CloseInventoryEvent += CloseInventoryScreen;
	    
	    _onGainedDialogue.OnEventRaised += StartDialogue;
	    _onEndedDialogue.OnEventRaised += HideDialogue;
    }
    
    private void OnDisable()
    {
	    _onSceneReady.OnEventRaised -= ShowHUD;
	    
	    _inputReader.OpenInventoryEvent -= SetInventoryScreen;
	    _inputReader.CloseInventoryEvent -= CloseInventoryScreen;
		
	    _onGainedDialogue.OnEventRaised -= StartDialogue;
	    _onEndedDialogue.OnEventRaised -= HideDialogue;
    }

    private void ShowHUD()
    {
	    _inGameScreen.gameObject.SetActive(true);
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
	
    private void CloseInventoryScreen()
    {
	    _inventoryPanel.gameObject.SetActive(false);
	    _inputReader.EnableGameplayInput();
	    _gameStateManager.UpdateGameState(GameState.Gameplay);
    }

    private void StartDialogue(DialogueDataSO dialogueData)
    {
	    ShowDialogue();
	    
	    _startDialogueEvent.RaiseEvent(dialogueData);
    }
    
    private void ShowDialogue()
    {
	    _inputReader.EnableDialogueInput();
	    _dialoguePanel.gameObject.SetActive(true);
	    _gameStateManager.UpdateGameState(GameState.Dialogue);
    }
     
    private void HideDialogue()
    {
	    _inputReader.EnableGameplayInput();
	    _dialoguePanel.gameObject.SetActive(false);
	    _gameStateManager.UpdateGameState(GameState.Gameplay);
    }
}