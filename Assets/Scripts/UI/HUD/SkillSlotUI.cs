using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
    [Header("Listening To")]
    [SerializeField] private InputReader _inputReader;
    
    [Header("Broadcasting On")] 
    [SerializeField] private VoidEventChannelSO _manaChangedEvent;

    [Header("References")]
    [SerializeField] private ProtagonistStateSO _protagonistState;

    [SerializeField] private Sprite _defaultIcon;           // Default skill icon
    [SerializeField] private List<Image> _slotImages;       // UI skill slot icons
    [SerializeField] private List<Image> _cooldownOverlays; // UI cooldown overlays

    private List<SkillSO> _equippedSkills; // Stores learned skills for UI display
    private Vector2 _chosenRange;          // Stores the chosen position for skill usage

    private void OnEnable()
    {
        _inputReader.AttackEvent += HandleSkillInput;
        _inputReader.ChoosePositionEvent += SetRange;
    }

    private void OnDisable()
    {
        _inputReader.AttackEvent -= HandleSkillInput;
        _inputReader.ChoosePositionEvent -= SetRange;
    }

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _equippedSkills = new List<SkillSO>();  
        LoadSkills(); // Load available skills when the game starts
    }

    /// <summary>
    /// Loads all learned skills from the protagonist's state and updates the UI.
    /// Empty slots remain unchanged or display a default state.
    /// </summary>
    private void LoadSkills()
    {
        _equippedSkills.Clear(); // Reset the skill list to avoid duplicates

        foreach (var skill in _protagonistState.learnedSkills)
        {
            if (skill != null)
            {
                _equippedSkills.Add(skill);
            }
        }

        UpdateSkillIcons();
    }

    private void Update()
    {
        UpdateCooldownUI();
    }

    /// <summary>
    /// Updates UI icons for skill slots. 
    /// If a skill is assigned, its icon is displayed; otherwise, the slot remains empty.
    /// </summary>
    private void UpdateSkillIcons()
    {
        for (int i = 0; i < _slotImages.Count; i++)
        {
            if (i < _equippedSkills.Count)
            {
                _slotImages[i].sprite = _equippedSkills[i].Icon;
            }
            else
            {
                _slotImages[i].sprite = _defaultIcon;
            }
        }
    }

    /// <summary>
    /// Handles skill activation based on player input.
    /// </summary>
    /// <param name="keyNumber">Skill slot number (1-based index)</param>
    private void HandleSkillInput(int keyNumber)
    {
        int index = keyNumber - 1; // Convert 1-based input to 0-based index

        // Ensure the index is within bounds and the slot contains a skill
        if (index >= 0 && index < _equippedSkills.Count)
        {
            UseSkill(index);
        }
        else
        {
            Debug.LogWarning("No skill assigned to this slot.");
        }
    }

    /// <summary>
    /// Uses a skill at the given slot index, applying cooldown and instantiating the skill object.
    /// </summary>
    private void UseSkill(int index)
    {
        SkillSO skill = _equippedSkills[index];
        
        // Can use skill
        if (skill.currentCooldown <= 0 && _protagonistState.currentMana >= skill.ManaCost)
        {
            skill.currentCooldown = skill.Cooldown; // Reset cooldown

            GameObject skillObject = Instantiate(skill.Prefab, _chosenRange, Quaternion.identity);
            DestroyAfterTime(skillObject);
                
            // Consume mana
            _protagonistState.currentMana -= skill.ManaCost;
            _manaChangedEvent.RaiseEvent();
        }
    }

    /// <summary>
    /// Destroys the instantiated skill object after its animation completes.
    /// If no animator is found, destroys the object after 1 second.
    /// </summary>
    private void DestroyAfterTime(GameObject skillObject)
    {
        Animator animator = skillObject.GetComponent<Animator>();

        if (animator != null)
        {
            float animationTime = animator.GetCurrentAnimatorStateInfo(0).length;
            Destroy(skillObject, animationTime);
        }
        else
        {
            Destroy(skillObject, 1f);
        }
    }

    /// <summary>
    /// Updates cooldown overlay UI based on remaining cooldown time.
    /// </summary>
    private void UpdateCooldownUI()
    {
        for (int i = 0; i < _cooldownOverlays.Count; i++)
        {
            if (i < _equippedSkills.Count) // Ensure we don't access non-existent skills
            {
                SkillSO skill = _equippedSkills[i];

                if (skill.currentCooldown > 0)
                {
                    skill.currentCooldown -= Time.deltaTime;
                    float cooldownRatio = skill.currentCooldown / skill.Cooldown;
                    _cooldownOverlays[i].fillAmount = cooldownRatio;  
                }
                else
                {
                    _cooldownOverlays[i].fillAmount = 0;
                }
            }
        }
    }

    /// <summary>
    /// Sets the chosen skill activation position.
    /// </summary>
    private void SetRange(Vector2 range)
    {
        _chosenRange = range;
    }
}