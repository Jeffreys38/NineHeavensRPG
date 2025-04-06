// using Cinemachine;

using System;
using System.Collections;
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
    
    public PathSO EntrancePath => _entrancePath;
    
    private void Awake()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _loadLocation.RaiseEvent(_sceneToLoad, true, true);
        }
    }

    private void PlanTransition()
    {
        StartCoroutine(TransitionToGameCamera());
    }
    
    private IEnumerator TransitionToGameCamera()
    {
    
        yield return new WaitForSeconds(.1f);
    
        // entranceShot.Priority = -1;
        _onSceneReady.OnEventRaised -= PlanTransition;
    }
}