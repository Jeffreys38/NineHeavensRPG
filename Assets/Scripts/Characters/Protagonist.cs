using System;
using UnityEngine;

public class Protagonist : MonoBehaviour
{
    [SerializeField] private ProtagonistStateSO protagonist;
    
    [Header("Listening To")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private PlayerRealmSO _playerRealm;
    [SerializeField] private IntEventChannelSO _onExpGained;
    
    [Header("Broadcasting On")]
    [SerializeField] private RealmEventChannelSO _realmEvent;
    [SerializeField] private VoidEventChannelSO _onExpChanged;

    private void OnEnable()
    {
        _realmEvent.OnEventRaised += OnRealmUpgraded;
        _onExpGained.OnEventRaised += AddExp;
    }

    private void OnDisable()
    {
        _realmEvent.OnEventRaised -= OnRealmUpgraded;
        _onExpGained.OnEventRaised -= AddExp;
    }

    private void AddExp(int exp)
    {
        protagonist.currentExp += exp;
        
        _onExpChanged.RaiseEvent();
    }
    
    private void OnRealmUpgraded(RealmTier tier, RealmStage stage)
    {
        Debug.Log($"[Event] Cảnh giới thăng cấp: {tier} - {stage}");
    }
}