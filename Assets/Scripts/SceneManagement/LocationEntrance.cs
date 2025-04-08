using UnityEngine;

public class LocationEntrance : MonoBehaviour
{
    [SerializeField] private GameSceneSO _sceneToLoad;
    [SerializeField] private PathSO _entrancePath;
    [SerializeField] private PathStorageSO _pathStorage = default; //This is where the last path taken has been stored
    
    [Header("Listening to")]
    [SerializeField] private VoidEventChannelSO _onSceneReady;
    
    [Header("Broadcasting on")]
    [SerializeField] private LoadEventChannelSO _loadLocation = default;
    
    [Header("Optional")]
    public Transform spawnPointOverride;
    
    public PathSO EntrancePath => _entrancePath;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _pathStorage.lastPathTaken = _entrancePath;
            _loadLocation.RaiseEvent(_sceneToLoad, true, true);
        }
    }
}