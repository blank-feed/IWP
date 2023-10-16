using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCustomization : MonoBehaviour
{
    //references
    //game objects
    public GameObject RaceSelectionUI;
    public GameObject ClassSelectionUI;
    public GameObject ConfirmationUI;

    //buttons
    public Button Race0btn;
    public Button Race1btn;
    public Button Race2btn;
    public Button Backbtn;
    public Button Class0btn;
    public Button Class1btn;
    public Button Yesbtn;
    public Button Nobtn;

    //TMPro
    public TextMeshProUGUI Class0Text;
    public TextMeshProUGUI Class1Text;
    public TextMeshProUGUI RaceClassText;

    //int
    int classInt;

    // Start is called before the first frame update
    void Start()
    {
        Button Race0Btn = Race0btn.GetComponent<Button>();
        Button Race1Btn = Race1btn.GetComponent<Button>();
        Button Race2Btn = Race2btn.GetComponent<Button>();
        Button BackBtn = Backbtn.GetComponent<Button>();
        Button Class0Btn = Class0btn.GetComponent<Button>();
        Button Class1Btn = Class1btn.GetComponent<Button>();
        Button YesBtn = Yesbtn.GetComponent<Button>();
        Button NoBtn = Nobtn.GetComponent<Button>();

        Race0Btn.onClick.AddListener(() => ClassType(0));
        Race1Btn.onClick.AddListener(() => ClassType(1));
        Race2Btn.onClick.AddListener(() => ClassType(2));
        BackBtn.onClick.AddListener(Back);
        Class0Btn.onClick.AddListener(() => ClassConfirmation(classInt, 0));
        Class1Btn.onClick.AddListener(() => ClassConfirmation(classInt, 1));
        NoBtn.onClick.AddListener(Back);
        YesBtn.onClick.AddListener(Back); //Switch Scene
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ClassType(int x)
    {
        RaceSelectionUI.SetActive(!RaceSelectionUI.activeSelf);
        ClassSelectionUI.SetActive(!ClassSelectionUI.activeSelf);

        switch (x)
        {
            case 0:
                classInt = x;
                Class0Text.text = "Paladin";
                Class1Text.text = "Rogue";
                break;
            case 1:
                classInt = x;
                Class0Text.text = "Sorcerer";
                Class1Text.text = "Ranger";
                break;
            case 2:
                classInt = x;
                Class0Text.text = "Fighter";
                Class1Text.text = "Druid";
                break;
            default:
                break;
        }
    }

    void ClassConfirmation(int x, int LeftRightChoice)
    {
        ClassSelectionUI.SetActive(!ClassSelectionUI.activeSelf);
        ConfirmationUI.SetActive(!ConfirmationUI.activeSelf);

        if (LeftRightChoice == 0)
        {
            switch (x)
            {
                case 0:
                    RaceClassText.text = "Race : Human \n Class : Paladin";
                    break;
                case 1:
                    RaceClassText.text = "Race : High Elf \n Class : Sorcerer";
                    break;
                case 2:
                    RaceClassText.text = "Race : Dragonborn \n Class : Fighter";
                    break;
                default:
                    break;
            }
        }

        else
        {
            switch (x)
            {
                case 0:
                    RaceClassText.text = "Race : Human \n Class : Rogue";
                    break;
                case 1:
                    RaceClassText.text = "Race : High Elf \n Class : Ranger";
                    break;
                case 2:
                    RaceClassText.text = "Race : Dragonborn \n Class : Druid";
                    break;
                default:
                    break;
            }
        }
    }

    void Back()
    {
        RaceSelectionUI.SetActive(true);
        ClassSelectionUI.SetActive(false);
        ConfirmationUI.SetActive(false);
    }
}
