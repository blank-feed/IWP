using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject DialogueUI;

    public Image actorImg;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI msgText;
    public RectTransform backgroundBox;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    public static bool isActive = false;
    public static DialogueManager instance;
    bool GoFightScene = false;

    public void OpenDialogue(Message[] messages, Actor[] actors, bool canFight)
    {
        DialogueUI.SetActive(true);
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;
        GoFightScene = canFight;

        DisplayMessage();
    }

    void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        msgText.text = messageToDisplay.msg;

        Actor actorToDisplay = currentActors[messageToDisplay.actorID];
        actorName.text = actorToDisplay.name;
        actorImg.sprite = actorToDisplay.sprite;
        if (actorToDisplay.self)
        {
            actorImg.sprite = PlayerManager.instance.spriteChosen;
        }
    }

    public void NextMessage()
    {
        activeMessage++;

        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            DialogueTrigger.AllMessages_Index = 0;
            DialogueTrigger.AllMessages_Length = 0;
            PlayerManager.instance.dialogueStarted = false;
            isActive = false;
            DialogueUI.SetActive(false);

            if (GoFightScene)
            {
                SceneManager.LoadScene("TMBattleScene");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DialogueUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isActive)
        {
            NextMessage();
        }
    }
}
