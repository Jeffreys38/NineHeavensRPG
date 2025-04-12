using UnityEngine;

public class StopLyric : MonoBehaviour
{
    [SerializeField] private FloatEventChannelSO _currentAudioTimeEvent;

    public AudioSource audioSource;
    private bool isPlaying = false;
    
    private void OnEnable()
    {
        _currentAudioTimeEvent.OnEventRaised += PlayAudio;
    }
    
    private void OnDisable()
    {
        _currentAudioTimeEvent.OnEventRaised -= PlayAudio;
    }

    private void PlayAudio(float time)
    {
        Debug.Log("Continue playing audio at: " + time);

        if (!isPlaying)
        {
            isPlaying = true;
            audioSource.time = time;
            audioSource.Play();
        }
    }
}
