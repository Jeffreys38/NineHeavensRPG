using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
    [Header("Listening To")] 
    [SerializeField] private InputReader _inputReader;
    
    public ProtagonistStateSO _protagonistState;
    public List<Image> slotImages = new List<Image>(4);
    public List<Image> cooldownOverlays = new List<Image>(4);

    private Vector2 choosedRange;

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
        UpdateSkillIcons();
    }
    
    private void Update()
    {
        UpdateCooldownUI();
    }
    
    private void UpdateSkillIcons()
    {
        for (int i = 0; i < slotImages.Count; i++)
        {
            slotImages[i].sprite = _protagonistState.learnedSkills[i].Icon;
        }
    }
    
    private void HandleSkillInput(int keyNumber)
    {
        if (keyNumber >= 0 && keyNumber < _protagonistState.learnedSkills.Count)
        {
            int index = keyNumber - 1;
            
            UseSkill(index);
        }
    }
    
    private void UseSkill(int index)
    {
        SkillSO skill = _protagonistState.learnedSkills[index];

        // Can use skill
        if (skill.currentCooldown <= 0)
        {
            skill.currentCooldown = skill.Cooldown;

            if (choosedRange != null)
            {
                var prefab = _protagonistState.learnedSkills[index].Prefab;
                var skillObject = Instantiate(prefab, choosedRange, Quaternion.identity);

                DestroyAfterTime(skillObject);
            }
        }
    }

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
    
    private void UpdateCooldownUI()
    {
        for (int i = 0; i < cooldownOverlays.Count; i++)
        {
            SkillSO skill = _protagonistState.learnedSkills[i];

            if (skill.currentCooldown > 0)
            {
                skill.currentCooldown -= Time.deltaTime;
                float cooldownRatio = skill.currentCooldown / skill.Cooldown;
                
                cooldownOverlays[i].fillAmount = cooldownRatio;  
            }
            else
            {
                cooldownOverlays[i].fillAmount = 0;
            }
        }
    }

    private void SetRange(Vector2 range)
    {
        choosedRange = range;
    }
}