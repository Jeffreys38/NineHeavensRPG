using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Analytics;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// This class contains the function to call when play button is pressed
/// </summary>
public class StartGame : MonoBehaviour
{
	[SerializeField] private GameStateSO _gameStateManager;
	[SerializeField] private GameSceneSO _locationsToLoad;
	[SerializeField] private SaveSystem _saveSystem = default;
	[SerializeField] private InputReader _inputReader = default;
	
	[Header("Broadcasting on")]
	[SerializeField] private LoadEventChannelSO _loadLocation = default;

	[Header("Listening to")]
	[SerializeField] private VoidEventChannelSO _onNewGameButton = default;
	[SerializeField] private VoidEventChannelSO _onContinueButton = default;
	
	public SaveSystem SaveSystem => _saveSystem;
	
	private const string FileName = "GameData.json";
	private string savedPath;
	private bool _hasSaveData;

	private void Awake()
	{
		_saveSystem.savedPath = Path.Combine(Application.persistentDataPath, FileName);
	}

	private void Start()
	{
		_inputReader.EnableMenuInput();
		
		_onNewGameButton.OnEventRaised += StartNewGame;
		_onContinueButton.OnEventRaised += ContinuePreviousGame;
	}

	private void OnDestroy()
	{
		_inputReader.DisableAllInput();
		
		_onNewGameButton.OnEventRaised -= StartNewGame;
		_onContinueButton.OnEventRaised -= ContinuePreviousGame;
	}

	private void StartNewGame()
	{
		_saveSystem.LoadGame();
		_loadLocation.RaiseEvent(_locationsToLoad, true, true);
		
		_gameStateManager.UpdateGameState(GameState.Gameplay);
	}

	private void ContinuePreviousGame()
	{
		
	}
}
