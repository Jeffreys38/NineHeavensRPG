using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SaveSystem : ScriptableObject
{
	[Header("Listening To")]
	[SerializeField] private VoidEventChannelSO _saveSettingsEvent;
	[SerializeField] private LoadEventChannelSO _loadLocation;

	[SerializeField] private ProtagonistStateSO _protagonist;
	[SerializeField] private MonsterSpawnTrackerSO _monsterSpawnTracker;
	[SerializeField] private InventorySO _playerInventory;
	[SerializeField] private SettingsSO _currentSettings;
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
		
		//_protagonist.LoadData(gameData);
		// _protagonist.learnedSkills = LoadSkills(gameData.protagonistData.learnedSkills);
		// _monsterSpawnTracker.LoadData(gameData);
		
		return true;
	}
	
	private List<SkillSO> LoadSkills(List<string> skillGuids)
	{
		if (skillGuids == null) return new List<SkillSO>();

		List<SkillSO> skills = new List<SkillSO>();
		foreach (string guid in skillGuids)
		{
			SkillSO skill = LoadAssetByGuid(guid, typeof(SkillSO)) as SkillSO;
			if (skill != null)
				skills.Add(skill);
		}
		return skills;
	}
	
	private object LoadAssetByGuid(string guid, Type type)
	{
#if UNITY_EDITOR
		string pathToAsset = AssetDatabase.GUIDToAssetPath(guid);

		return AssetDatabase.LoadAssetAtPath(pathToAsset, type);
#endif
		return null;
	}

	public void SaveGame()
	{
		_protagonist.SaveData(ref gameData);
		_monsterSpawnTracker.SaveData(ref gameData);
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
