using System;
using UnityEngine;

/// <summary>
/// <para>This component consumes input on the InputReader and stores its values. The input is then read, and manipulated, by the StateMachines's Actions.</para>
/// </summary>
public class ProtagonistController : MonoBehaviour
{
	[SerializeField] private InputReader _inputReader = default;
	
	private Rigidbody2D _rigidbody2D;
	private Animator _animator;
	private Vector2 _inputVector;
	private Vector2 _lastInputVector;
	private float _baseSpeed = 1f;

	//These fields are read and manipulated by the StateMachine actions
	[NonSerialized] public bool extraActionInput;
	[NonSerialized] public bool attackInput;
	[NonSerialized] public ControllerColliderHit lastHit;
	[NonSerialized] public bool isRunning; // Used when using the keyboard to run, brings the normalised speed to 1

	public const float GRAVITY_MULTIPLIER = 5f;
	public const float MAX_FALL_SPEED = -50f;
	public const float MAX_RISE_SPEED = 100f;
	public const float GRAVITY_COMEBACK_MULTIPLIER = .03f;
	public const float GRAVITY_DIVIDER = .6f;
	public const float AIR_RESISTANCE = 5f;

	private void Start()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		// lastHit = hit;
	}

	// Adds listeners for events being triggered in the InputReader script
	private void OnEnable()
	{
		_inputReader.MoveEvent += OnMove;
		_inputReader.StoppedMoving += OnIdle;
		_inputReader.AttackEvent += OnStartedAttack;
		//...
	}

	//Removes all listeners to the events coming from the InputReader script
	private void OnDisable()
	{
		_inputReader.MoveEvent -= OnMove;
		_inputReader.StoppedMoving -= OnIdle;
		_inputReader.AttackEvent -= OnStartedAttack;
		//...
	}

	private void FixedUpdate()
	{
		MovePosition();
		UpdatePlayerAnimation();
	}

	private void MovePosition()
	{
		_rigidbody2D.MovePosition(_rigidbody2D.position + _inputVector * _baseSpeed * Time.fixedDeltaTime);
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
	}

	private void OnIdle()
	{
		_inputVector = Vector2.zero;
		isRunning = false;
	}

	private void OnStartedAttack() => attackInput = true;

	// Triggered from Animation Event
	public void ConsumeAttackInput() => attackInput = false;
}