using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the spawning of monsters at predefined locations based on MonsterSpawnerDataSO.
/// Attached to MonsterSpawnerManager GameObject
/// </summary>
public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private MonsterSpawnerDataSO _spawnerData;
    private List<GameObject> _activeMonsters = new List<GameObject>();
    private bool _isSpawning = false;

    public MonsterSpawnerDataSO SpawnerData => _spawnerData;

    private void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    private IEnumerator SpawnMonsters()
    {
        while (true)
        {
            if (_activeMonsters.Count < _spawnerData.MaxMonsters)
            {
                int spawnIndex = _activeMonsters.Count % _spawnerData.SpawnPositions.Count;
                Vector3 spawnPosition = _spawnerData.SpawnPositions[spawnIndex];

                GameObject monster = Instantiate(_spawnerData.MonsterPrefab.PrefabReview, spawnPosition, Quaternion.identity);
                _activeMonsters.Add(monster);
            }

            yield return new WaitForSeconds(_spawnerData.RespawnTime);
        }
    }
}