using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

// Only used for monster, npc
[CreateAssetMenu(fileName = "New Entity", menuName = "GameData/Entity")]
public class EntitySO : SerializableScriptableObject
{
    [SerializeField] private LocalizedString _name;
    [SerializeField] private LocalizedString _description;
    [SerializeField] private GameObject _prefabReview;
    [SerializeField] private RealmStage _realmStage;
    [SerializeField] private RealmTier _realmTier;
    
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _maxMana;
    [SerializeField] private int _maxAttack;
    [SerializeField] private int _maxDefense;
    [SerializeField] private float _maxIntelligence;
    [SerializeField] private float _maxLucky;
    
    [SerializeField] private List<SkillSO> _learnedSkills;    
    [SerializeField] private Rarity _rarity;
    
    public LocalizedString Name => _name;
    public LocalizedString Description => _description;
    public GameObject PrefabReview => _prefabReview;
    public RealmStage RealmData => _realmStage;
    public RealmTier RealmTier => _realmTier;
    public int MaxHealth => _maxHealth;
    public int MaxMana => _maxMana;
    public int MaxAttack => _maxAttack;
    public int MaxDefense => _maxDefense;
    public float MaxIntelligence => _maxIntelligence;
    public float MaxLucky => _maxLucky;
    public List<SkillSO> LearnedSkills => _learnedSkills;
    public Rarity Rarity => _rarity;
}