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

    private MonsterController _target;

    private void OnEnable()
    {
        _inputReader.PickTarget += SetTarget;
    }

    private void OnDisable()
    {
        _inputReader.PickTarget -= SetTarget;
    }

    private void SetTarget(MonsterController target)
    {
        if (_target != null)
        {
            _target.OnHealthChanged -= UpdateHealthBar;
        }

        _target = target;

        if (_target != null)
        {
            _target.OnHealthChanged += UpdateHealthBar;
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
        UpdateHealthBar(_target);
    }

    private void HideBar()
    {    
        _targetBar.gameObject.SetActive(false);
    }

    private void UpdateHealthBar(MonsterController target)
    {
        if (target == null) return;

        int currentHealth = target.GetCurrentHealth();
        int maxHealth = target.GetMaxHealth();

        _healthText.SetText($"{currentHealth}");
        _healthBar.fillAmount = (float)currentHealth / maxHealth;
    }
}