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
	[SerializeField] private CutsceneEventChannelSO _cutsceneEvent = default;

	[Header("Listening to")]
	[SerializeField] private VoidEventChannelSO _onNewGameButton = default;
	
	public SaveSystem SaveSystem => _saveSystem;
	
	private const string FileName = "Immortal.json";
	private string savedPath;
	private bool _hasSaveData;

	private void Awake()
	{
		_saveSystem.savedPath = Path.Combine(Application.persistentDataPath, FileName);
	}

	private void Start()
	{
		_inputReader.EnableMenuInput();
		
		_onNewGameButton.OnEventRaised += ContinueGame;
	}

	private void OnDestroy()
	{
		_inputReader.DisableAllInput();
		
		_onNewGameButton.OnEventRaised -= ContinueGame;
	}

	void ContinueGame()
	{
		StartCoroutine(StartNewGame());
	}

	private IEnumerator StartNewGame()
	{
		_saveSystem.LoadGame();
		yield return new WaitForSeconds(1.5f);
		_cutsceneEvent.RaiseEvent(_saveSystem.Protagonist.lastScene);
	}
}
