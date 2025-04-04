﻿using System;
using UnityEngine;

public enum SkillType
{
    Active,     // Can be activated with press keyboard
    Passive,    // Automatic activated with some conditions
}

public enum SkillEffect
{
    Normal,     // Attack skill
    Burn,
    Toxic,
    None
}

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skill", order = 1)]
public class SkillSO : SerializableScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _description;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private SkillType _skillType;
    [SerializeField] private SkillEffect _skillEffect;
    [SerializeField] private int _baseDamage;
    [SerializeField] private RealmTier _requiredRealmTier;
    [SerializeField] private RealmStage _requiredRealmStage;
    [SerializeField] private Rarity _rarity;
    [SerializeField] private float _cooldown;
    [SerializeField] private int _manaCost;
    
    public string Name => _name;
    public Sprite Icon => _icon;
    public string Description => _description;
    public GameObject Prefab => _prefab;
    public SkillType SkillType => _skillType;
    public SkillEffect SkillEffect => _skillEffect;
    public int BaseDamage => _baseDamage;
    public RealmTier RequiredRealmTier => _requiredRealmTier;
    public RealmStage RequiredRealmLevel => _requiredRealmStage;
    public Rarity Rarity => _rarity;
    public float Cooldown => _cooldown;
    public int ManaCost => _manaCost;

    public float currentCooldown;
}