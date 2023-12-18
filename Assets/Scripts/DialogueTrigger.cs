using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public AllMessages[] AllMessages;
    public Actor[] actors;
    public static int AllMessages_Index = 0;
    public static int AllMessages_Length = 0;

    public void StartDialogue()
    {
        AllMessages_Length = AllMessages.Length;
        Message[] msg = AllMessages[AllMessages_Index].messages_array;
        FindAnyObjectByType<DialogueManager>().OpenDialogue(msg, actors);
    }
}

[System.Serializable]
public class AllMessages
{
    public Message[] messages_array;
}

[System.Serializable]
public class Message
{
    public int actorID;
    public string msg;
}

[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}
