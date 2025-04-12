using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/AudioSource Event Channel")]
public class AudioSourceEventChannelSO: DescriptionBaseSO
{
    public UnityAction<AudioSource> OnEventRaised;

    public void RaiseEvent(AudioSource value)
    {
        OnEventRaised?.Invoke(value);
    }
}