using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SkillsUI : MonoBehaviour
{
    public GameObject SkillDescriptionUI;

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

    bool open = false;

    void Start()
    {
        SkillDescriptionUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            open = !open;
            if (open)
            {
                OnF1KeyPress();
            }
            else
            {
                OnF1KeyReleased();
            }
        }

        if (SkillDescriptionUI.activeSelf)
        {
            PlayerManager.instance.canMove = false;
        }
        else
        {
            PlayerManager.instance.canMove = true;
        }
    }

    public void OnF1KeyPress()
    {
        SkillDescriptionUI.SetActive(true);

        Skill1Img.sprite = PlayerSkills.instance.S1_Img;
        Skill1Name.text = PlayerSkills.instance.ProcessSkillName(PlayerSkills.instance.S1);
        Skill1Desc.text = PlayerSkills.instance.GetSkillDescription(PlayerSkills.instance.S1);

        Skill2Img.sprite = PlayerSkills.instance.S2_Img;
        Skill2Name.text = PlayerSkills.instance.ProcessSkillName(PlayerSkills.instance.S2);
        Skill2Desc.text = PlayerSkills.instance.GetSkillDescription(PlayerSkills.instance.S2);

        Skill3Img.sprite = PlayerSkills.instance.S3_Img;
        Skill3Name.text = PlayerSkills.instance.ProcessSkillName(PlayerSkills.instance.S3);
        Skill3Desc.text = PlayerSkills.instance.GetSkillDescription(PlayerSkills.instance.S3);

        string boonnamestr = "";
        switch (PlayerManager.instance.PlayerClass)
        {
            case PlayerManager.Class.Paladin:
                BoonImg.sprite = PlayerSkills.instance.Boon_Img[0];
                boonnamestr = "Holy Buff"; //aka Deficiency
                BoonDesc.text = "Gains more damage by losing more health";
                break;
            case PlayerManager.Class.Rogue:
                BoonImg.sprite = PlayerSkills.instance.Boon_Img[1];
                boonnamestr = "Confiscation";
                BoonDesc.text = "Gains more damage by stealing attack stats from enemies";
                break;
            case PlayerManager.Class.Sorcerer:
                BoonImg.sprite = PlayerSkills.instance.Boon_Img[2];
                boonnamestr = "Amplify";
                BoonDesc.text = "Casts a stronger spell on every 4th spell cast";
                break;
            case PlayerManager.Class.Ranger:
                BoonImg.sprite = PlayerSkills.instance.Boon_Img[3];
                boonnamestr = "Crippled";
                BoonDesc.text = /*"Gains more damage by being further away from enemies\n\n*/"Deals 30% more damage to crippled enemies";
                break;
            case PlayerManager.Class.Fighter:
                BoonImg.sprite = PlayerSkills.instance.Boon_Img[4];
                boonnamestr = "Bloodlust";
                BoonDesc.text = "Gains more damage by killing more enemies";
                break;
            case PlayerManager.Class.Druid:
                BoonImg.sprite = PlayerSkills.instance.Boon_Img[5];
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
        open = false;
        SkillDescriptionUI.SetActive(false);
    }
}
