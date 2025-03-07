using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores the list of monster spawners, tracking their respawn time and remaining monsters.
/// Used to persist spawn states across scene changes.
/// </summary>
[CreateAssetMenu(fileName = "MonsterSpawnerTracker", menuName = "Game Data/MonsterSpawnerTracker")]
public class MonsterSpawnTrackerSO : ScriptableObject, IDataPersistence
{
    [Serializable]
    public class SpawnerState
    {
        public string SpawnerGUID;      // Unique GUID for each spawner
        public int RemainingMonsters;   // Number of monsters currently alive
        public float RespawnTimeLeft;   // Remaining time before respawn
        public string SceneName;
    }

    [SerializeField] private List<SpawnerState> _spawnerStates = new List<SpawnerState>();

    /// <summary>
    /// Get the state of a spawner by its ID.
    /// If not found, return null
    /// </summary>
    public SpawnerState GetSpawnerState(string spawnerGUID)
    {
        var state = _spawnerStates.Find(s => s.SpawnerGUID == spawnerGUID);

        return state;
    }

    /// <summary>
    /// Update the state of a spawner.
    /// </summary>
    public void UpdateSpawnerState(string spawnerGUID, int remainingMonsters, float respawnTime)
    {
        var state = GetSpawnerState(spawnerGUID);
        state.RemainingMonsters = remainingMonsters;
        state.RespawnTimeLeft = respawnTime;
    }

    public void LoadData(GameData data)
    {
        var monsterSpawnerStateData = data.monsterSpawnerStateData;
        
        _spawnerStates = monsterSpawnerStateData._spawnerStates;
    }
    
    public void SaveData(ref GameData data)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Reset all spawner states, typically used when reloading a scene.
    /// </summary>
    public void ResetAllSpawners()
    {
        _spawnerStates.Clear();
    }
}