using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

/// <summary>
/// This class contains all the variables that will be serialized and saved to a file.<br/>
/// Can be considered as a save file structure or format.
/// </summary>
[Serializable]
public class GameData
{
    public ProtagonistData _protagonistData;
    public List<SerializedItemStack> _itemStacks;
    public List<string> _finishedQuestItemsGUIds;
    public List<string> _finishedCutSceneGUIds;
    public string _locationId;

    public void Reset()
    {
        _protagonistData = new ProtagonistData
        {
            currentHealth = 100000,
            currentMana = 100000,
            currentIntelligence = 1000f,
            currentLucky = 1000f,
            currentRealmTier = RealmTier.Mortal,
            currentRealmStage = RealmStage.Early,
            currentExp = 0,
            currentPosition = Vector3.zero,
            equippedEquipments = new List<string>(),
            learnedSkills = new List<string>(),
            power = 0
        };

        _itemStacks = new List<SerializedItemStack>();
        _finishedQuestItemsGUIds = new List<string>();
        _finishedCutSceneGUIds = new List<string>();
        _locationId = "1bb1ff3615216f7408fa0c12d52cf7e6";
    }
    
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}