using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [Header("Asset References")] 
    [SerializeField] private InputReader _inputReader = default;
    [SerializeField] private GameObject _playerPrefab = default;
    [SerializeField] private PathStorageSO _pathStorage = default;

    [Header("Listening To")] 
    [SerializeField] private VoidEventChannelSO _onSceneReady = default;

    [Header("Broadcasting On")] 
    [SerializeField] private VoidEventChannelSO _onPlayerSpawned;

    private Transform _defaultSpawnPoint;

    private void Awake()
    {
        _defaultSpawnPoint = transform.GetChild(0);
    }

    private void OnEnable()
    {
        _onSceneReady.OnEventRaised += SpawnPlayer;
    }

    private void OnDisable()
    {
        _onSceneReady.OnEventRaised -= SpawnPlayer;
    }

    private Transform GetSpawnLocation()
    {
        if (_pathStorage.lastPathTaken == null)
        {
            Debug.Log("No previous path recorded. Spawning at default location.");
            return _defaultSpawnPoint;
        }
        
        LocationEntrance[] entrances = FindObjectsByType<LocationEntrance>(FindObjectsSortMode.None);
        foreach (LocationEntrance entrance in entrances)
        {
            if (entrance.EntrancePath == _pathStorage.lastPathTaken)
            {
                if (entrance.spawnPointOverride != null)
                    return entrance.spawnPointOverride;
                
                Vector3 spawnPos = entrance.transform.position + entrance.transform.forward * 1.5f;
                GameObject temp = new GameObject("TempSpawnPoint");
                temp.transform.position = spawnPos;
                temp.transform.rotation = entrance.transform.rotation;

                return temp.transform;
            }
        }

        return _defaultSpawnPoint;
    }

    private void SpawnPlayer()
    {
        Transform spawnLocation = GetSpawnLocation();
        Instantiate(_playerPrefab, spawnLocation.position, spawnLocation.rotation);

        _onPlayerSpawned.RaiseEvent();
        _inputReader.EnableGameplayInput();
    }
}
