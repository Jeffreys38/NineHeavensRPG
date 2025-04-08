using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public enum GameState
{
	Gameplay, // regular state: player moves, attacks, can perform actions
	Pause, // pause menu is opened, the whole game world is frozen
	Inventory,
	Dialogue,
	Cutscene,
	LocationTransition, // when the character steps into LocationExit trigger, fade to black begins and control is removed from the player
	Combat, // enemy is nearby and alert, player can't open Inventory or initiate dialogues, but can pause the game
}

// [CreateAssetMenu(fileName = "GameState", menuName = "Gameplay/GameState", order = 51)]
public class GameStateSO : DescriptionBaseSO
{
	public GameState CurrentGameState => _currentGameState;
	
	[Header("Game states")]
	[SerializeField][ReadOnly] private GameState _currentGameState = default;
	[SerializeField][ReadOnly] private GameState _previousGameState = default;

	[Header("Broadcasting on")]
	[SerializeField] private BoolEventChannelSO _onCombatStateEvent = default;
	
	public void UpdateGameState(GameState newGameState)
	{
		if (newGameState == CurrentGameState)
			return;

		if (newGameState == GameState.Combat)
		{
			_onCombatStateEvent.RaiseEvent(true);
		}
		else
		{
			_onCombatStateEvent.RaiseEvent(false);
		}

		_previousGameState = _currentGameState;
		_currentGameState = newGameState;
	}
}
