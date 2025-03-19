using System.Collections;
using UnityEngine;

public class MonsterKnockback : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private float knockbackForce = 2f;
    private float duration = 0.2f; 
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public IEnumerator Knockback()
    {
        if (_rigidbody2D != null)
        {
            Vector2 knockbackDirection = Random.insideUnitCircle.normalized;
            
            _rigidbody2D.linearVelocity = knockbackDirection * knockbackForce;
            
            yield return new WaitForSeconds(duration);
            _rigidbody2D.linearVelocity = Vector2.zero;
        }
    }
}