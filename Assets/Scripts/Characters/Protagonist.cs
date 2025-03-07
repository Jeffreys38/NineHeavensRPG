using System;
using UnityEngine;

public class Protagonist : MonoBehaviour
{
    [Header("Listening To")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private RealmManager _realmManager;
    
    [Header("Broadcasting On")]
    [SerializeField] private RealmEventChannelSO _realmEvent;

    private void OnEnable()
    {
        _realmEvent.OnEventRaised += OnRealmUpgraded;
    }

    private void OnDisable()
    {
        _realmEvent.OnEventRaised -= OnRealmUpgraded;
    }
    
    private void OnRealmUpgraded(RealmTier tier, RealmStage stage)
    {
        Debug.Log($"[Event] Cảnh giới thăng cấp: {tier} - {stage}");
    }
}