using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores data for a monster spawner, including the number of monsters, spawn positions, 
/// respawn timers, and associated monster types. This data persists across game sessions 
/// to maintain consistent spawn behavior.
/// </summary>
[CreateAssetMenu(fileName = "NewMonsterSpawnerData", menuName = "Game Data/MonsterSpawnerData")]
public class MonsterSpawnerDataSO : SerializableScriptableObject
{ 
    [SerializeField] private EntitySO _monsterPrefab;        // Monster spawn
    [SerializeField] private List<Vector3> _spawnPositions;  // Positions where monsters spawn
    [SerializeField] private int _maxMonsters;               // Maximum number of monsters at this spawner
    [SerializeField] private float _respawnTime;             // Time before monsters respawn after being defeated
    
    public EntitySO MonsterPrefab => _monsterPrefab;
    public List<Vector3> SpawnPositions => _spawnPositions;
    public int MaxMonsters => _maxMonsters;
    public float RespawnTime => _respawnTime;
    
    /// <summary>
    /// Clears the current spawn positions and updates them based on child objects of a given spawner.
    /// This should be called in the Editor to auto-assign spawn positions.
    /// </summary>
    public void UpdateSpawnPositions(Transform spawnerTransform)
    {
        _spawnPositions.Clear();
        foreach (Transform child in spawnerTransform)
        {
            _spawnPositions.Add(child.position);
        }
    }
}