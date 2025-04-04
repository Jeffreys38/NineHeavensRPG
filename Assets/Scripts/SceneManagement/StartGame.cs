﻿using System.Collections;
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
	
	private bool _hasSaveData;

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
