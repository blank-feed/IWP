using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool talkedBefore = false;
    public bool fightable;
    public AllMessages[] AllMessages;
    public Actor[] actors;
    public static int AllMessages_Index = 0;
    public static int AllMessages_Length = 0;

    public void StartDialogue()
    {
        PlayerManager.instance.fightable = fightable;
        AllMessages_Length = AllMessages.Length;
        if (!talkedBefore)
        {
            AllMessages_Index = 0;
        }
        else
        {
            AllMessages_Index = 1;
        }
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
    public bool self;
}
