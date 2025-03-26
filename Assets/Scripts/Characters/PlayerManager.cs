using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private ProtagonistStateSO _player;
    
    [Header("Listening to")] 
    [SerializeField] private IntEventChannelSO _onManaConsumed;
    [SerializeField] private IntEventChannelSO _onManaRestored;
    [SerializeField] private IntEventChannelSO _onHealthConsumed;
    [SerializeField] private IntEventChannelSO _onHealthRestored;
    [SerializeField] private VoidEventChannelSO _onPowerChanged;
    [SerializeField] private EquipmentEventChannelSO _onEquippedEquipment;
    [SerializeField] private EquipmentEventChannelSO _onUnequippedEquipment;
    
    [Header("Broadcasting on")] 
    [SerializeField] private VoidEventChannelSO _powerUpdatedEvent;
    [SerializeField] private VoidEventChannelSO _healthChangedEvent;
    [SerializeField] private VoidEventChannelSO _manaChangedEvent;

    private static readonly Dictionary<RealmTier, int> RealmTierMultiplier = new Dictionary<RealmTier, int>
    {
        { RealmTier.Mortal, 1 },
        { RealmTier.QiRefining, 2 },
        { RealmTier.Foundation, 3 },
        { RealmTier.CoreFormation, 5 },
        { RealmTier.NascentSoul, 8 },
        { RealmTier.SoulFormation, 12 },
        { RealmTier.GreatAscension, 20 },
        { RealmTier.ImmortalEmperor, 30 }
    };

    private static readonly Dictionary<RealmStage, float> RealmStageMultiplier = new Dictionary<RealmStage, float>
    {
        { RealmStage.Early, 1.0f },
        { RealmStage.Mid, 1.2f },
        { RealmStage.Late, 1.5f }
    };

    private void OnEnable()
    {
        _onHealthConsumed.OnEventRaised += ConsumeHealth;
        _onHealthRestored.OnEventRaised += RestoreHealth;
        _onManaConsumed.OnEventRaised += ConsumeMana;
        _onManaRestored.OnEventRaised += RestoreMana;
        _onPowerChanged.OnEventRaised += CalculateCombatPower;
        
        _onEquippedEquipment.OnEventRaised += EquipItem;
        _onUnequippedEquipment.OnEventRaised += UnequipItem;
    }

    private void OnDisable()
    {
        _onHealthConsumed.OnEventRaised -= ConsumeHealth;
        _onHealthRestored.OnEventRaised -= RestoreHealth;
        _onManaConsumed.OnEventRaised -= ConsumeMana;
        _onManaRestored.OnEventRaised -= RestoreMana;
        _onPowerChanged.OnEventRaised -= CalculateCombatPower;
        
        _onEquippedEquipment.OnEventRaised -= EquipItem;
        _onUnequippedEquipment.OnEventRaised -= UnequipItem;
    }
    
    private void ConsumeHealth(int amount)
    {
        if (_player.currentHealth >= amount)
        {
            _player.currentHealth -= amount;
        }
        
        _healthChangedEvent.RaiseEvent();
    }
    
    private void RestoreHealth(int amount)
    {
        if (amount + _player.currentHealth > _player.maxHealth)
        {
            _player.currentHealth = _player.maxHealth;
        }
        else
        {
            _player.currentHealth += amount;
        }
        
        _healthChangedEvent.RaiseEvent();
    }
    
    private void ConsumeMana(int amount)
    {
        if (amount > _player.currentMana)
        {
            _player.currentMana = 0;
        }
        else
        {
            _player.currentMana -= amount;
        }
        
        _manaChangedEvent.RaiseEvent();
    }
    
    private void RestoreMana(int amount)
    {
        if (amount + _player.currentMana > _player.maxMana)
        {
            _player.currentMana = _player.maxMana;
        }
        else
        {
            _player.currentMana += amount;
        }
        
        _manaChangedEvent.RaiseEvent();
    }

    private void EquipItem(EquipmentItemSO item)
    {
        bool iEquipped = _player.equippedItems.TryAdd(item.EquipmentType, item);

        if (iEquipped)
        {
            ApplyBonusStatsFrom(item);
            CalculateCombatPower();
        
            _powerUpdatedEvent.RaiseEvent();
        }
    }
    
    private void ApplyBonusStatsFrom(EquipmentItemSO item)
    {
        _player.bonusAtk += item.BaseAttack;
        _player.bonusDefense += item.BaseDefense;
        _player.bonusLucky += item.BaseLucky;
        _player.bonusIntelligence += item.BaseIntelligence;
    }

    private void UnequipItem(EquipmentItemSO item)
    {
        if (_player.equippedItems.ContainsKey(item.EquipmentType))
        {
            bool isUnequip = _player.equippedItems.Remove(item.EquipmentType);

            if (isUnequip)
            {
                RemoveBonusStatsFrom(item);
                CalculateCombatPower();
            
                _powerUpdatedEvent.RaiseEvent();
            }
        }
    }

    private void RemoveBonusStatsFrom(EquipmentItemSO item)
    {
        _player.bonusAtk -= item.BaseAttack;
        _player.bonusDefense -= item.BaseDefense;
        _player.bonusLucky -= item.BaseLucky;
        _player.bonusIntelligence -= item.BaseIntelligence;
    }

    private void CalculateCombatPower()
    {
        int basePower = _player.currentAtk * 2 + _player.currentDefense * 2 +
                        Mathf.RoundToInt(_player.currentIntelligence * 1.5f) +
                        Mathf.RoundToInt(_player.currentLucky * 1.2f);

        int equipmentBonusBase = 0;
        int equipmentBonusEnhanced = 0;

        foreach (var item in _player.equippedItems.Values)
        {
            if (item != null)
            {
                equipmentBonusBase += item.BaseAttack * 2 + item.BaseDefense * 2 +
                                      Mathf.RoundToInt(item.BaseIntelligence * 1.5f) +
                                      Mathf.RoundToInt(item.BaseLucky * 1.2f);

                equipmentBonusEnhanced += item.EnhancedAttack * 2 + item.EnhancedDefense * 2 +
                                          Mathf.RoundToInt(item.EnhancedIntelligence * 1.5f) +
                                          Mathf.RoundToInt(item.EnhancedLucky * 1.2f);
            }
        }

        int realmMultiplier = RealmTierMultiplier.ContainsKey(_player.currentRealmTier)
            ? RealmTierMultiplier[_player.currentRealmTier]
            : 1;

        float stageMultiplier = RealmStageMultiplier.ContainsKey(_player.currentRealmStage)
            ? RealmStageMultiplier[_player.currentRealmStage]
            : 1.0f;

        // Lực chiến = (Chỉ số cơ bản + chỉ số trang bị cơ bản + chỉ số cường hóa) * Hệ số cảnh giới * Hệ số giai đoạn  
        _player.power = Mathf.RoundToInt((basePower + equipmentBonusBase + equipmentBonusEnhanced) * realmMultiplier * stageMultiplier);
        
        _powerUpdatedEvent.RaiseEvent();
    }
}