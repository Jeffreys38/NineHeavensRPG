using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : DescriptionBaseSO, GameInput.IGameplayActions, GameInput.IMenusActions
{
	[Space]
	[SerializeField] private GameStateSO _gameStateManager;
	
	// Assign delegate{} to events to initialise them with an empty delegate
	// so we can skip the null check when we use them

	// Gameplay
	public event UnityAction AttackEvent = delegate { };
	public event UnityAction<Vector2> MoveEvent = delegate { };
	public event UnityAction StoppedMoving = delegate { };
	
	// Menus
	public UnityAction OnOpenInventoryEvent;
	public UnityAction OnCloseInventoryEvent;
	public UnityAction OnStartDragItemEvent;
	public UnityAction OnDropItemEvent;
	
	private bool isInventoryOpen = false;
	private bool isDraggingItem = false;

	private GameInput _gameInput;

	private void OnEnable()
	{
		if (_gameInput == null)
		{
			_gameInput = new GameInput();

			_gameInput.Menus.SetCallbacks(this);
			_gameInput.Gameplay.SetCallbacks(this);
			// _gameInput.Dialogues.SetCallbacks(this);
		}
	}

	private void OnDisable()
	{
		DisableAllInput();
	}
	
	public void EnableDialogueInput()
	{
		// _gameInput.Menus.Enable();
		// _gameInput.Gameplay.Disable();
		// _gameInput.Dialogues.Enable();
	}

	public void EnableGameplayInput()
	{
		_gameInput.Menus.Disable();
		// _gameInput.Dialogues.Disable();
		_gameInput.Gameplay.Enable();
	}

	public void EnableMenuInput()
	{
		// _gameInput.Dialogues.Disable();
		_gameInput.Gameplay.Disable();
		_gameInput.Menus.Enable();
	}

	public void DisableAllInput()
	{
		_gameInput.Gameplay.Disable();
		_gameInput.Menus.Disable();
		// _gameInput.Dialogues.Disable();
	}

	public void OnAttack(InputAction.CallbackContext context)
	{
		switch (context.phase)
		{
			case InputActionPhase.Performed:
				AttackEvent.Invoke();
				break;
		}
	}

	public void OnClick(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			Debug.Log("OnClick");
		}
	}

	public void OnPoint(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			Debug.Log("OnPoint");
		}
	}

	// --- Inventory Controls ---
	public void OnOpenInventory(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			isInventoryOpen = !isInventoryOpen;
			if (isInventoryOpen)
			{
				EnableMenuInput();
				OnOpenInventoryEvent?.Invoke();
			}
			else
			{
				EnableGameplayInput();
				OnCloseInventoryEvent?.Invoke();
			}
		}
	}

	public void OnDragItem(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			isDraggingItem = true;
			OnStartDragItemEvent?.Invoke();
		}
	}

	public void OnDropItem(UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			isDraggingItem = false;
			OnDropItemEvent?.Invoke();
		}
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		Vector2 move = context.ReadValue<Vector2>();
		
		if (move != Vector2.zero) MoveEvent.Invoke(move);
		else StoppedMoving.Invoke();
	}
}
