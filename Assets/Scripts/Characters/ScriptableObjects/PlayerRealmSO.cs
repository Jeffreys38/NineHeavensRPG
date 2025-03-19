using UnityEngine;

[CreateAssetMenu(menuName = "Player/Player Realm Manager")]
public class PlayerRealmSO : DescriptionBaseSO
{
    [Header("Listening To")] 
    [SerializeField] private RealmData _realmData;

    [SerializeField] private ProtagonistStateSO _protagonistState;

    [Header("Broadcasting On")] 
    [SerializeField] private RealmEventChannelSO _realmEvent;

    public void GainExp(int amount)
    {
        _protagonistState.currentExp += amount;
        Debug.Log($"Nhận {amount} EXP! Tổng EXP: {_protagonistState.currentExp}");
        CheckForRealmUpgrade();
    }

    private void CheckForRealmUpgrade()
    {
        foreach (var level in _realmData.levels)
        {
            if (level.stage == _protagonistState.currentRealmStage &&
                _protagonistState.currentExp >= level.requiredExp)
            {
                _protagonistState.currentExp -= level.requiredExp;
                AdvanceRealm(level);
                _realmEvent.RaiseEvent(_protagonistState.currentRealmTier, _protagonistState.currentRealmStage);
                break;
            }
        }
    }

    private void AdvanceRealm(RealmLevel level)
    {
        _protagonistState.currentHealth += level.bonusHealth;
        _protagonistState.currentMana += level.bonusMana;
        _protagonistState.currentAtk += level.bonusAttack;
        _protagonistState.currentDefense += level.bonusDefense;
        _protagonistState.currentIntelligence += level.bonusIntelligence;
        _protagonistState.currentLucky += level.bonusLucky;

        // (Early -> Mid -> Late -> new stage)
        if (_protagonistState.currentRealmStage == RealmStage.Early)
        {
            _protagonistState.currentRealmStage = RealmStage.Mid;
        }
        else if (_protagonistState.currentRealmStage == RealmStage.Mid)
        {
            _protagonistState.currentRealmStage = RealmStage.Late;
        }
        else
        {
            _protagonistState.currentRealmStage = RealmStage.Early;
            UpgradeRealmTier();
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