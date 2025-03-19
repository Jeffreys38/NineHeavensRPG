using System;
using UnityEngine;

public class MonsterHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private UIHealthBar _healthBar;
    [SerializeField] private MonsterSO _entity;
    
    private MonsterLoot _monsterLoot;
    private MonsterKnockback _monsterKnockback;

    public int currentHealth;
    public int currentMana;
    public int currentAttack;
    public int currentDefense;
    public float currentLucky;
    public float currentIntelligence;
    
    public MonsterSO Entity => _entity;
    
    public event Action<int, int> OnHealthChanged;

    private void Awake()
    {
        _monsterLoot = GetComponent<MonsterLoot>();
        _monsterKnockback = GetComponent<MonsterKnockback>();
    }

    private void Start()
    {
        currentHealth = _entity.MaxHealth;
        currentMana = _entity.MaxMana;
        currentAttack = _entity.MaxAttack;
        currentDefense = _entity.MaxDefense;
        currentLucky = _entity.MaxLucky;
        currentIntelligence = _entity.MaxIntelligence;
        
        // Init ui health
        OnHealthChanged?.Invoke(currentHealth, _entity.MaxHealth);
    }

    public void TakeDamage(Vector2 attackerPosition, int damage)
    {
        currentHealth -= damage;
        if (_monsterKnockback) StartCoroutine(_monsterKnockback.Knockback());
        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        _healthBar.Show();
        _healthBar.UpdateHealth(currentHealth, _entity.MaxHealth);
        
        OnHealthChanged?.Invoke(currentHealth, _entity.MaxHealth);
    }
    
    public bool IsLowHealth(float threshold = 0.3f)
    {
        return currentHealth < _entity.MaxHealth * threshold;
    }

    private void Die()
    {
        _monsterLoot.DropReward();
        Destroy(gameObject);
    }
}