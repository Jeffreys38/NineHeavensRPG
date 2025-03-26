using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Exp Request Event")]
public class ExpRequestEventChannelSO : ScriptableObject
{
    public UnityAction<UnityAction<int>> OnRequestExp;

    public void RequestExp(UnityAction<int> callback)
    {
        OnRequestExp?.Invoke(callback);
    }
}