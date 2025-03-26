using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;
using System.Collections.Generic;

public class UIDialogue : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private InputReader _inputReader;
    
    [SerializeField] private TextMeshProUGUI _actorText;
    [SerializeField] private LocalizeStringEvent _dialogueTextEvent;
    [SerializeField] private TextMeshProUGUI _leftChoiceText;
    [SerializeField] private TextMeshProUGUI _rightChoiceText;

    [Header("Broadcasting On")]
    [SerializeField] private VoidEventChannelSO _endedDialogueEvent;
    [SerializeField] private QuestEventChannelSO _addedQuestEvent;
    [SerializeField] private QuestEventChannelSO _completedQuestEvent;

    [Header("Listening To")]
    [SerializeField] private DialogueEventChannelSO _onStartDialogue;

    private DialogueDataSO _currentDialogue;
    private int _currentNodeIndex = 0;
    private List<DialogueChoice> _currentChoices = new();

    private void OnEnable()
    {
        _onStartDialogue.OnEventRaised += StartDialogue;
    }

    private void OnDisable()
    {
        _onStartDialogue.OnEventRaised -= StartDialogue;
    }

    private void StartDialogue(DialogueDataSO dialogueData)
    {
        _currentDialogue = dialogueData;
        _currentNodeIndex = 0;
        ShowDialogue();
    }

    private void ShowDialogue()
    {
        DialogueNode currentNode = _currentDialogue.Nodes[_currentNodeIndex];
        _dialogueTextEvent.StringReference = currentNode.dialogueText;

        UpdateChoices(currentNode.choices);
    }

    private void UpdateChoices(List<DialogueChoice> choices)
    {
        _currentChoices = choices;

        _leftChoiceText.gameObject.SetActive(choices.Count > 0);
        _rightChoiceText.gameObject.SetActive(choices.Count > 1);

        if (choices.Count > 0)
            _leftChoiceText.text = choices[0].choiceText.GetLocalizedString();
        if (choices.Count > 1)
            _rightChoiceText.text = choices[1].choiceText.GetLocalizedString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
        
        bool isEnded = _currentDialogue.Nodes[_currentNodeIndex].isEndNode || _currentNodeIndex >= _currentDialogue.Nodes.Count;
        if (isEnded)
        {
            EndDialogue();
            return;
        }
        
        if (clickedObject == _leftChoiceText.gameObject)
        {
            ChooseOption(0);
        }
        else if (clickedObject == _rightChoiceText.gameObject)
        {
            ChooseOption(1);
        } 
        else if (_currentChoices.Count == 0)
        {
            _currentNodeIndex++;
            ShowDialogue();
        }
    }

    private void ChooseOption(int choiceIndex)
    {
        if (_currentDialogue == null || _currentNodeIndex >= _currentDialogue.Nodes.Count) return;
        if (choiceIndex < 0 || choiceIndex >= _currentChoices.Count) return;

        DialogueChoice choice = _currentChoices[choiceIndex];

        if (choice.questToStart != null)
        {
            Debug.Log("Exist quest");
            _addedQuestEvent.RaiseEvent(choice.questToStart);
        }
        
        _currentNodeIndex = choice.nextNodeIndex;
        ShowDialogue();
    }

    private void EndDialogue()
    {
        _endedDialogueEvent.RaiseEvent();
    }
}