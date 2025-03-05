using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SaveSystem : ScriptableObject
{
	[Header("Listening To")]
	[SerializeField] private VoidEventChannelSO _saveSettingsEvent = default;
	[SerializeField] private LoadEventChannelSO _loadLocation = default;
	
	[SerializeField] private ProtagonistStateSO _protagonist = default;
	[SerializeField] private InventorySO _playerInventory = default;
	[SerializeField] private SettingsSO _currentSettings = default;
	[SerializeField] private GameSceneSO _savedLocation;
	
	[Header("References")]
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
        
		// Data of player
		List<string> skillGuids = gameData.protagonistData.learnedSkills;
		// List<SkillSO> skills = new List<SkillSO>();
		// foreach (var guid in skillGuids)
		// {
		// 	var skill = LoadAssetByGuid(guid, typeof(SkillSO)) as SkillSO;
		// 	skills.Add(skill);
		// }
		_protagonist.SetData(gameData.protagonistData);
		//_protagonist.LoadSkills(skills);
		
		// Data of inventory
		
		return true;
	}
	
	private object LoadAssetByGuid(string guid, Type type)
	{
		string pathToAsset = AssetDatabase.GUIDToAssetPath(guid);

		return AssetDatabase.LoadAssetAtPath(pathToAsset, type);
	}

	public void SaveGame()
	{
		// Save SO to game data
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
			gameData.locationId = locationSO.Guid;
		}

		// SaveGame();
	}

	private void OnApplicationQuit()
	{
		SaveGame();
	}
}
