using UnityEngine;

public class SaveSystem : ScriptableObject
{
	[Header("Listening To")]
	[SerializeField] private VoidEventChannelSO _saveSettingsEvent;
	[SerializeField] private LoadEventChannelSO _loadLocation;

	[Header("Data")]
	[SerializeField] private ProtagonistStateSO _protagonist;
	[SerializeField] private InventorySO _playerInventory;
	
	private readonly IDataHandler _dataHandler = GameConstants.useDatabase ? new DatabaseDataHandler() : new LocalDataHandler();

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

	public bool LoadGame()
	{
		gameData = _dataHandler.Load();
		
		// If no data can be loaded, initialize to a new game
		if (gameData == null)
		{
			Debug.Log("No data was found. Initializing data to defaults.");
			NewGame();
			return false;
		}
		
		(_protagonist as IDataPersistence)?.LoadData(gameData);
		(_playerInventory as IDataPersistence)?.LoadData(gameData);
		
		return true;
	}

	private void SaveGame()
	{
		_protagonist.SaveData(ref gameData);
	}

	private void SaveSettings()
	{
	
	}

	private void CacheLoadLocations(GameSceneSO locationToLoad, bool showLoadingScreen = true, bool fadeScreen = false)
	{
		LocationSO locationSO = locationToLoad as LocationSO;
		if (locationSO)
		{
			gameData.locationId = locationSO.Guid;
		}

		// SaveGame();
	}
}
