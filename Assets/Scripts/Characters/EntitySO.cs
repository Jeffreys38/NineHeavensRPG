using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity", menuName = "GameData/Entity")]
public class EntitySO : SerializableScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private GameObject _prefabReview;
    [SerializeField] private RealmData _realmData;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _maxMana;
    [SerializeField] private float _maxIntelligence;
    [SerializeField] private float _maxLucky;
    
    // With monster, npc: learnedSkills will be assigned in inspector
    // With protagonist: learnedSkills will be default initialized is empty
    [SerializeField] private List<SkillSO> _learnedSkills;    
    
    public string Name => _name;
    public GameObject PrefabReview => _prefabReview;
    public RealmData RealmData => _realmData;
    public int maxHealth => _maxHealth;
    public int maxMana => _maxMana;
    public float maxIntelligence => _maxIntelligence;
    public float maxLucky => _maxLucky;
}