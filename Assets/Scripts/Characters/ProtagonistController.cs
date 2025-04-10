using System;
using UnityEngine;

/// <summary>
/// <para>This component consumes input on the InputReader and stores its values. The input is then read, and manipulated, by the StateMachines's Actions.</para>
/// </summary>
public class ProtagonistController : MonoBehaviour
{
	[SerializeField] private InputReader _inputReader = default;
	[SerializeField] private TransformEventChannelSO _transformEvent;
	[SerializeField] private Vector2EventChannelSO _onPlayerMoveDirection;
	
	private Rigidbody2D _rigidbody2D;
	private Animator _animator;
	private Vector2 _inputVector;
	private Vector2 _lastInputVector;
	private float _baseSpeed = 1f;
	private bool _isMovementBlocked = false;
	
	[NonSerialized] public bool isRunning;

	private void Start()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
	}
	
	// Adds listeners for events being triggered in the InputReader script
	private void OnEnable()
	{
		_inputReader.MoveEvent += OnMove;
		_inputReader.StoppedMoving += OnIdle;
	}

	// Removes all listeners to the events coming from the InputReader script
	private void OnDisable()
	{
		_inputReader.MoveEvent -= OnMove;
		_inputReader.StoppedMoving -= OnIdle;
	}

	private void FixedUpdate()
	{
		MovePosition();
		UpdatePlayerAnimation();
	}

	private void MovePosition()
	{
		if (_isMovementBlocked) return;
		
		_rigidbody2D.MovePosition(_rigidbody2D.position + _inputVector * _baseSpeed * Time.fixedDeltaTime);
		_transformEvent.RaiseEvent(transform);
	}

	public void BlockMovement()
	{
		_isMovementBlocked = true;
		_inputVector = Vector2.zero;     
		isRunning = false;               
		UpdatePlayerAnimation();         
	}

	public void UnlockMovement()
	{
		_isMovementBlocked = false;
	}

	private void UpdatePlayerAnimation()
	{
		if (!isRunning)
		{
			_animator.SetFloat("Speed", 0);
			_animator.SetFloat("Horizontal", _lastInputVector.x);
			_animator.SetFloat("Vertical", _lastInputVector.y);
		}
		else
		{
			_animator.SetFloat("Speed", 1);
			_animator.SetFloat("Horizontal", _inputVector.x);
			_animator.SetFloat("Vertical", _inputVector.y);
		}
	}

	//---- EVENT LISTENERS ----
	
	private void OnMove(Vector2 movement)
	{
		_inputVector = movement;
		
		// Store last state to update idle state, because _inputVector will become 0,0 when character (idle)
		_lastInputVector = _inputVector;
		isRunning = true;
		
		_onPlayerMoveDirection.RaiseEvent(movement);
	}

	private void OnIdle()
	{
		_inputVector = Vector2.zero;
		isRunning = false;
	}
}