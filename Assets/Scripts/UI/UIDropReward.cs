using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UIDropReward : MonoBehaviour
{
    [SerializeField] private GameObject itemDropPrefab;
    [SerializeField] private float dropRadius = 0.8f;
    [SerializeField] private float dropHeight = 2.0f;
    [SerializeField] private float dropDuration = 0.5f;
    [SerializeField] private float collectDelay = 1.0f;
    [SerializeField] private float flyDuration = 0.5f;
    [SerializeField] private AudioClip dropSound;
    
    [Header("Broadcasting On")]
    [SerializeField] private ItemEventChannelSO _addItemEvent;
    
    [Header("Listening To")]
    [SerializeField] private EnemyDefeatedEventSO _enemyDefeatedEvent;

    private void OnEnable()
    {
        _enemyDefeatedEvent.OnEventRaised += SpawnDropItems;
    }

    private void OnDisable()
    {
        _enemyDefeatedEvent.OnEventRaised -= SpawnDropItems;
    }

    public void SpawnDropItems(Vector2 spawnPosition, List<ItemSO> droppedItems)
    {
        StartCoroutine(SpawnDropCoroutine(droppedItems, spawnPosition));
    }

    private IEnumerator SpawnDropCoroutine(List<ItemSO> droppedItems, Vector3 spawnPosition)
    {
        List<GameObject> spawnedItems = new List<GameObject>();

        foreach (var item in droppedItems)
        {
            Vector3 randomOffset = Random.insideUnitCircle * dropRadius;
            Vector3 itemPosition = spawnPosition + new Vector3(randomOffset.x, dropHeight, randomOffset.y);

            GameObject dropObj = Instantiate(itemDropPrefab, itemPosition, Quaternion.identity);
            UIDropItem dropItem = dropObj.GetComponent<UIDropItem>();
            dropItem.Initialize(item);
            
            spawnedItems.Add(dropObj);
            
            StartCoroutine(AnimateDrop(dropObj, spawnPosition + randomOffset));
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(collectDelay);

        Transform playerTransform  = GameObject.FindGameObjectWithTag("Player").transform;
        foreach (var dropObj in spawnedItems)
        {
            StartCoroutine(MoveToPlayer(dropObj, playerTransform));
        }
    }

    private IEnumerator AnimateDrop(GameObject item, Vector3 targetPosition)
    {
        float elapsedTime = 0;
        Vector3 startPos = item.transform.position;

        while (elapsedTime < dropDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / dropDuration;
            item.transform.position = Vector3.Lerp(startPos, targetPosition, t);
            yield return null;
        }

        if (dropSound)
        {
            AudioSource.PlayClipAtPoint(dropSound, targetPosition);
        }
    }

    private IEnumerator MoveToPlayer(GameObject item, Transform playerTransform)
    {
        float elapsedTime = 0;
        Vector3 startPos = item.transform.position;
        Vector3 endPos = playerTransform.position;
        Vector3 startScale = item.transform.localScale;

        while (elapsedTime < flyDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / flyDuration;
            item.transform.position = Vector3.Lerp(startPos, endPos, t);
            item.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);
            yield return null;
        }

        ItemSO itemSO = item.GetComponent<UIDropItem>().GetItem();
        
        AddItemToInventory(itemSO);
        Destroy(item);
    }
    
    private void AddItemToInventory(ItemSO item)
    {
        Debug.Log("Add Item To Inventory");
        _addItemEvent.RaiseEvent(item);
    }
}
