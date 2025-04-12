using UnityEngine;

public class ComingSoonTrigger : MonoBehaviour
{
    [SerializeField] private FloatEventChannelSO _currentAudioTimeEvent;
    [SerializeField] private VoidEventChannelSO _pauseLyrics;
    
    public AudioSource audioSource;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ComingSoonTrigger");
            var currentTime = audioSource.time;
            _pauseLyrics.RaiseEvent();
            _currentAudioTimeEvent.RaiseEvent(currentTime);
        }
    }
}