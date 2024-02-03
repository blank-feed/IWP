using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool lastfight = false;
    public bool talkedBefore = false;
    public bool fightable;
    public AllMessages[] AllMessages;
    public Actor[] actors;
    public static int AllMessages_Index = 0;
    public static int AllMessages_Length = 0;

    public void StartDialogue()
    {
        AllMessages_Length = AllMessages.Length;
        if (PlayerPrefs.GetInt(gameObject.name, 0) == 0)
        {
            AllMessages_Index = 0;
        }
        else
        {
            if (AllMessages.Length != 1)
            {
                AllMessages_Index = 1;
            }
            else
            {
                AllMessages_Index = 0;
            }
        }
        fightable = AllMessages[AllMessages_Index].fightable;
        Message[] msg = AllMessages[AllMessages_Index].messages_array;
        FindAnyObjectByType<DialogueManager>().OpenDialogue(msg, actors, fightable);
    }
}

[System.Serializable]
public class AllMessages
{
    public Message[] messages_array;
    public bool fightable;
    public bool NeedGive;
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
