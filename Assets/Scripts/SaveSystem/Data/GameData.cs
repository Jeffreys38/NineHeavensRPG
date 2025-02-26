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
    // Protagonist state: current state, list of learned skill
    public ProtagonistStateSO _protagonist = default;
    
    // Used to load scene
    public string _locationId;
    
    // Items in the protagonist's inventory
    public List<SerializedItemStack> _itemStacks = new List<SerializedItemStack>();
    
    // List of GUIDs for completed questline items
    // Each entry in this list is a unique string representing a completed quest.  
    public List<string> _finishedQuestlineItemsGUIds = new List<string>();
    
    // Other settings
    public float _masterVolume = default;
    public float _musicVolume = default;
    public float _sfxVolume = default;
    public int _resolutionsIndex = default;
    public int _antiAliasingIndex = default;
    public float _shadowDistance = default;
    public bool _isFullscreen = default;
    public Locale _currentLocale = default;
    
    public void SaveSettings(SettingsSO settings)
    {
        _masterVolume = settings.MasterVolume;
        _musicVolume = settings.MusicVolume;
        _sfxVolume = settings.SfxVolume;
        _resolutionsIndex = settings.ResolutionsIndex;
        _antiAliasingIndex = settings.AntiAliasingIndex;
        _shadowDistance = settings.ShadowDistance;
        _isFullscreen = settings.IsFullscreen;
        _currentLocale = settings.CurrentLocale;
    }
}