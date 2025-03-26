using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    [SerializeField] private List<DialogueDataSO> _dialogueData;
    
    [Header("Broadcasting On")]
    [SerializeField] private DialogueEventChannelSO _triggerDialogueEvent;
    
    public int currentDialogueIndex = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (currentDialogueIndex < _dialogueData.Count)
            {
                Debug.Log("Dialogue Triggered");
                _triggerDialogueEvent.RaiseEvent(_dialogueData[currentDialogueIndex]);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Dialogue Exit");
        }
    }
}