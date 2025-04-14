using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
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
	[SerializeField] private SaveSystem _saveSystem;
	[SerializeField] private InputReader _inputReader;

	[Header("Broadcasting on")] 
	[SerializeField] private LoadEventChannelSO _loadLocation = default;
	[SerializeField] private CutsceneEventChannelSO _cutsceneEvent = default;

	[Header("Listening to")] 
	[SerializeField] private VoidEventChannelSO _onNewGameButton = default;

	private bool _hasSaveData;

	private void Start()
	{
		_inputReader.EnableMenuInput();
		_hasSaveData = _saveSystem.LoadSaveDataFromDisk();
		_onNewGameButton.OnEventRaised += ContinuePreviousGame;
	}

	private void OnDestroy()
	{
		_inputReader.DisableAllInput();
		_onNewGameButton.OnEventRaised -= ContinuePreviousGame;
	}

	void StartNewGame()
	{
		_hasSaveData = false;

		_saveSystem.WriteEmptySaveFile();
		_saveSystem.SetNewGameData();
	}

	void ContinuePreviousGame()
	{
		StartCoroutine(LoadSaveGame());
	}
	
	private IEnumerator LoadSaveGame() {
		yield return StartCoroutine(_saveSystem.LoadSavedPlayerData());
		yield return StartCoroutine(_saveSystem.LoadSavedInventory());
		yield return StartCoroutine(_saveSystem.LoadSavedQuestlineStatus());
		
		var asyncOperationHandle = Addressables.LoadAssetAsync<GameSceneSO>(_saveSystem.gameData._locationId);
		yield return asyncOperationHandle;

		if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
		{
			GameSceneSO gameSceneSO = asyncOperationHandle.Result;
			Debug.Log("Game Data at LoadSaveGame: " + _saveSystem.gameData);
			_cutsceneEvent.RaiseEvent(gameSceneSO);
		}
	}
}
