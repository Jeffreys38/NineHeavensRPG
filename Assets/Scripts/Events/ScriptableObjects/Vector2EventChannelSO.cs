using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Vector2 Event Channel")]
public class Vector2EventChannelSO : ScriptableObject
{
    public Action<Vector2> OnEventRaised;

    public void RaiseEvent(Vector2 direction)
    {
        OnEventRaised?.Invoke(direction);
    }
}