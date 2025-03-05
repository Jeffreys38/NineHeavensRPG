using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Protagonist State", menuName = "State/Protagonist State")]
public class ProtagonistStateSO : SerializableScriptableObject
{
    public int currentHealth;
    public int currentMana;
    public int power;
    public float currentIntelligence;
    public float currentLucky;
    public RealmTier currentRealmTier;
    public RealmStage currentRealmStage;
    public int currentExp;
    public Vector3 currentPosition;
    public List<SkillSO> learnedSkills;

    public void SetData(ProtagonistData data)
    {
        currentHealth = data.currentHealth;
        currentMana = data.currentMana;
        power = data.power;
        currentIntelligence = data.currentIntelligence;
        currentLucky = data.currentLucky;
        currentRealmTier = data.currentRealmTier;
        currentRealmStage = data.currentRealmStage;
        currentExp = data.currentExp;
        currentPosition = data.currentPosition;
    }

    public void LoadSkills(List<SkillSO> skills)
    {
        learnedSkills = skills;
    }
}