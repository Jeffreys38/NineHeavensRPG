using System;
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
    
    [SerializeField] private RealmData defaultRealmData;
    
    // Default value when it is not assigned in inspector
    private void OnEnable()
    {
        if (currentRealmTier == 0)
        {
            InitializeDefaults();
        }
    }

    private void InitializeDefaults()
    {
        currentRealmTier = defaultRealmData.realmTier;
        currentRealmStage = RealmStage.Early;
        currentExp = 0;
        
        currentHealth = 100;
        currentMana = 50;
        power = 10;
        currentIntelligence = 5.0f;
        currentLucky = 1.0f;
        currentPosition = Vector3.zero;
    }
}