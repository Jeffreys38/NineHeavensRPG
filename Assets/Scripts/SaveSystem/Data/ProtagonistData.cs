using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] 
public class ProtagonistData
{
    public int currentHealth;
    public int currentMana;
    public int level;
    public float currentIntelligence;
    public float currentLucky;
    public RealmTier currentRealmTier;
    public RealmStage currentRealmStage;
    public int currentExp;
    public Vector3 currentPosition;
    public List<string> learnedSkills;
    
    // Auto-update
    public int power;
}