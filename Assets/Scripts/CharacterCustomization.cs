using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterCustomization : MonoBehaviour
{
    //references

    //game objects
    public GameObject RaceSelectionUI;
    public GameObject ClassSelectionUI;
    public GameObject ConfirmationUI;

    //Skill Images
    public Sprite[] skillImg;

    //sprites
    public Sprite paladin;

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


    public PlayerSkills playerSkills;

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
        YesBtn.onClick.AddListener(GoToScene); //Switch Scene
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GoToScene()
    {
        SceneManager.LoadScene(1);
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
                    PlayerManager.instance.PlayerRace = PlayerManager.Race.Human;
                    PlayerManager.instance.PlayerClass = PlayerManager.Class.Paladin;
                    playerSkills.S1 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(0);
                    playerSkills.S2 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(1);
                    playerSkills.S3 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(2);
                    playerSkills.S1_Img = skillImg[0];
                    playerSkills.S2_Img = skillImg[1];
                    playerSkills.S3_Img = skillImg[2];
                    PlayerManager.instance.playerSprite = paladin;
                    break;
                case 1:
                    RaceClassText.text = "Race : High Elf \n Class : Sorcerer";
                    PlayerManager.instance.PlayerRace = PlayerManager.Race.HighElf;
                    PlayerManager.instance.PlayerClass = PlayerManager.Class.Sorcerer;
                    playerSkills.S1 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(6);
                    playerSkills.S2 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(7);
                    playerSkills.S3 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(8);
                    playerSkills.S1_Img = skillImg[6];
                    playerSkills.S2_Img = skillImg[7];
                    playerSkills.S3_Img = skillImg[8];
                    PlayerManager.instance.playerSprite = paladin;
                    break;
                case 2:
                    RaceClassText.text = "Race : Dragonborn \n Class : Fighter";
                    PlayerManager.instance.PlayerRace = PlayerManager.Race.Dragonborn;
                    PlayerManager.instance.PlayerClass = PlayerManager.Class.Fighter;
                    playerSkills.S1 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(12);
                    playerSkills.S2 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(13);
                    playerSkills.S3 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(14);
                    playerSkills.S1_Img = skillImg[12];
                    playerSkills.S2_Img = skillImg[13];
                    playerSkills.S3_Img = skillImg[14];
                    PlayerManager.instance.playerSprite = paladin;
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
                    PlayerManager.instance.PlayerRace = PlayerManager.Race.Human;
                    PlayerManager.instance.PlayerClass = PlayerManager.Class.Rogue;
                    playerSkills.S1 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(3);
                    playerSkills.S2 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(4);
                    playerSkills.S3 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(5);
                    playerSkills.S1_Img = skillImg[3];
                    playerSkills.S2_Img = skillImg[4];
                    playerSkills.S3_Img = skillImg[5];
                    PlayerManager.instance.playerSprite = paladin;
                    break;
                case 1:
                    RaceClassText.text = "Race : High Elf \n Class : Ranger";
                    PlayerManager.instance.PlayerRace = PlayerManager.Race.HighElf;
                    playerSkills.S1 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(9);
                    playerSkills.S2 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(10);
                    playerSkills.S3 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(11);
                    playerSkills.S1_Img = skillImg[9];
                    playerSkills.S2_Img = skillImg[10];
                    playerSkills.S3_Img = skillImg[11];
                    PlayerManager.instance.playerSprite = paladin;
                    break;
                case 2:
                    RaceClassText.text = "Race : Dragonborn \n Class : Druid";
                    PlayerManager.instance.PlayerRace = PlayerManager.Race.Dragonborn;
                    PlayerManager.instance.PlayerClass = PlayerManager.Class.Druid;
                    playerSkills.S1 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(15);
                    playerSkills.S2 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(16);
                    playerSkills.S3 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(17);
                    playerSkills.S1_Img = skillImg[15];
                    playerSkills.S2_Img = skillImg[16];
                    playerSkills.S3_Img = skillImg[17];
                    PlayerManager.instance.playerSprite = paladin;
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
