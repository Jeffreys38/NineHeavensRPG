using UnityEngine;
using UnityEngine.Localization;
using System.Collections.Generic;

public enum QuestState { NotStarted, InProgress, Completed }

// [Type]_[Action]_[Target Name]_[Quantity]
public abstract class QuestDataSO : SerializableScriptableObject
{
    [SerializeField] private LocalizedString questName;
    [SerializeField] private LocalizedString description;
    [SerializeField] private List<ItemStack> rewards = new List<ItemStack>();
    [SerializeField] private int experienceReward;
    public QuestState questState = QuestState.NotStarted;
    
    public LocalizedString Name => questName;
    public LocalizedString Description => description;
    public List<ItemStack> Rewards => rewards;
    public int ExperienceReward => experienceReward;

    public abstract bool IsComplete();
    public abstract void RegisterEvent();
    public abstract void RemoveEvent();
}