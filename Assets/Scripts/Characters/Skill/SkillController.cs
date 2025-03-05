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

    private void UpdateSelectedRange(Vector2 position)
    {
        _selectedRange = position;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void PlaySound()
    {
        _audioCueKey = _sfxEventChannel.RaisePlayEvent(_SFX, _SFXConfiguration, _selectedRange);

        StartCoroutine(StopAudioAfterTime(_audioCueKey));
    }

    private IEnumerator StopAudioAfterTime(AudioCueKey audioCueKey)
    {
        yield return new WaitForSeconds(0.5f);

        _sfxEventChannel.RaiseStopEvent(audioCueKey);
    }
}