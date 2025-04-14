using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[CreateAssetMenu(fileName = "Storage_QuestList", menuName = "Quest/Current Quest List")]
public class QuestListSO : ScriptableObject
{
    [Header("Quest GUID Lists")]
    [SerializeField] private List<string> _completedQuestGuids = new List<string>();
    [SerializeField] private List<string> _inProgressQuestGuids = new List<string>();

    [Header("Loaded Quests")]
    [SerializeField] private List<QuestDataSO> _allLoadedQuests = new List<QuestDataSO>();

    public List<string> CompletedQuestGuids => _completedQuestGuids;
    public List<string> InProgressQuestGuids => _inProgressQuestGuids;
    public List<QuestDataSO> AllLoadedQuests => _allLoadedQuests;
    
    public void ResetQuestlines()
    {
        _completedQuestGuids.Clear();
        _inProgressQuestGuids.Clear();
        _allLoadedQuests.Clear();
    }

    public void SetCompletedQuestlinesFromSave(List<string> savedCompletedQuests)
    {
        _completedQuestGuids = new List<string>(savedCompletedQuests);
    }

    public void SetInProgressQuestlinesFromSave(List<string> savedInProgressQuests)
    {
        _inProgressQuestGuids = new List<string>(savedInProgressQuests);
    }

    public IEnumerator LoadAllQuestsFromGuids()
    {
        _allLoadedQuests.Clear();

        // Combine both GUID lists
        List<string> allGuids = new List<string>();
        allGuids.AddRange(_completedQuestGuids);
        allGuids.AddRange(_inProgressQuestGuids);

        foreach (string guid in allGuids)
        {
            AsyncOperationHandle<QuestDataSO> handle = Addressables.LoadAssetAsync<QuestDataSO>(guid);
            yield return handle;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                QuestDataSO quest = handle.Result;
                if (quest != null && !_allLoadedQuests.Contains(quest))
                {
                    _allLoadedQuests.Add(quest);
                }
            }
            else
            {
                Debug.LogError($"Failed to load quest with GUID: {guid}");
            }
        }
    }
}
