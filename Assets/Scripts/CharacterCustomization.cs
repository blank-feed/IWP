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
    public GameObject SelectionUI;
    public GameObject ConfirmationUI;

    //animator controllers
    public RuntimeAnimatorController[] animators;

    //Skill Images
    public Sprite[] skillImg;

    public Image ConfirmationImg;

    //sprites
    public Sprite paladin;
    public Sprite rogue;
    public Sprite sorcerer;
    public Sprite ranger;
    public Sprite fighter;
    public Sprite druid;

    //buttons
    public Button PaladinBtn;
    public Button RogueBtn;
    public Button SorcererBtn;
    public Button RangerBtn;
    public Button FighterBtn;
    public Button DruidBtn;

    public Button Yesbtn;
    public Button Nobtn;

    //TMPro
    public TextMeshProUGUI RaceClassText;


    public PlayerSkills playerSkills;

    // Start is called before the first frame update
    void Start()
    {
        Button PaladinButton = PaladinBtn.GetComponent<Button>();
        Button RogueButton = RogueBtn.GetComponent<Button>();
        Button SorcererButton = SorcererBtn.GetComponent<Button>();
        Button RangerButton = RangerBtn.GetComponent<Button>();
        Button FighterButton = FighterBtn.GetComponent<Button>();
        Button DruidButton = DruidBtn.GetComponent<Button>();

        PaladinButton.onClick.AddListener(() => ClassType(0));
        SorcererButton.onClick.AddListener(() => ClassType(1)); 
        FighterButton.onClick.AddListener(() => ClassType(2));
        RogueButton.onClick.AddListener(() => ClassType(3));
        RangerButton.onClick.AddListener(() => ClassType(4));
        DruidButton.onClick.AddListener(() => ClassType(5));

        Button YesBtn = Yesbtn.GetComponent<Button>();
        Button NoBtn = Nobtn.GetComponent<Button>();
        NoBtn.onClick.AddListener(Back);
        YesBtn.onClick.AddListener(GoToScene); //Switch Scene
    }

    void GoToScene()
    {
        SceneManager.LoadScene(1);
    }

    void ClassType(int x)
    {
        SelectionUI.SetActive(!SelectionUI.activeSelf);
        ConfirmationUI.SetActive(!ConfirmationUI.activeSelf);

        switch (x)
        {
            case 0:
                RaceClassText.text = "Class : Paladin";
                PlayerManager.instance.PlayerRace = PlayerManager.Race.Human;
                PlayerManager.instance.PlayerClass = PlayerManager.Class.Paladin;
                playerSkills.S1 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(0);
                playerSkills.S2 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(1);
                playerSkills.S3 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(2);
                playerSkills.S1_Img = skillImg[0];
                playerSkills.S2_Img = skillImg[1];
                playerSkills.S3_Img = skillImg[2];
                PlayerManager.instance.spriteChosen = paladin;
                PlayerManager.instance.maxHealth = 100;
                PlayerManager.instance.health = 100;
                PlayerManager.instance.gameObject.GetComponent<Animator>().runtimeAnimatorController = animators[0];
                break;
            case 1:
                RaceClassText.text = "Class : Sorcerer";
                PlayerManager.instance.PlayerRace = PlayerManager.Race.HighElf;
                PlayerManager.instance.PlayerClass = PlayerManager.Class.Sorcerer;
                playerSkills.S1 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(6);
                playerSkills.S2 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(7);
                playerSkills.S3 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(8);
                playerSkills.S1_Img = skillImg[6];
                playerSkills.S2_Img = skillImg[7];
                playerSkills.S3_Img = skillImg[8];
                PlayerManager.instance.spriteChosen = sorcerer;
                PlayerManager.instance.maxHealth = 60;
                PlayerManager.instance.health = PlayerManager.instance.maxHealth;
                PlayerManager.instance.gameObject.GetComponent<Animator>().runtimeAnimatorController = animators[2];
                break;
            case 2:
                RaceClassText.text = "Class : Fighter";
                PlayerManager.instance.PlayerRace = PlayerManager.Race.Dragonborn;
                PlayerManager.instance.PlayerClass = PlayerManager.Class.Fighter;
                playerSkills.S1 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(12);
                playerSkills.S2 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(13);
                playerSkills.S3 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(14);
                playerSkills.S1_Img = skillImg[12];
                playerSkills.S2_Img = skillImg[13];
                playerSkills.S3_Img = skillImg[14];
                PlayerManager.instance.spriteChosen = fighter;
                PlayerManager.instance.maxHealth = 100;
                PlayerManager.instance.health = PlayerManager.instance.maxHealth;
                PlayerManager.instance.gameObject.GetComponent<Animator>().runtimeAnimatorController = animators[4];
                break;

            case 3:
                RaceClassText.text = "Class : Rogue";
                PlayerManager.instance.PlayerRace = PlayerManager.Race.Human;
                PlayerManager.instance.PlayerClass = PlayerManager.Class.Rogue;
                playerSkills.S1 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(3);
                playerSkills.S2 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(4);
                playerSkills.S3 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(5);
                playerSkills.S1_Img = skillImg[3];
                playerSkills.S2_Img = skillImg[4];
                playerSkills.S3_Img = skillImg[5];
                PlayerManager.instance.spriteChosen = rogue;
                PlayerManager.instance.maxHealth = 75;
                PlayerManager.instance.health = PlayerManager.instance.maxHealth;
                PlayerManager.instance.gameObject.GetComponent<Animator>().runtimeAnimatorController = animators[1];
                break;
            case 4:
                RaceClassText.text = "Class : Ranger";
                PlayerManager.instance.PlayerRace = PlayerManager.Race.HighElf;
                PlayerManager.instance.PlayerClass = PlayerManager.Class.Ranger;
                playerSkills.S1 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(9);
                playerSkills.S2 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(10);
                playerSkills.S3 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(11);
                playerSkills.S1_Img = skillImg[9];
                playerSkills.S2_Img = skillImg[10];
                playerSkills.S3_Img = skillImg[11];
                PlayerManager.instance.spriteChosen = ranger;
                PlayerManager.instance.maxHealth = 60;
                PlayerManager.instance.health = PlayerManager.instance.maxHealth;
                PlayerManager.instance.gameObject.GetComponent<Animator>().runtimeAnimatorController = animators[3];
                break;
            case 5:
                RaceClassText.text = "Class : Druid";
                PlayerManager.instance.PlayerRace = PlayerManager.Race.Dragonborn;
                PlayerManager.instance.PlayerClass = PlayerManager.Class.Druid;
                playerSkills.S1 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(15);
                playerSkills.S2 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(16);
                playerSkills.S3 = (PlayerSkills.AllSkills)playerSkills.skillValues.GetValue(17);
                playerSkills.S1_Img = skillImg[15];
                playerSkills.S2_Img = skillImg[16];
                playerSkills.S3_Img = skillImg[17];
                PlayerManager.instance.spriteChosen = druid;
                PlayerManager.instance.maxHealth = 70;
                PlayerManager.instance.health = PlayerManager.instance.maxHealth;
                PlayerManager.instance.gameObject.GetComponent<Animator>().runtimeAnimatorController = animators[5];
                break;
            default:
                break;
        }
        ConfirmationImg.sprite = PlayerManager.instance.spriteChosen;
    }

    void Back()
    {
        SelectionUI.SetActive(true);
        ConfirmationUI.SetActive(false);
    }
}
