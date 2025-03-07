using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterController : MonoBehaviour, IDamageable
{
    [Header("References")] 
    [SerializeField] private MonsterSpawnTrackerSO _monsterSpawnTracker;
    [SerializeField] private EntitySO _entity;
    
    private Rigidbody2D _rigidbody2D;
    
    private int _currentHealth;
    private int _currentMana;
    private float _currentIntelligence;
    private float _currentLucky;
    
    private void Start()
    {
        _rigidbody2D  = GetComponent<Rigidbody2D>();
        
        _currentHealth = _entity.maxHealth;
        _currentMana = _entity.maxMana;
        _currentIntelligence = _entity.maxIntelligence;
        _currentLucky = _entity.maxLucky;
    }

    public void TakeDamage(Vector2 attackerPosition, int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
        
        StartCoroutine(Knockback(attackerPosition, 2f, 0.3f));
        
        // Fire a message that monster health is changed
        OnHealthChanged?.Invoke(this);
    }
    
    private IEnumerator Knockback(Vector2 attackerPosition, float knockbackForce, float duration)
    {
        Vector2 knockbackDirection = (transform.position - (Vector3)attackerPosition).normalized;

        if (_rigidbody2D != null)
        {
            _rigidbody2D.linearVelocity = knockbackDirection * knockbackForce;

            yield return new WaitForSeconds(duration);

            _rigidbody2D.linearVelocity = Vector2.zero;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public event Action<MonsterController> OnHealthChanged;
    
    public int GetCurrentHealth() => _currentHealth;
    public int GetMaxHealth() => _entity.maxHealth;
}