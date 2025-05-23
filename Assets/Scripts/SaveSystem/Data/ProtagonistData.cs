using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] 
public class ProtagonistData
{
    public int currentHealth;
    public int currentMana;
    public int currentAtk;
    public int currentDefense;
    public float currentIntelligence;
    public float currentLucky;

    public int bonusAtk;
    public int bonusDefense;
    public float bonusIntelligence;
    public float bonusLucky;
    public int bonusHealth;
    public int bonusMana;
    
    public RealmTier currentRealmTier;
    public RealmStage currentRealmStage;
    public int currentExp;
    public Vector3 currentPosition;
    public List<string> equippedEquipments;
    public List<string> learnedSkills;
    
    // Auto-update
    public int power;
}