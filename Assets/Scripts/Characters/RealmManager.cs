using System.Collections.Generic;
using UnityEngine;

public class RealmManager : MonoBehaviour
{
    [SerializeField] private List<RealmData> _realmDatas;
    [SerializeField] private ProtagonistStateSO _protagonistState;
    
    [Header("Listening To")]
    [SerializeField] private VoidEventChannelSO _onExpChanged;
    [SerializeField] private RealmEventChannelSO _realmEvent;

    private void OnEnable()
    {
        _onExpChanged.OnEventRaised += CheckForRealmUpgrade;
    }

    private void OnDisable()
    {
        _onExpChanged.OnEventRaised -= CheckForRealmUpgrade;
    }

    private void CheckForRealmUpgrade()
    {
        RealmData currentRealm = GetCurrentRealmData();
        if (currentRealm == null) return;

        RealmLevel nextLevel = GetNextRealmLevel(currentRealm);
        if (nextLevel == null || _protagonistState.currentExp < nextLevel.requiredExp) return;

        _protagonistState.currentExp -= nextLevel.requiredExp;
        UpgradeRealm(nextLevel);
        _realmEvent.RaiseEvent(_protagonistState.currentRealmTier, _protagonistState.currentRealmStage);
    }

    private RealmData GetCurrentRealmData()
    {
        foreach (var realm in _realmDatas)
        {
            if (realm.realmTier == _protagonistState.currentRealmTier)
                return realm;
        }
        return null;
    }

    private RealmLevel GetNextRealmLevel(RealmData realmData)
    {
        foreach (var level in realmData.levels)
        {
            if (level.stage == _protagonistState.currentRealmStage)
                return level;
        }
        return null;
    }

    private void UpgradeRealm(RealmLevel level)
    {
        ApplyRealmBonuses(level);
        AdvanceRealmStage();
    }

    private void ApplyRealmBonuses(RealmLevel level)
    {
        _protagonistState.currentHealth += level.bonusHealth;
        _protagonistState.currentMana += level.bonusMana;
        _protagonistState.currentAtk += level.bonusAttack;
        _protagonistState.currentDefense += level.bonusDefense;
        _protagonistState.currentIntelligence += level.bonusIntelligence;
        _protagonistState.currentLucky += level.bonusLucky;
    }

    private void AdvanceRealmStage()
    {
        if (_protagonistState.currentRealmStage == RealmStage.Late)
        {
            _protagonistState.currentRealmStage = RealmStage.Early;
            UpgradeRealmTier();
        }
        else
        {
            _protagonistState.currentRealmStage++;
        }
    }

    private void UpgradeRealmTier()
    {
        int nextTierIndex = (int)_protagonistState.currentRealmTier + 1;
        if (nextTierIndex < System.Enum.GetValues(typeof(RealmTier)).Length)
        {
            _protagonistState.currentRealmTier = (RealmTier)nextTierIndex;
            Debug.Log($"Đột phá cảnh giới mới: {_protagonistState.currentRealmTier}!");
        }
    }
}