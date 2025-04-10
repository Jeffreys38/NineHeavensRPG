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
    public ProtagonistData protagonistData;
    public List<SerializedItemStack> itemStacks;
    public List<string> finishedQuestItemsGUIds;
    public List<string> finishedCutSceneGUIds;
    public string lastMapGUIds = "1bb1ff3615216f7408fa0c12d52cf7e6";

    public GameData()
    {
        protagonistData = new ProtagonistData
        {
            currentHealth = 100000,
            currentMana = 100000,
            currentIntelligence = 10f,
            currentLucky = 5f,
            currentRealmTier = RealmTier.Mortal,
            currentRealmStage = RealmStage.Early,
            currentExp = 0,
            currentPosition = Vector3.zero,
            equippedEquipments = new List<string>(),
            learnedSkills = new List<string>(),
            power = 100000
        };

        itemStacks = new List<SerializedItemStack>();
        finishedQuestItemsGUIds = new List<string>();
        finishedCutSceneGUIds = new List<string>();
        lastMapGUIds = "1bb1ff3615216f7408fa0c12d52cf7e6";
    }
}