using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[CreateAssetMenu(fileName = "Storage_QuestList", menuName = "Quest/Current Quest List")]
public class QuestListSO : ScriptableObject, IDataPersistence
{
    [SerializeField] private List<QuestDataSO> _currentQuests = new List<QuestDataSO>();
    
    public List<QuestDataSO> CurrentQuests => _currentQuests;

    public void AddQuest(QuestDataSO quest)
    {
        _currentQuests.Add(quest);
    }

    public void SaveData(ref GameData data)
    {
        data.finishedQuestItemsGUIds.Clear(); 
        
        foreach (var quest in _currentQuests)
        {
            string questID = quest.Guid;
            data.finishedQuestItemsGUIds.Add(questID);
        }
    }

    public void LoadData(GameData data)
    {
        _currentQuests.Clear();

        AddressableLoader.LoadAssetsByGuids<QuestDataSO>(data.finishedQuestItemsGUIds, (loadedQuests) =>
        {
            _currentQuests.AddRange(loadedQuests);
            Debug.Log($"Loaded {_currentQuests.Count} quests from Addressables.");
        });
    }
}