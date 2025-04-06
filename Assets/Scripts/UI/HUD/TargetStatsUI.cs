using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetStatsUI : MonoBehaviour
{
    [Header("UI Elements")] 
    [SerializeField] private GameObject _targetBar;
    [SerializeField] private Image _healthBar;
    [SerializeField] private TextMeshProUGUI _healthText;

    [Header("Listening To")] 
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private VoidEventChannelSO _onDamageDealt;

    private MonsterHealth _healthTarget;

    private void OnEnable()
    {
        _inputReader.PickTarget += SetTarget;
    }

    private void OnDisable()
    {
        _inputReader.PickTarget -= SetTarget;
    }

    private void SetTarget(MonsterHealth healthTarget)
    {
        // Clean previous
        if (healthTarget != null)
        {
            healthTarget.OnHealthChanged -= UpdateHealthBar;
        }

        _healthTarget = healthTarget;
        if (_healthTarget != null)
        {
            _healthTarget.OnHealthChanged += UpdateHealthBar;
            UpdateHealthBar(healthTarget.currentHealth, healthTarget.Entity.MaxHealth);
            ShowBar();
        }
        else
        {
            HideBar();
        }
    }

    private void ShowBar()
    {
        _targetBar.gameObject.SetActive(true);
    }

    private void HideBar()
    {    
        _targetBar.gameObject.SetActive(false);
    }

    private void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        _healthText.SetText($"{currentHealth}");
        _healthBar.fillAmount = (float)currentHealth / maxHealth;
    }
}