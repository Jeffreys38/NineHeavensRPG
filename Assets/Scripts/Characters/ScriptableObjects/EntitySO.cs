using System;
using System.Collections.Generic;
using UnityEngine;

// Only used for monster, npc
[CreateAssetMenu(fileName = "New Entity", menuName = "GameData/Entity")]
public class EntitySO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private GameObject _prefabReview;
    [SerializeField] private RealmStage _realmStage;
    [SerializeField] private RealmTier _realmTier;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _maxMana;
    [SerializeField] private float _maxIntelligence;
    [SerializeField] private float _maxLucky;
    [SerializeField] private List<SkillSO> _learnedSkills;    
    [SerializeField] private int _receivedEXP;    
    
    public string Name => _name;
    public GameObject PrefabReview => _prefabReview;
    public RealmStage RealmData => _realmStage;
    public RealmTier RealmTier => _realmTier;
    public int maxHealth => _maxHealth;
    public int maxMana => _maxMana;
    public float maxIntelligence => _maxIntelligence;
    public float maxLucky => _maxLucky;
    public List<SkillSO> LearnedSkills => _learnedSkills;
    public int ReceivedEXP => _receivedEXP;
}