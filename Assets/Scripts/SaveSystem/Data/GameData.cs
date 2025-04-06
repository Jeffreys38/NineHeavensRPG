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
    public string locationId;
    public List<SerializedItemStack> itemStacks;
    public List<string> finishedQuestItemsGUIds;
    public List<string> finishedCutSceneGUIds;
    
    // Other settings
    public Locale _currentLocale = default;

    public GameData()
    {
        itemStacks = new List<SerializedItemStack>();
        finishedQuestItemsGUIds = new List<string>() {"1667f9c06fb5f30458b95a1808670908"};
        protagonistData = new ProtagonistData();
        finishedCutSceneGUIds = new List<string>();
    }
}