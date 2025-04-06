using UnityEngine;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] private GameSceneSO _sceneToLoad;
    
    [Header("Broadcasting on")]
    [SerializeField] private LoadEventChannelSO _loadLocation = default;

    void OnEnable()
    {
        _loadLocation.RaiseEvent(_sceneToLoad, true, true);
    }
}