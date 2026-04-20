using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DIalogueEventType
{
    None,
    ChangeScene,
    GiveItems,
}

[System.Serializable]
public struct Line 
{
    public string speakerName;
    [TextArea(2,3)]
    public string dialogueLine;

    public DIalogueEventType eventType;

    public string optionalParameter;

   // public List<DialogueItemReward> itemsToGive
}

[CreateAssetMenu(fileName ="NewConversation", menuName="Dialogue/Conversation")]
public class ConversationTemplate : ScriptableObject
{
    public List<Line> conversationLines;
}
