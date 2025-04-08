using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class SaveSystem : ScriptableObject
{
	[Header("Listening To")]
	[SerializeField] private LoadEventChannelSO _loadLocation;

	[Header("Data")]
	[SerializeField] private ProtagonistStateSO _protagonist;
	[SerializeField] private InventorySO _playerInventory;
	[SerializeField] private QuestListSO _questListSaved;
	
	public ProtagonistStateSO Protagonist => _protagonist;
	
	private IDataHandler _dataHandler;
	
	public string savedPath = "";
	public GameData gameData;
	
	void OnEnable()
	{
		_loadLocation.OnLoadingRequested += CacheLoadLocations;
	}

	void OnDisable()
	{
		_loadLocation.OnLoadingRequested -= CacheLoadLocations;
	}
	
	public void NewGame()
	{
		gameData = new GameData();
	}

	public bool LoadGame()
	{
		_dataHandler = GameConstants.useDatabase 
			? new DatabaseDataHandler() 
			: new LocalDataHandler(savedPath);
		
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
		try
		{
			Debug.Log("Saving protagonist...");
			_protagonist.SaveData(ref gameData);

			Debug.Log("Saving inventory...");
			_playerInventory.SaveData(ref gameData);

			Debug.Log("Saving quests...");
			_questListSaved.SaveData(ref gameData);

			Debug.Log("Writing to file...");
			_dataHandler.Save(gameData);

			Debug.Log("<color=green>SaveGame finished successfully.</color>");
		}
		catch (System.Exception ex)
		{
			Debug.LogError("Exception during SaveGame: " + ex.Message);
			Debug.LogError("StackTrace:\n" + ex.StackTrace);
		}
	}

	private void CacheLoadLocations(GameSceneSO locationToLoad, bool showLoadingScreen = true, bool fadeScreen = false)
	{
		if (locationToLoad)
		{
			gameData.lastMapGUIds = locationToLoad.Guid;
		}
		
		SaveGame();
	}
}
