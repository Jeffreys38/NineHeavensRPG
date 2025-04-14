using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SaveSystem : ScriptableObject
{
	[Header("Listening To")]
	[SerializeField] private LoadEventChannelSO _loadLocation;
	[SerializeField] private VoidEventChannelSO _onResetGameRequested;

	[Header("Data")]
	[SerializeField] private ProtagonistStateSO _protagonist;
	[SerializeField] private InventorySO _playerInventory;
	[SerializeField] private QuestListSO _questListSaved;
	
	public string saveFilename = "Save.NineHeavensRPG.json";
	public string backupSaveFilename = "Save.NineHeavensRPG.bak";
	public GameData gameData = new GameData();
	
	private string _lastSavedLocationId;
	
	void OnEnable()
	{
		Debug.Log("[SaveSystem] OnEnable called");
		
		_loadLocation.OnLoadingRequested += CacheLoadLocations;
		_onResetGameRequested.OnEventRaised += SetNewGameData;
	}

	void OnDisable()
	{
		_loadLocation.OnLoadingRequested -= CacheLoadLocations;
		_onResetGameRequested.OnEventRaised -= SetNewGameData;
	}
	
	public void SetNewGameData()
	{
		WriteEmptySaveFile();
		_playerInventory.Init();
		_questListSaved.ResetQuestlines();
		
		SaveDataToDisk();
	}
	
	public void SaveDataToDisk()
	{
		// Inventory
		gameData._itemStacks.Clear();
		foreach (var itemStack in _playerInventory.Items)
		{
			gameData._itemStacks.Add(new SerializedItemStack(itemStack.Item.Guid, itemStack.Amount));
		}
		
		// Quest
		gameData._completedQuestItemsGUIds.Clear();
		gameData._inProgressQuestItemsGUIds.Clear();
		foreach (var guid in _questListSaved.InProgressQuestGuids)
		{
			gameData._inProgressQuestItemsGUIds.Add(guid);
		}
		foreach (var guid in _questListSaved.CompletedQuestGuids)
		{
			gameData._completedQuestItemsGUIds.Add(guid);
		}
		
		// Player
		var pData = gameData._protagonistData;
		
		pData.currentHealth = _protagonist.currentHealth;
		pData.currentMana = _protagonist.currentMana;
		pData.currentAtk = _protagonist.currentAtk;
		pData.currentDefense = _protagonist.currentDefense;
		pData.currentIntelligence = _protagonist.currentIntelligence;
		pData.currentLucky = _protagonist.currentLucky;

		pData.bonusAtk = _protagonist.bonusAtk;
		pData.bonusDefense = _protagonist.bonusDefense;
		pData.bonusHealth = _protagonist.bonusHealth;
		pData.bonusMana = _protagonist.bonusMana;
		pData.bonusIntelligence = _protagonist.bonusIntelligence;
		pData.bonusLucky = _protagonist.bonusLucky;

		pData.power = _protagonist.power;
		pData.currentExp = _protagonist.currentExp;
		pData.currentPosition = _protagonist.currentPosition;

		pData.currentRealmTier = _protagonist.currentRealmTier;
		pData.currentRealmStage = _protagonist.currentRealmStage;

		// Player Skill
		gameData._protagonistData.learnedSkills.Clear();
		foreach (var skill in _protagonist.learnedSkills)
		{
			gameData._protagonistData.learnedSkills.Add(skill.Guid);
		}
		
		// Player Equipments
		gameData._protagonistData.equippedEquipments.Clear();
		foreach (var kvp in _protagonist.equippedItems)
		{
			gameData._protagonistData.equippedEquipments.Add(kvp.Value.Guid);
		}
		
		if (!FileManager.MoveFile(saveFilename, backupSaveFilename))
		{
			Debug.Log("No previous save to backup, proceeding to create a new one.");
		}

		if (FileManager.WriteToFile(saveFilename, gameData.ToJson()))
		{
			Debug.Log(gameData);
			Debug.Log("Save successful " + saveFilename);
		}
	}

	public bool LoadSaveDataFromDisk()
	{
		if (FileManager.LoadFromFile(saveFilename, out var json))
		{
			Debug.Log("1. Loading save data from disk");
			Debug.Log(json);
			Debug.Log("2. Check if json is nul or empty");
			
			if (String.IsNullOrEmpty(json))
			{
				gameData.Reset();
				Debug.Log("json is null or empty");
				Debug.Log(gameData);
				FileManager.WriteToFile(saveFilename, gameData.ToJson());
			}
			else
			{
				gameData.LoadFromJson(json);
			}
			
			return true;
		}

		return false;
	}

	public IEnumerator LoadSavedInventory()
	{
		_playerInventory.Items.Clear();
		foreach (var serializedItemStack in gameData._itemStacks)
		{
			var loadItemOperationHandle = Addressables.LoadAssetAsync<ItemSO>(serializedItemStack.itemGuid);
			yield return loadItemOperationHandle;
			if (loadItemOperationHandle.Status == AsyncOperationStatus.Succeeded)
			{
				var itemSO = loadItemOperationHandle.Result;
				_playerInventory.Add(itemSO, serializedItemStack.amount);
			}
		}
	}

	public IEnumerator LoadSavedQuestlineStatus()
	{
		_questListSaved.SetInProgressQuestlinesFromSave(gameData._inProgressQuestItemsGUIds);
		_questListSaved.SetCompletedQuestlinesFromSave(gameData._completedQuestItemsGUIds);
		yield return _questListSaved.LoadAllQuestsFromGuids();
	}

	public IEnumerator LoadSavedPlayerData()
	{
		var pData = gameData._protagonistData;
		// Base Stats
		_protagonist.currentHealth = pData.currentHealth;
		_protagonist.currentMana = pData.currentMana;
		_protagonist.currentAtk = pData.currentAtk;
		_protagonist.currentDefense = pData.currentDefense;
		_protagonist.currentIntelligence = pData.currentIntelligence;
		_protagonist.currentLucky = pData.currentLucky;

		// Bonus Stats
		_protagonist.bonusAtk = pData.bonusAtk;
		_protagonist.bonusDefense = pData.bonusDefense;
		_protagonist.bonusHealth = pData.bonusHealth;
		_protagonist.bonusMana = pData.bonusMana;
		_protagonist.bonusIntelligence = pData.bonusIntelligence;
		_protagonist.bonusLucky = pData.bonusLucky;

		// Power and EXP
		_protagonist.power = pData.power;
		_protagonist.currentExp = pData.currentExp;

		// Position
		_protagonist.currentPosition = pData.currentPosition;

		// Realm Info
		_protagonist.currentRealmTier = pData.currentRealmTier;
		_protagonist.currentRealmStage = pData.currentRealmStage;
		
		// Skill
		_protagonist.learnedSkills = new List<SkillSO>();
		foreach (var serializedSkill in gameData._protagonistData.learnedSkills)
		{
			var loadItemOperationHandle = Addressables.LoadAssetAsync<SkillSO>(serializedSkill);
			yield return loadItemOperationHandle;
			if (loadItemOperationHandle.Status == AsyncOperationStatus.Succeeded)
			{
				var skillSO = loadItemOperationHandle.Result;
				_protagonist.learnedSkills.Add(skillSO);
			}
		}
		
		// Equipment
		_protagonist.equippedItems = new Dictionary<EquipmentType, EquipmentItemSO>();
		foreach (var serializedEquipment in gameData._protagonistData.equippedEquipments)
		{
			var loadItemOperationHandle = Addressables.LoadAssetAsync<EquipmentItemSO>(serializedEquipment);
			yield return loadItemOperationHandle;
			if (loadItemOperationHandle.Status == AsyncOperationStatus.Succeeded)
			{
				var equipmentItemSO = loadItemOperationHandle.Result;
				_protagonist.equippedItems.Add(equipmentItemSO.EquipmentType, equipmentItemSO);
			}
		}
	}

	public void WriteEmptySaveFile()
	{
		FileManager.WriteToFile(saveFilename, "");
	}

	private void CacheLoadLocations(GameSceneSO locationToLoad, bool showLoadingScreen = true, bool fadeScreen = false)
	{
		LocationSO location = locationToLoad as LocationSO;
		if (location)
    	{
    		gameData._locationId = locationToLoad.Guid;
			
    		SaveDataToDisk();
    	}
	}
}
