using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class UICreditsRoller : MonoBehaviour
{
	[SerializeField, Tooltip("Set speed of a rolling effect")] private float _speedPreset = 100f; // normal rolling speed
	[SerializeField, Tooltip("This is actual speed of rolling")] private float _speed = 100f; // actual speed of rolling
	[SerializeField] private bool _rollAgain = false;

	[Header("References")]
	[SerializeField] private InputReader _inputReader = default;
	[SerializeField] private RectTransform _textCredits = default;
	[SerializeField] private RectTransform _mask = default;

	public event UnityAction OnRollingEnded;
	
	private float _expectedFinishingPoint;


	public void StartRolling()
	{
		_speed = _speedPreset;
		StartCoroutine(InitialOffset()); // This offset is needed to get true information about rectangle and his mask
	}

	private void OnEnable()
	{
		_inputReader.MoveEvent += OnMove;
	}

	private void OnDisable()
	{
		_inputReader.MoveEvent -= OnMove;
	}

	void Update()
	{
		// This make rolling effect
		if (_textCredits.anchoredPosition.y < _expectedFinishingPoint)
		{
			_textCredits.anchoredPosition = new Vector2(_textCredits.anchoredPosition.x, _textCredits.anchoredPosition.y + _speed * Time.deltaTime);
		}
		else if (_expectedFinishingPoint != 0) //this happened when rolling reach to end
		{
			RollingEnd();
		}
	}

	private IEnumerator InitialOffset()
	{
		yield return new WaitForSecondsRealtime(0.02f);

		_inputReader.EnableGameplayInput();
		_expectedFinishingPoint = (_textCredits.rect.height + _mask.rect.height) / 2;

		_textCredits.anchoredPosition = new Vector2(_textCredits.anchoredPosition.x, -((_textCredits.rect.height + _mask.rect.height) / 2));
	}

	private void OnMove(Vector2 direction)
	{
		if (direction.y == 0f) // No horizontal movment
		{
			_speed = _speedPreset;
		}
		else if (direction.y > 0f) // Upward movement
		{
			_speed = _speed * 2;
		}
		else // Downward movement
		{
			_speed = -_speedPreset;
		}
	}

	private void RollingEnd()
	{
		if (_rollAgain)
		{
			// Reset position of an element
			_textCredits.anchoredPosition = new Vector2(_textCredits.anchoredPosition.x, -((_textCredits.rect.height + _mask.rect.height) / 2));
		}
		else
		{
			OnRollingEnded.Invoke();
		}
	}
}
