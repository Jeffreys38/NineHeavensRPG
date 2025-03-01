using UnityEditor;
using UnityEngine;

public class SaveSystem : ScriptableObject
{
	[Header("Listening To")]
	[SerializeField] private VoidEventChannelSO _saveSettingsEvent = default;
	[SerializeField] private LoadEventChannelSO _loadLocation = default;
	
	[SerializeField] private SettingsSO _currentSettings = default;
	[SerializeField] private ProtagonistStateSO _defaultProtagonist = default;
	[SerializeField] private ProtagonistStateSO _protagonist = default;
	// [SerializeField] private QuestManagerSO _questManagerSO = default;
	
	private readonly IDataHandler _dataHandler = GameConstants.useDatabase ? new DatabaseDataHandler() : new LocalDataHandler();
	
	public GameData gameData;
	public GameSceneSO savedLocation;
	
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
		_protagonist = ScriptableObject.Instantiate(_defaultProtagonist);
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

		//savedLocation = LoadScriptableObjectByGUID<GameSceneSO>(gameData._locationId);
		//_protagonist = LoadScriptableObjectByGUID<ProtagonistStateSO>(gameData._protagonistId);
		
		return true;
	}
	
	// private T LoadScriptableObjectByGUID<T>(string guid) where T : ScriptableObject
	// {
	// 	string path = AssetDatabase.GUIDToAssetPath(guid);
	// 	if (string.IsNullOrEmpty(path))
	// 	{
	// 		Debug.LogError($"Invalid GUID: {guid}");
	// 		return null;
	// 	}
	// 	return AssetDatabase.LoadAssetAtPath<T>(path);
	// } 
	
	public void SaveGame()
	{
		gameData._protagonistId = _protagonist.Guid;
		// 
		
		_dataHandler.Save(gameData);
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

		// SaveGame();
	}

	private void OnApplicationQuit()
	{
		SaveGame();
	}
}
