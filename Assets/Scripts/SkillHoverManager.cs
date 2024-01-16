using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SkillHoverManager : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject SkillDescriptionUI;
    public TextMeshProUGUI SkillName;
    public TextMeshProUGUI SkillDesc;

    private GameObject currentHoveredButton;

    void Start()
    {
        SkillDescriptionUI.SetActive(false);
        SetupButtonHoverEvents(button1);
        SetupButtonHoverEvents(button2);
        SetupButtonHoverEvents(button3);
    }

    void SetupButtonHoverEvents(GameObject button)
    {
        if (button == null)
        {
            Debug.LogError("Button GameObject is null!");
            return;
        }

        EventTrigger eventTrigger = button.GetComponent<EventTrigger>();

        if (eventTrigger == null)
        {
            eventTrigger = button.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) => { OnPointerEnter(button); });
        eventTrigger.triggers.Add(entryEnter);

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnPointerExit(); });
        eventTrigger.triggers.Add(entryExit);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F1) && currentHoveredButton != null)
        {
            OnF1KeyPress(currentHoveredButton.name);
        }
        else if (Input.GetKeyUp(KeyCode.F1))
        {
            OnF1KeyReleased();
        }
    }

    void OnPointerEnter(GameObject button)
    {
        currentHoveredButton = button;
    }

    void OnPointerExit()
    {
        currentHoveredButton = null;
    }

    void OnF1KeyPress(string buttonName)
    {
        SkillDescriptionUI.SetActive(true);
        string desc = "";
        switch (buttonName)
        {
            case "Skill1":
                SkillName.text = PlayerSkills.instance.ProcessSkillName(PlayerSkills.instance.S1);
                desc = PlayerSkills.instance.GetSkillDescription(PlayerSkills.instance.S1);
                break;
            case "Skill2":
                SkillName.text = PlayerSkills.instance.ProcessSkillName(PlayerSkills.instance.S2);
                desc = PlayerSkills.instance.GetSkillDescription(PlayerSkills.instance.S2);
                break;
            case "Skill3":
                SkillName.text = PlayerSkills.instance.ProcessSkillName(PlayerSkills.instance.S3);
                desc = PlayerSkills.instance.GetSkillDescription(PlayerSkills.instance.S3);
                break;
            default:
                break;
        }
        SkillDesc.text = desc;
    }

    void OnF1KeyReleased()
    {
        SkillDescriptionUI.SetActive(false);
        currentHoveredButton = null;
    }
}
