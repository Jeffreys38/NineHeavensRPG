using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Analytics;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// This class contains the function to call when play button is pressed
/// </summary>
public class StartGame : MonoBehaviour
{
	[SerializeField] private GameSceneSO _locationsToLoad;
	[SerializeField] private SaveSystem _saveSystem = default;
	
	[Header("Broadcasting on")]
	[SerializeField] private LoadEventChannelSO _loadLocation = default;

	[Header("Listening to")]
	[SerializeField] private VoidEventChannelSO _onNewGameButton = default;
	[SerializeField] private VoidEventChannelSO _onContinueButton = default;

	private readonly IDataHandler _dataHandler = GameConstants.useDatabase ? new DatabaseDataHandler() : new LocalDataHandler();
	private bool _hasSaveData;

	private void Start()
	{
		_onNewGameButton.OnEventRaised += StartNewGame;
		_onContinueButton.OnEventRaised += ContinuePreviousGame;
	}

	private void OnDestroy()
	{
		_onNewGameButton.OnEventRaised -= StartNewGame;
		_onContinueButton.OnEventRaised -= ContinuePreviousGame;
	}

	private void StartNewGame()
	{
		_hasSaveData = _saveSystem.LoadGame(_dataHandler);
		_loadLocation.RaiseEvent(_locationsToLoad, true, true);
	}

	private void ContinuePreviousGame()
	{
		
	}
}
