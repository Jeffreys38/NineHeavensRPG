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
	public event UnityAction<int> AttackEvent = delegate { };
	public event UnityAction<Vector2> ChoosePositionEvent = delegate { };
	public event UnityAction<Vector2> MoveEvent = delegate { };
	public event UnityAction StoppedMoving = delegate { };
	public event UnityAction<MonsterHealth> PickTarget = delegate { };
	
	// Menus
	public event UnityAction OpenInventoryEvent;
	public event UnityAction CloseInventoryEvent;
	public event UnityAction StartDragItemEvent;
	public event UnityAction DropItemEvent;
	
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
		if (context.performed)
		{
			int numKeyValue; 
			
			// Warning! If ctx.control.name can't parse as an int, numKeyValue will be 0
			int.TryParse(context.control.name, out numKeyValue);
			
			AttackEvent.Invoke(numKeyValue);
		}
	}

	void GameInput.IGameplayActions.OnClick(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			Vector2 screenPosition = Mouse.current.position.ReadValue();
			Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
			
			ChoosePositionEvent.Invoke(worldPosition);
			
			// If a target is selected
			Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition);
			if (hitCollider != null)
			{
				MonsterHealth entity = hitCollider.GetComponent<MonsterHealth>();
				PickTarget.Invoke(entity);
			}
			else
			{
				PickTarget.Invoke(null);
			}
		}
	}

	// --- Inventory Controls ---
	public void OnOpenInventory(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
		{
			OpenInventoryEvent.Invoke();
		}
	}
    
    public void OnCloseInventory(InputAction.CallbackContext context)
    {
    	if (context.phase == InputActionPhase.Performed)
    	{
    		CloseInventoryEvent.Invoke();
    	}
    }

	public void OnDragItem(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			isDraggingItem = true;
			StartDragItemEvent?.Invoke();
		}
	}

	public void OnDropItem(UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			isDraggingItem = false;
			DropItemEvent?.Invoke();
		}
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		Vector2 move = context.ReadValue<Vector2>();
		
		if (move != Vector2.zero) MoveEvent.Invoke(move);
		else StoppedMoving.Invoke();
	}
}
