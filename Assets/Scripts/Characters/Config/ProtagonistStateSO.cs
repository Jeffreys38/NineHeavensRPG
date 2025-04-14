using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Protagonist State", menuName = "State/Protagonist State")]
public class ProtagonistStateSO : SerializableScriptableObject
{
    [Header("Info")] 
    public string nickName;
    public string avatarUrl;
    public string bio;
    public GameObject prefabReview;

    [Header("Character Base Stats")] 
    public int maxHealth;
    public int maxMana;
    
    [Header("Character Current Stats")] 
    public int currentAtk;
    public int currentDefense;
    public int currentHealth;
    public int currentMana;
    public float currentIntelligence;
    public float currentLucky;
    
    [Header("Character Bonus Stats")] 
    public int bonusAtk;
    public int bonusDefense;
    public int bonusHealth;
    public int bonusMana;
    public float bonusIntelligence;
    public float bonusLucky;
    
    public int power;
    public int currentExp;
    public Vector3 currentPosition;
    public List<SkillSO> learnedSkills;
    
    [Header("Realm Progression")]
    public RealmTier currentRealmTier;
    public RealmStage currentRealmStage;
  
    [Header("Equipped Items")]
    public Dictionary<EquipmentType, EquipmentItemSO> equippedItems = new Dictionary<EquipmentType, EquipmentItemSO>();
    
    private List<string> ConvertEquippedItemsToList()
    {
        return equippedItems.Select(kvp => kvp.Value.Guid).ToList();
    }
}