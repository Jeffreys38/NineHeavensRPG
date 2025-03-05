using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Protagonist : MonoBehaviour
{
    [Header("Broadcasting On")]
    [SerializeField] private IntEventChannelSO _powerEvent;

    [Header("Listening On")] 
    [SerializeField] private InputReader _inputReader;
    
    [SerializeField] private ProtagonistStateSO _protagonist;
    
    private PowerManager _powerManager;
    private Vector2 _targetPosition;
    private SpriteRenderer lastTargetRenderer; // Stores the last targeted enemy's SpriteRenderer
    private Color originalColor = Color.white; // Stores the original color of the enemy

    private void OnEnable()
    {
       _inputReader.AttackEvent += ExecuteSkill;
       _inputReader.ChoosePositionEvent += ChooseTarget;

    }

    private void OnDisable()
    {
       _inputReader.AttackEvent -= ExecuteSkill;
    }

    private void Start()
    {
        _powerManager = new PowerManager(_protagonist);
    }

    private void ExecuteSkill(int keyNumber)
    {
 
    }

    private void ChooseTarget(Vector2 position)
    {
        _targetPosition = position;

        // Cast a ray from the mouse position to detect objects in the scene
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            SpriteRenderer enemyRenderer = hit.collider.GetComponent<SpriteRenderer>();
        
            if (enemyRenderer != null)
            {
                // Reset the color of the previously selected enemy, if different from the new one
                if (lastTargetRenderer != null && lastTargetRenderer != enemyRenderer)
                {
                    lastTargetRenderer.color = originalColor;
                }

                // Store the original color of the newly selected enemy
                originalColor = enemyRenderer.color;
            
                // Change the color of the selected enemy to a light red
                enemyRenderer.color = new Color(1f, 0.5f, 0.5f, 1f);

                // Update the reference to the currently selected enemy
                lastTargetRenderer = enemyRenderer;
            }
        }
        else
        {
            // If clicking outside an enemy, reset the last enemy's color
            if (lastTargetRenderer != null)
            {
                lastTargetRenderer.color = originalColor;
                lastTargetRenderer = null; // Clear reference to avoid unnecessary resets
            }
        }
    }
    
    // public void EquipItem(EquipmentData item)
    // {
    //     Data.equippedItems.Add(item);
    //     UpdatePower();
    // }

    // public void UnequipItem(EquipmentData item)
    // {
    //     Data.equippedItems.Remove(item);
    //     UpdatePower();
    // }

    private void UpdatePower()
    {
        _protagonist.power = _powerManager.CalculatePower();
        _powerEvent.RaiseEvent(_protagonist.power);
    }
}