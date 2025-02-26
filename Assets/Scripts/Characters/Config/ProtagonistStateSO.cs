using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Protagonist State", menuName = "State/Protagonist State")]
public class ProtagonistStateSO : ScriptableObject, IDataPersistence
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
        if (defaultRealmData != null)
        {
            currentRealmTier = defaultRealmData.realmTier;
            currentRealmStage = RealmStage.Early;
            currentExp = 0;
            
            Debug.Log("Default Realm Data is not assigned. Please assign it in the inspector.");
        }

        currentHealth = 100;
        currentMana = 50;
        power = 10;
        currentIntelligence = 5.0f;
        currentLucky = 1.0f;
        currentPosition = Vector3.zero;
    }
     
    public void LoadData(GameData data)
    {
        currentHealth = data._protagonist.currentHealth;
        currentMana = data._protagonist.currentMana;
        currentRealmTier = data._protagonist.currentRealmTier;
        currentRealmStage = data._protagonist.currentRealmStage;
        currentExp = data._protagonist.currentExp;
        power = data._protagonist.power;
        currentIntelligence = data._protagonist.currentIntelligence;
        currentLucky = data._protagonist.currentLucky;
        currentPosition = data._protagonist.currentPosition;
    }
    
    public void SaveData(ref GameData data)
    {
        if (data == null) Debug.LogWarning("Cannot save a null GameData");
        data._protagonist.currentHealth = currentHealth;
        data._protagonist.currentMana = currentMana;
        data._protagonist.currentRealmTier = currentRealmTier;
        data._protagonist.currentRealmStage = currentRealmStage;
        data._protagonist.currentExp = currentExp;
        data._protagonist.power = power;
        data._protagonist.currentIntelligence = currentIntelligence;
        data._protagonist.currentLucky = currentLucky;
        data._protagonist.currentPosition = currentPosition;
    }
}