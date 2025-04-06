using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[Serializable]
public class DialogueChoice
{
    public LocalizedString choiceText;
    public QuestDataSO questToStart;
    public int nextNodeIndex;
}

[Serializable]
public class DialogueNode
{
    public LocalizedString dialogueText;
    public List<DialogueChoice> choices;
    public bool isEndNode = false;
}

[CreateAssetMenu(fileName = "Dialogue_NPC_JadeFairy_Welcome", menuName = "Dialogues/Dialogue Data ")]
public class DialogueDataSO : ScriptableObject
{
    [SerializeField] private List<DialogueNode> nodes;
    
    public List<DialogueNode> Nodes => nodes;
}