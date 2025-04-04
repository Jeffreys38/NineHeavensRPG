﻿using System;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
	[Header("Asset References")]
	[SerializeField] private InputReader _inputReader = default;
	[SerializeField] private GameObject _playerPrefab = default;
	
	[Header("Scene Ready Event")]
	[SerializeField] private VoidEventChannelSO _onSceneReady = default; // Raised by SceneLoader when the scene is set to active
	[SerializeField] private VoidEventChannelSO _onPlayerSpawned;
	
	private LocationEntrance[] _spawnLocations;
	private Transform _defaultSpawnPoint;

	private void Awake()
	{
		// _spawnLocations = GameObject.FindObjectsOfType<LocationEntrance>();
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
		//Look for the element in the available LocationEntries that matches tha last PathSO taken
		return _defaultSpawnPoint;
	}

	private void SpawnPlayer()
	{
		Transform spawnLocation = GetSpawnLocation();
		Instantiate(_playerPrefab, spawnLocation.position, spawnLocation.rotation);
		
		_onPlayerSpawned.RaiseEvent();
		
		//TODO: Probably move this to the GameManager once it's up and running
		_inputReader.EnableGameplayInput();
	}
}
