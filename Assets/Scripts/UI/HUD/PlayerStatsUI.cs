using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private ProtagonistStateSO _protagonistState;

    [Header("UI Elements")] 
    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _manaBar;
    [SerializeField] private Image _expBar;
    [SerializeField] private TextMeshProUGUI _expText;
    [SerializeField] private TextMeshProUGUI _powerText;

    [Header("Listening To")] 
    [SerializeField] private VoidEventChannelSO _onHealthChanged;
    [SerializeField] private VoidEventChannelSO _onManaChanged;
    [SerializeField] private VoidEventChannelSO _onExpChanged;
    [SerializeField] private VoidEventChannelSO _onPowerChanged;

    private void OnEnable()
    {
        _onHealthChanged.OnEventRaised += UpdateHealthBar;
        _onManaChanged.OnEventRaised += UpdateManaBar;
        _onExpChanged.OnEventRaised += UpdateExpBar;
        _onPowerChanged.OnEventRaised += UpdatePowerBar;
    }

    private void OnDisable()
    {
        _onHealthChanged.OnEventRaised -= UpdateHealthBar;
        _onManaChanged.OnEventRaised -= UpdateManaBar;
        _onExpChanged.OnEventRaised -= UpdateExpBar;
        _onPowerChanged.OnEventRaised -= UpdatePowerBar;
    }

    private void Start()
    {
        UpdateHealthBar();
        UpdateManaBar();
        UpdateExpBar();
        UpdatePowerBar();
    }

    private void UpdateHealthBar()
    {
        float healthPercent = (float)_protagonistState.currentHealth / _protagonistState.maxHealth;
        _healthBar.fillAmount = Mathf.Clamp01(healthPercent);
    }

    private void UpdateManaBar()
    {
        float manaPercent = (float)_protagonistState.currentMana / _protagonistState.maxMana;
        _manaBar.fillAmount = Mathf.Clamp01(manaPercent);
    }

    private void UpdateExpBar()
    {
        int currentExp = _protagonistState.currentExp;
        int requiredExp = _protagonistState.GetRequiredExpForCurrentRealm();

        float expPercent = (float)currentExp / requiredExp;
        _expBar.fillAmount = Mathf.Clamp01(expPercent);
        _expText.text = $"{currentExp} / {requiredExp}";
    }

    private void UpdatePowerBar()
    {
        int power = _protagonistState.power;
        
        _powerText.SetText(power.ToString() + " CP");
    }
}