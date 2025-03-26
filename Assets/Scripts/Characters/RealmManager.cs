using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RealmManager : MonoBehaviour
{
    [SerializeField] private List<RealmData> _realmDatas;
    [SerializeField] private ProtagonistStateSO _protagonistState;
    
    [Header("Listening To")]
    [SerializeField] private RealmEventChannelSO _realmEvent;
    [SerializeField] private ExpRequestEventChannelSO _expRequestEvent;
    [SerializeField] private IntEventChannelSO _onExpGained;
    
    [Header("Broadcasting On")]
    [SerializeField] private VoidEventChannelSO _powerChangedEvent;
    [SerializeField] private VoidEventChannelSO _expChangedEvent;

    private void OnEnable()
    {
        _onExpGained.OnEventRaised += AddExp;
        _expRequestEvent.OnRequestExp += ProvideExpData;
    }

    private void OnDisable()
    {
        _onExpGained.OnEventRaised -= AddExp;
        _expRequestEvent.OnRequestExp -= ProvideExpData;
    }

    private void AddExp(int exp)
    {
        _protagonistState.currentExp += exp;
        CheckForRealmUpgrade();
    }

    private void CheckForRealmUpgrade()
    {
        RealmData currentRealm = GetCurrentRealmData();
        if (currentRealm == null) return;

        RealmLevel nextLevel = GetNextRealmLevel(currentRealm);
        if (nextLevel == null || _protagonistState.currentExp < nextLevel.requiredExp)
        {
            _expChangedEvent.RaiseEvent();
            return;
        }

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
        
        _powerChangedEvent.RaiseEvent();
        _expChangedEvent.RaiseEvent();
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
    
    private void ProvideExpData(UnityAction<int> callback)
    {
        int exp = GetRequiredExpForCurrentRealm();
        callback?.Invoke(exp);
    }
    
    private int GetRequiredExpForCurrentRealm()
    {
        if (_realmDatas == null) return 99999; // Fallback

        foreach (var realm in _realmDatas)
        {
            foreach (var level in realm.levels)
            {
                if (level.stage == _protagonistState.currentRealmStage)
                {
                    return level.requiredExp;
                }
            }
        }
        return 99999; // Fallback
    }
}