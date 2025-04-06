using System.Collections.Generic;
using UnityEngine;

public class MonsterLoot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ProtagonistStateSO _player;
    [SerializeField] private MonsterSO _entity;
    [SerializeField] private GameObject expTextPrefab;
    [SerializeField] private IntEventChannelSO _expGainEvent;
    
    [Header("Broadcasting On")]
    [SerializeField] private EnemyDefeatedEventSO _enemyDefeatedEvent;

    public void DropReward()
    {
        MonsterDropTableSO monsterDropTable = _entity.MonsterDropTable;
        List<ItemSO> droppedItems = monsterDropTable.GetDrops(_entity.Rarity);

        if (droppedItems.Count > 0)
        {
            _enemyDefeatedEvent.RaiseEvent(transform.position, droppedItems);
        }

        int receivedExp = ExpCalculator.CalculateExpGain(_player.currentRealmTier, _entity);
        ShowExpText(receivedExp);
    }

    private void ShowExpText(int gainExp)
    {
        Vector3 spawnPosition = transform.position + new Vector3(0, 1f, 0);
        GameObject expTextObj = Instantiate(expTextPrefab, spawnPosition, Quaternion.identity);
        expTextObj.GetComponent<UIExpTextEffect>().Initialize(gainExp);
        _expGainEvent.RaiseEvent(gainExp);
    }
}