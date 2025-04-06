using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class SaveSystem : ScriptableObject
{
	[Header("Listening To")]
	[SerializeField] private VoidEventChannelSO _saveSettingsEvent;
	[SerializeField] private LoadEventChannelSO _loadLocation;

	[Header("Data")]
	[SerializeField] private ProtagonistStateSO _protagonist;
	[SerializeField] private InventorySO _playerInventory;
	[SerializeField] private QuestListSO _questListSaved;
	
	private IDataHandler _dataHandler;
	
	public string savedPath = "";
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
		_dataHandler = GameConstants.useDatabase ? new DatabaseDataHandler() : new LocalDataHandler(savedPath);
		gameData = _dataHandler.Load();
		
		// If no data can be loaded, initialize to a new game
		if (gameData == null)
		{
			Debug.Log("No data was found. Initializing data to defaults.");
			NewGame();
			return false;
		}
		
		_protagonist.LoadData(gameData);
		_playerInventory.LoadData(gameData);
		_questListSaved.LoadData(gameData);
		
		return true;
	}

	public void SaveGame()
	{
		_protagonist.SaveData(ref gameData);
		_playerInventory.SaveData(ref gameData);
		_questListSaved.SaveData(ref gameData);
		
		_dataHandler.Save(gameData);
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
