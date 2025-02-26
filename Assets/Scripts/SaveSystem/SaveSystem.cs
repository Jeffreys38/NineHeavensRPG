using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SaveSystem : ScriptableObject
{
	[Header("Listening To")]
	[SerializeField] private VoidEventChannelSO _saveSettingsEvent = default;
	[SerializeField] private LoadEventChannelSO _loadLocation = default;
	
	[SerializeField] private ProtagonistStateSO _playerState = default;
	[SerializeField] private InventorySO _playerInventory = default;
	[SerializeField] private SettingsSO _currentSettings = default;
	// [SerializeField] private QuestManagerSO _questManagerSO = default;

	public GameData gameData;
	
	void OnEnable()
	{
		_saveSettingsEvent.OnEventRaised += SaveSettings;
		_loadLocation.OnLoadingRequested += CacheLoadLocations;
	}

	void OnDisable()
	{
		_saveSettingsEvent.OnEventRaised -= SaveSettings;
		_loadLocation.OnLoadingRequested -= CacheLoadLocations;
	}

	public void NewGame()
	{
		gameData = new GameData();
	}

	public bool LoadGame(IDataHandler dataHandler)
	{
		// If no data can be loaded, initialize to a new game
		if (gameData == null)
		{
			Debug.Log("No data was found. Initializing data to defaults.");
			NewGame();
			return false;
		}

		Debug.Log("Loaded successfully.");
		return true;
	}
	
	public void SaveGame()
	{
		_playerState.SaveData(ref gameData);
	}
	
	private void SaveSettings()
	{
		gameData.SaveSettings(_currentSettings);
	}

	private void CacheLoadLocations(GameSceneSO locationToLoad, bool showLoadingScreen = true, bool fadeScreen = false)
	{
		LocationSO locationSO = locationToLoad as LocationSO;
		if (locationSO)
		{
			gameData._locationId = locationSO.Guid;
		}

		SaveGame();
	}

	private void OnApplicationQuit()
	{
		SaveGame();
	}
}
