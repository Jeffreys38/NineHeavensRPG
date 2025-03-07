using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Protagonist State", menuName = "State/Protagonist State")]
public class ProtagonistStateSO : SerializableScriptableObject, IDataPersistence
{
    [Header("Character Stats")] 
    public int currentAtk;
    public int currentDefense;
    public int currentHealth;
    public int maxHealth;
    public int currentMana;
    public int maxMana;
    public int power;
    public float currentIntelligence;
    public float currentLucky;
    
    [Header("Realm Progression")]
    public RealmData realmData;
    public RealmTier currentRealmTier;
    public RealmStage currentRealmStage;
    public int currentExp;

    public Vector3 currentPosition;
    public List<SkillSO> learnedSkills;

    public void LoadData(GameData gameData)
    {
        var protagonistData = gameData.protagonistData;
        
        currentHealth = protagonistData.currentHealth;
        currentMana = protagonistData.currentMana;
        power = protagonistData.power;
        currentIntelligence = protagonistData.currentIntelligence;
        currentLucky = protagonistData.currentLucky;
        currentRealmTier = protagonistData.currentRealmTier;
        currentRealmStage = protagonistData.currentRealmStage;
        currentExp = protagonistData.currentExp;
        currentPosition = protagonistData.currentPosition;
    }
    
    public void SaveData(ref GameData data)
    {
        data.protagonistData.currentHealth = currentHealth;
        data.protagonistData.currentMana = currentMana;
        data.protagonistData.currentIntelligence = currentIntelligence;
        data.protagonistData.currentLucky = currentLucky;
        data.protagonistData.currentRealmTier = currentRealmTier;
        data.protagonistData.currentRealmStage = currentRealmStage;
        data.protagonistData.currentExp = currentExp;
        data.protagonistData.currentPosition = currentPosition;
        
        // Bind skill
        List<string> skillsGUID = new List<string>();
        foreach (var skill in learnedSkills)
        {
            skillsGUID.Add(skill.Guid.ToString());
        }
        data.protagonistData.learnedSkills = skillsGUID;
    }
    
    public int GetRequiredExpForCurrentRealm()
    {
        if (realmData == null) return 99999; // Fallback

        foreach (var level in realmData.levels)
        {
            if (level.stage == currentRealmStage)
            {
                return level.requiredExp;
            }
        }
        return 99999;
    }
}