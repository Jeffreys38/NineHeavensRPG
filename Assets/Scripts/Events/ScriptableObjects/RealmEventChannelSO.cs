using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Realm Event", menuName = "Events/Realm Event")]
public class RealmEventChannelSO : ScriptableObject
{
    public UnityAction<RealmTier, RealmStage> OnEventRaised;

    public void RaiseEvent(RealmTier tier, RealmStage stage)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(tier, stage);
    }
}