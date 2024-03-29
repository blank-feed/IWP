using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SkillHoverManager : MonoBehaviour
{
    public GameObject SkillDescriptionUI;
    public GameObject MovementDescriptionUI;

    public Sprite HighShotReadyImg;

    public Image Skill1Img;
    public Image Skill2Img;
    public Image Skill3Img;

    public TextMeshProUGUI Skill1Name;
    public TextMeshProUGUI Skill2Name;
    public TextMeshProUGUI Skill3Name;

    public TextMeshProUGUI Skill1Desc;
    public TextMeshProUGUI Skill2Desc;
    public TextMeshProUGUI Skill3Desc;

    public Image BoonImg;
    public TextMeshProUGUI BoonName;
    public TextMeshProUGUI BoonDesc;


    void Start()
    {
        SkillDescriptionUI.SetActive(false);
        MovementDescriptionUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            OnF1KeyPress();
        }
        else if (Input.GetKeyUp(KeyCode.F1))
        {
            OnF1KeyReleased();
        }
    }

    public void OnF1KeyPress()
    {
        SkillDescriptionUI.SetActive(true);

        Skill1Img.sprite = PlayerSkills.instance.S1_Img;
        Skill1Name.text = PlayerSkills.instance.ProcessSkillName(PlayerSkills.instance.S1);
        Skill1Desc.text = PlayerSkills.instance.GetSkillDescription(PlayerSkills.instance.S1);

        Skill2Img.sprite = PlayerSkills.instance.S2_Img;
        if (PlayerManager.instance.PlayerClass == PlayerManager.Class.Ranger && BattleManager.instance.HighShotCount == 2)
        {
            Skill2Img.sprite = HighShotReadyImg;
        }
        Skill2Name.text = PlayerSkills.instance.ProcessSkillName(PlayerSkills.instance.S2);
        Skill2Desc.text = PlayerSkills.instance.GetSkillDescription(PlayerSkills.instance.S2);

        Skill3Img.sprite = PlayerSkills.instance.S3_Img;
        Skill3Name.text = PlayerSkills.instance.ProcessSkillName(PlayerSkills.instance.S3);
        Skill3Desc.text = PlayerSkills.instance.GetSkillDescription(PlayerSkills.instance.S3);

        BoonImg.sprite = BattleManager.instance.BoonImg.sprite;
        string boonnamestr = "";
        switch (PlayerManager.instance.PlayerClass)
        {
            case PlayerManager.Class.Paladin:
                boonnamestr = "Holy Buff"; //aka Deficiency
                BoonDesc.text = "Gains more damage by losing more health";
                break;
            case PlayerManager.Class.Rogue:
                boonnamestr = "Confiscation";
                BoonDesc.text = "Gains more damage by stealing attack stats from enemies";
                break;
            case PlayerManager.Class.Sorcerer:
                boonnamestr = "Amplify";
                BoonDesc.text = "Casts a stronger spell on every 4th spell cast";
                break;
            case PlayerManager.Class.Ranger:
                boonnamestr = "Crippled";
                BoonDesc.text = /*"Gains more damage by being further away from enemies\n\n*/"Deals 30% more damage to crippled enemies";
                break;
            case PlayerManager.Class.Fighter:
                boonnamestr = "Bloodlust";
                BoonDesc.text = "Gains more damage by killing more enemies";
                break;
            case PlayerManager.Class.Druid:
                boonnamestr = "Dragon Pals";
                BoonDesc.text = "Gains more damage by having more Dragon Pals";
                break;
            default:
                break;
        }
        BoonName.text = "Boon :\n" + boonnamestr;
    }

    public void OnF1KeyReleased()
    {
        SkillDescriptionUI.SetActive(false);
    }

    public void OpenMovementUI()
    {
        MovementDescriptionUI.SetActive(true);
    }

    public void CloseMovementUI()
    {
        MovementDescriptionUI.SetActive(false);
    }
}
