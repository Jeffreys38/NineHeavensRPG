using System;
using System.Collections;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    [Header("Listening To")]
    [SerializeField] private InputReader _inputReader;

    [Header("Broadcasting On")]
    [SerializeField] private AudioCueEventChannelSO _sfxEventChannel = default;
    [SerializeField] private AudioCueSO _SFX = default;
    [SerializeField] private AudioConfigurationSO _SFXConfiguration = default;

    public SkillSO skillSO;

    private AudioCueKey _audioCueKey;
    private Animator _animator;
    private Vector2 _selectedRange;

    private void OnEnable()
    {
        _inputReader.ChoosePositionEvent += UpdateSelectedRange;
    }

    private void OnDisable()
    {
        _inputReader.ChoosePositionEvent -= UpdateSelectedRange;
    }
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            StartCoroutine(DealDamageWithDelay(damageable));
        }
    }

    private IEnumerator DealDamageWithDelay(IDamageable target)
    {
        if (_animator == null) yield break;

        // Get current length
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;

        // Wait util pass % time
        float delayTime = animationDuration * 0.3f;
        yield return new WaitForSeconds(delayTime);

        // Deal damage
        target.TakeDamage(gameObject.transform.position, skillSO.BaseDamage);
    }

    private void UpdateSelectedRange(Vector2 position)
    {
        _selectedRange = position;
    }
    
    private void PlaySound()
    {
        // _audioCueKey = _sfxEventChannel.RaisePlayEvent(_SFX, _SFXConfiguration, _selectedRange);
        //
        // StartCoroutine(StopAudioAfterTime(_audioCueKey));
    }

    private IEnumerator StopAudioAfterTime(AudioCueKey audioCueKey)
    {
        yield return new WaitForSeconds(1f);

        _sfxEventChannel.RaiseStopEvent(audioCueKey);
    }
}